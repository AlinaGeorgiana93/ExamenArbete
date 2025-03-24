USE graphefc;
GO

--01-create-schema.sql
--create a schema for guest users, i.e. not logged in
CREATE SCHEMA gstusr;
GO
--create a schema for logged in user
CREATE SCHEMA usr;
GO

--02-create-gstusr-view.sql
--create a view that gives overview of the database content
CREATE OR ALTER VIEW gstusr.vwInfoDb AS
    SELECT 'Guest user database overview' as Title,
    (SELECT COUNT(*) FROM supusr.Staffs) as nrStaffs  -- Removed Seeded filter
GO




CREATE OR ALTER VIEW gstusr.vwInfoStaffs AS
    SELECT 
        s.FirstName,      -- Staff first name
        s.LastName,       -- Staff last name
        COUNT(p.PatientId) as NrPatients  -- Number of patients assigned to this staff
    FROM supusr.Staffs s
    LEFT JOIN supusr.Patients p ON p.StaffId = s.StaffId  -- Assuming a relationship between Staff and Patients
    GROUP BY s.FirstName, s.LastName WITH ROLLUP;
GO



--03-create-supusr-sp.sql
CREATE OR ALTER PROC supusr.spDeleteAll
    @Seeded BIT = 1
AS
BEGIN
    SET NOCOUNT ON;

    -- Delete from the current project tables: Staffs, Patients, Moods, Activities, Appetites, Graphs
    DELETE FROM supusr.Staffs WHERE Seeded = @Seeded;
    DELETE FROM supusr.Patients WHERE Seeded = @Seeded;
    DELETE FROM supusr.Moods WHERE Seeded = @Seeded;
    DELETE FROM supusr.Activities WHERE Seeded = @Seeded;
    DELETE FROM supusr.Appetites WHERE Seeded = @Seeded;
    DELETE FROM supusr.Graphs WHERE Seeded = @Seeded;

    -- Optionally, return the current overview of the database
    SELECT * FROM gstusr.vwInfoDb;

    -- Return the status
    RETURN 0;  -- Success

GO

-- Drop the existing constraint
ALTER TABLE [supusr].[Activities] 
DROP CONSTRAINT [FK_Activities_Patients_PatientDbMPatientId];

-- Add a new constraint with ON DELETE SET NULL or NO ACTION
ALTER TABLE [supusr].[Activities] 
ADD CONSTRAINT [FK_Activities_Patients_PatientDbMPatientId]
FOREIGN KEY ([PatientDbMPatientId]) REFERENCES [supusr].[Patients] ([PatientId]) ON DELETE SET NULL;


--04-create-users.sql
--Create 3 logins
IF SUSER_ID (N'gstusr') IS NOT NULL
DROP LOGIN gstusr;

IF SUSER_ID (N'usr') IS NOT NULL
DROP LOGIN usr;

IF SUSER_ID (N'supusr') IS NOT NULL
DROP LOGIN supusr;

CREATE LOGIN gstusr WITH PASSWORD=N'pa$$Word1', 
    DEFAULT_DATABASE=graphefc, DEFAULT_LANGUAGE=us_english, 
    CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;

CREATE LOGIN usr WITH PASSWORD=N'pa$$Word1', 
DEFAULT_DATABASE=graphefc, DEFAULT_LANGUAGE=us_english, 
CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;

CREATE LOGIN supusr WITH PASSWORD=N'pa$$Word1', 
DEFAULT_DATABASE=graphefc, DEFAULT_LANGUAGE=us_english, 
CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF;

--Create 3 users from the logins, we will later set credentials for these
DROP USER IF EXISTS  gstusrUser;
DROP USER IF EXISTS usrUser;
DROP USER IF EXISTS supusrUser;

CREATE USER gstusrUser FROM LOGIN gstusr;
CREATE USER usrUser FROM LOGIN usr;
CREATE USER supusrUser FROM LOGIN supusr;

--05-create-roles-credentials.sql
--create roles
CREATE ROLE graphefcGstUsr;
CREATE ROLE graphefcUsr;
CREATE ROLE graphefcSupUsr;

--assign securables credentials to the roles
GRANT SELECT, EXECUTE ON SCHEMA::gstusr to graphefcGstUsr;
GRANT SELECT ON SCHEMA::supusr to graphefcUsr;
GRANT SELECT, UPDATE, INSERT, DELETE, EXECUTE ON SCHEMA::supusr to graphefcSupUsr;

--finally, add the users to the roles
ALTER ROLE graphefcGstUsr ADD MEMBER gstusrUser;
ALTER ROLE graphefcGstUsr ADD MEMBER usrUser;
ALTER ROLE graphefcUsr ADD MEMBER usrUser;
ALTER ROLE graphefcGstUsr ADD MEMBER supusrUser;
ALTER ROLE graphefcUsr ADD MEMBER supusrUser;
ALTER ROLE graphefcSupUsr ADD MEMBER supusrUser;
GO

--07-create-gstusr-login.sql
CREATE OR ALTER PROC gstusr.spLogin
    @UserNameOrEmail NVARCHAR(100),
    @Password NVARCHAR(200),

    @UserId UNIQUEIDENTIFIER OUTPUT,
    @UserName NVARCHAR(100) OUTPUT,
    @Role NVARCHAR(100) OUTPUT
    AS
    SET NOCOUNT ON;

    SET @UserId = NULL;
    SET @UserName = NULL;
    SET @Role = NULL;

    SELECT Top 1 @UserId = UserId, @UserName = UserName, @Role = [Role] 
    FROM dbo.Users 
    WHERE ((UserName = @UserNameOrEmail) OR
           (Email IS NOT NULL AND (Email = @UserNameOrEmail))) 
           AND ([Password] = @Password
