using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DbContext.Migrations.SqlServerDbContext
{
    /// <inheritdoc />
    public partial class miInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "supusr");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Graphs",
                schema: "supusr",
                columns: table => new
                {
                    GraphId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Graphs", x => x.GraphId);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                schema: "supusr",
                columns: table => new
                {
                    StaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PersonalNumber = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "dbo",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                schema: "supusr",
                columns: table => new
                {
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GraphId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StaffDbMStaffId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PersonalNumber = table.Column<string>(type: "nvarchar(200)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatientId);
                    table.ForeignKey(
                        name: "FK_Patients_Graphs_GraphId",
                        column: x => x.GraphId,
                        principalSchema: "supusr",
                        principalTable: "Graphs",
                        principalColumn: "GraphId");
                    table.ForeignKey(
                        name: "FK_Patients_Staffs_StaffDbMStaffId",
                        column: x => x.StaffDbMStaffId,
                        principalSchema: "supusr",
                        principalTable: "Staffs",
                        principalColumn: "StaffId");
                });

            migrationBuilder.CreateTable(
                name: "ActivityLevels",
                schema: "supusr",
                columns: table => new
                {
                    ActivityLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    PatientDbMPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLevels", x => x.ActivityLevelId);
                    table.ForeignKey(
                        name: "FK_ActivityLevels_Patients_PatientDbMPatientId",
                        column: x => x.PatientDbMPatientId,
                        principalSchema: "supusr",
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "AppetiteLevels",
                schema: "supusr",
                columns: table => new
                {
                    AppetiteLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    PatientDbMPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppetiteLevels", x => x.AppetiteLevelId);
                    table.ForeignKey(
                        name: "FK_AppetiteLevels_Patients_PatientDbMPatientId",
                        column: x => x.PatientDbMPatientId,
                        principalSchema: "supusr",
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "MoodKinds",
                schema: "supusr",
                columns: table => new
                {
                    MoodKindId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    PatientDbMPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodKinds", x => x.MoodKindId);
                    table.ForeignKey(
                        name: "FK_MoodKinds_Patients_PatientDbMPatientId",
                        column: x => x.PatientDbMPatientId,
                        principalSchema: "supusr",
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "SleepLevels",
                schema: "supusr",
                columns: table => new
                {
                    SleepLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    PatientDbMPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepLevels", x => x.SleepLevelId);
                    table.ForeignKey(
                        name: "FK_SleepLevels_Patients_PatientDbMPatientId",
                        column: x => x.PatientDbMPatientId,
                        principalSchema: "supusr",
                        principalTable: "Patients",
                        principalColumn: "PatientId");
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                schema: "supusr",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StrDayOfWeek = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    StrDate = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PatientDbMPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityLevelDbMActivityLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activities_ActivityLevels_ActivityLevelDbMActivityLevelId",
                        column: x => x.ActivityLevelDbMActivityLevelId,
                        principalSchema: "supusr",
                        principalTable: "ActivityLevels",
                        principalColumn: "ActivityLevelId");
                    table.ForeignKey(
                        name: "FK_Activities_Patients_PatientDbMPatientId",
                        column: x => x.PatientDbMPatientId,
                        principalSchema: "supusr",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appetites",
                schema: "supusr",
                columns: table => new
                {
                    AppetiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    StrDayOfWeek = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    StrDate = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PatientDbMPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AppetiteLevelDbMAppetiteLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appetites", x => x.AppetiteId);
                    table.ForeignKey(
                        name: "FK_Appetites_AppetiteLevels_AppetiteLevelDbMAppetiteLevelId",
                        column: x => x.AppetiteLevelDbMAppetiteLevelId,
                        principalSchema: "supusr",
                        principalTable: "AppetiteLevels",
                        principalColumn: "AppetiteLevelId");
                    table.ForeignKey(
                        name: "FK_Appetites_Patients_PatientDbMPatientId",
                        column: x => x.PatientDbMPatientId,
                        principalSchema: "supusr",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Moods",
                schema: "supusr",
                columns: table => new
                {
                    MoodId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    StrDayOfWeek = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    StrDate = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PatientDbMPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoodKindDbMMoodKindId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Moods", x => x.MoodId);
                    table.ForeignKey(
                        name: "FK_Moods_MoodKinds_MoodKindDbMMoodKindId",
                        column: x => x.MoodKindDbMMoodKindId,
                        principalSchema: "supusr",
                        principalTable: "MoodKinds",
                        principalColumn: "MoodKindId");
                    table.ForeignKey(
                        name: "FK_Moods_Patients_PatientDbMPatientId",
                        column: x => x.PatientDbMPatientId,
                        principalSchema: "supusr",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sleeps",
                schema: "supusr",
                columns: table => new
                {
                    SleepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Day = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    StrDayOfWeek = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    StrDate = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    PatientDbMPatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SleepLevelDbMSleepLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sleeps", x => x.SleepId);
                    table.ForeignKey(
                        name: "FK_Sleeps_Patients_PatientDbMPatientId",
                        column: x => x.PatientDbMPatientId,
                        principalSchema: "supusr",
                        principalTable: "Patients",
                        principalColumn: "PatientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Sleeps_SleepLevels_SleepLevelDbMSleepLevelId",
                        column: x => x.SleepLevelDbMSleepLevelId,
                        principalSchema: "supusr",
                        principalTable: "SleepLevels",
                        principalColumn: "SleepLevelId");
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "ActivityLevels",
                columns: new[] { "ActivityLevelId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250428092945_miInitial.cs
                    { new Guid("71efd8e0-f61f-4b83-a1f9-ce859ac7499a"), "Jogging 🏃‍♂️", "Jogging", null, 10 },
                    { new Guid("9e01cf41-93db-4edd-a9e0-e94068e8e421"), "Training 🏋️‍♂️", "Training", null, 9 },
                    { new Guid("a86acc89-c30e-40a4-bda1-b9524f7bcedb"), "Reading 📖", "Reading", null, 3 },
                    { new Guid("a97a2ab9-9e40-453b-8240-3835d3aaf441"), "Swimming 🏊‍♂️", "Swimming", null, 7 },
                    { new Guid("b81bc185-51bc-45f5-bf47-5b05999cd651"), "Resting 🛌", "Resting", null, 1 },
                    { new Guid("c2c435d1-1708-4f2f-8498-162cf64440e7"), "Take a Walk 🚶‍♂️", "Take a Walk", null, 5 }
========
                    { new Guid("231b8e17-f4e0-4833-8b20-8323335013b8"), "Resting 🛌", "Resting", null, 1 },
                    { new Guid("559f45aa-af52-4d47-9401-915040ec5849"), "Jogging 🏃‍♂️", "Jogging", null, 10 },
                    { new Guid("5783350d-19f5-430b-8403-d8041e03d541"), "Training 🏋️‍♂️", "Training", null, 9 },
                    { new Guid("5e2166da-ad93-4cd9-93fd-213c9f3291ea"), "Take a Walk 🚶‍♂️", "Take a Walk", null, 5 },
                    { new Guid("b7ee4e1e-3551-4a24-8aa4-529219ee9cd2"), "Swimming 🏊‍♂️", "Swimming", null, 7 },
                    { new Guid("c41e433a-4274-4308-8414-59aeb9848f19"), "Reading 📖", "Reading", null, 3 }
>>>>>>>> 33c538c5673b6d7bc6542909128a9c0fc37d9e33:DbContext/Migrations/SqlServerDbContext/20250428093148_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "AppetiteLevels",
                columns: new[] { "AppetiteLevelId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250428092945_miInitial.cs
                    { new Guid("2d91da61-5792-407c-8685-0f1e048663fa"), "Very Much 🍴", "Very Much", null, 10 },
                    { new Guid("3a43f47a-7b71-481a-84c4-c8cb4c988679"), "Didn't Eat At All 🤢", "Didn't Eat At All", null, 1 },
                    { new Guid("4b3d9e53-55dc-43a9-8ecb-c78d933a5cc6"), "Normal Appetite 🙂", "Normal", null, 5 },
                    { new Guid("960ac13a-e84d-4275-98d7-bf84a310721b"), "Medium 😋", "Medium", null, 7 },
                    { new Guid("e0fac1fa-6f65-4e7a-b85e-5345890ca321"), "Little 🍽️", "Little", null, 3 }
========
                    { new Guid("0e349508-81dc-4b6d-bbe6-d605e4711abd"), "Medium 😋", "Medium", null, 7 },
                    { new Guid("63b6618c-38dd-4577-842a-fcad1aefd4d8"), "Didn't Eat At All 🤢", "Didn't Eat At All", null, 1 },
                    { new Guid("7412f31e-1a60-437a-92f6-a88eed546747"), "Very Much 🍴", "Very Much", null, 10 },
                    { new Guid("8640dbc5-765e-41f7-a160-cfd0e6c8e3a9"), "Little 🍽️", "Little", null, 3 },
                    { new Guid("dcffb13d-1bd6-4002-a411-99101de68952"), "Normal Appetite 🙂", "Normal", null, 5 }
>>>>>>>> 33c538c5673b6d7bc6542909128a9c0fc37d9e33:DbContext/Migrations/SqlServerDbContext/20250428093148_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "MoodKinds",
                columns: new[] { "MoodKindId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250428092945_miInitial.cs
                    { new Guid("090f2d39-fc71-491e-a006-42ca47e9d7d8"), "Depressed 😢", "Depressed", null, 1 },
                    { new Guid("0ef428d4-f741-459a-9a10-be02b1bf9cd8"), "Sad 🙁", "Sad", null, 2 },
                    { new Guid("3e6d933a-7cf5-4a39-bef4-d376395f9c7a"), "Happy 😃", "Happy", null, 10 },
                    { new Guid("4b42e1a4-9496-423f-b7bd-a26e29dfab49"), "Lovely 😍", "Lovely", null, 7 },
                    { new Guid("64ed15dd-21e7-4987-845a-fb5831ddd392"), "Angry 😡", "Angry", null, 3 },
                    { new Guid("a69bf5ef-ebcf-4ecd-812f-2e3fa339ffc1"), "Excited 🤩", "Excited", null, 9 },
                    { new Guid("fe9bfdb1-f5f6-4ece-bcc3-899b976ec52b"), "Bored 😒", "Bored", null, 4 }
========
                    { new Guid("20449aa4-ea55-47d2-9559-a7ba90606b63"), "Depressed 😢", "Depressed", null, 1 },
                    { new Guid("3c6b3571-945d-41ad-81ca-8d90a2699cd3"), "Angry 😡", "Angry", null, 3 },
                    { new Guid("5468ce36-b660-4cd4-a757-5ac2123fa81d"), "Bored 😒", "Bored", null, 4 },
                    { new Guid("a6f07910-961c-4a46-8380-eb43d4edb0b8"), "Sad 🙁", "Sad", null, 2 },
                    { new Guid("b76b1741-4fb5-40a8-af38-d71c158e37ea"), "Happy 😃", "Happy", null, 10 },
                    { new Guid("cf3132ab-d808-4fea-8ee1-1700d0fc4e02"), "Lovely 😍", "Lovely", null, 7 },
                    { new Guid("e2cfe8be-8859-49d2-832d-cc4fe8e2781a"), "Excited 🤩", "Excited", null, 9 }
>>>>>>>> 33c538c5673b6d7bc6542909128a9c0fc37d9e33:DbContext/Migrations/SqlServerDbContext/20250428093148_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "Patients",
                columns: new[] { "PatientId", "FirstName", "GraphId", "LastName", "PersonalNumber", "StaffDbMStaffId" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250428092945_miInitial.cs
                    { new Guid("3cff38a9-bd45-48f5-af5d-f6d84ca9cec1"), "Jane", null, "Smith", "19610228-1212", null },
                    { new Guid("41b223c5-ce01-4f8b-8fd0-9868bff1bec8"), "John", null, "Doe", "19480516-2222", null },
                    { new Guid("7ca661b7-9abd-4471-8a83-9f0aaef31e73"), "Bob", null, "Brown", "19501110-1331", null },
                    { new Guid("8b54023e-13bb-463a-8d0d-2742b39a3fd1"), "Madi", null, "Alabama", "19560831-1111", null },
                    { new Guid("d566d722-0d1a-4913-a544-7d3c3e38269d"), "Charlie", null, "Davis", "19511231-16181", null },
                    { new Guid("dfde9665-8ee9-492c-9afe-5b193382dac5"), "Alice", null, "Johnson", "19450801-4444", null }
========
                    { new Guid("1f5d21d8-e9eb-4a7a-93ad-03080d8936a0"), "Madi", null, "Alabama", "19560831-1111", null },
                    { new Guid("2f206bdf-b54b-4799-a3dd-42cf3aac0981"), "Jane", null, "Smith", "19610228-1212", null },
                    { new Guid("a4239d9b-9e6b-442d-b287-e9c9e5073877"), "Charlie", null, "Davis", "19511231-16181", null },
                    { new Guid("bc12c446-43d0-45fe-8c47-3fc27fc3c082"), "John", null, "Doe", "19480516-2222", null },
                    { new Guid("c7b59ff5-191b-4fcd-8250-e19b7f422fc4"), "Alice", null, "Johnson", "19450801-4444", null },
                    { new Guid("c90fe0a2-c90d-472a-a126-dc19c6a51251"), "Bob", null, "Brown", "19501110-1331", null }
>>>>>>>> 33c538c5673b6d7bc6542909128a9c0fc37d9e33:DbContext/Migrations/SqlServerDbContext/20250428093148_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "SleepLevels",
                columns: new[] { "SleepLevelId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250428092945_miInitial.cs
                    { new Guid("1dd66f87-640f-4ea9-bd19-a4f290e7a9e6"), "Low Sleep Level 🙁", "Low", null, 5 },
                    { new Guid("53b01a98-d37b-48a2-921c-51c647c70a61"), "OK Sleep Level 🙂", "OK", null, 8 },
                    { new Guid("84a1df17-eeef-484b-96f3-a4c0e7526d6b"), "Too much Sleep Level 😃", "Too much", null, 10 },
                    { new Guid("e20e4040-9a0f-4a42-89fd-b2ea668da682"), "Medium Sleep Level 😐", "Medium", null, 6 }
========
                    { new Guid("02faffd2-f1fa-49a6-9312-d205cf2259a6"), "Medium Sleep Level 😐", "Medium", null, 6 },
                    { new Guid("690e5bba-8fb8-4343-8684-94ad5f16018b"), "Too much Sleep Level 😃", "Too much", null, 10 },
                    { new Guid("80b9b73e-c122-4d0c-9f78-68006277de47"), "OK Sleep Level 🙂", "OK", null, 8 },
                    { new Guid("f4906fe7-3608-491f-91aa-1dc272b0e7f7"), "Low Sleep Level 🙁", "Low", null, 5 }
>>>>>>>> 33c538c5673b6d7bc6542909128a9c0fc37d9e33:DbContext/Migrations/SqlServerDbContext/20250428093148_miInitial.cs
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ActivityLevelDbMActivityLevelId",
                schema: "supusr",
                table: "Activities",
                column: "ActivityLevelDbMActivityLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_PatientDbMPatientId",
                schema: "supusr",
                table: "Activities",
                column: "PatientDbMPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityLevels_PatientDbMPatientId",
                schema: "supusr",
                table: "ActivityLevels",
                column: "PatientDbMPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AppetiteLevels_PatientDbMPatientId",
                schema: "supusr",
                table: "AppetiteLevels",
                column: "PatientDbMPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appetites_AppetiteLevelDbMAppetiteLevelId",
                schema: "supusr",
                table: "Appetites",
                column: "AppetiteLevelDbMAppetiteLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Appetites_PatientDbMPatientId",
                schema: "supusr",
                table: "Appetites",
                column: "PatientDbMPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_MoodKinds_PatientDbMPatientId",
                schema: "supusr",
                table: "MoodKinds",
                column: "PatientDbMPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Moods_MoodKindDbMMoodKindId",
                schema: "supusr",
                table: "Moods",
                column: "MoodKindDbMMoodKindId");

            migrationBuilder.CreateIndex(
                name: "IX_Moods_PatientDbMPatientId",
                schema: "supusr",
                table: "Moods",
                column: "PatientDbMPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_GraphId",
                schema: "supusr",
                table: "Patients",
                column: "GraphId",
                unique: true,
                filter: "[GraphId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_StaffDbMStaffId",
                schema: "supusr",
                table: "Patients",
                column: "StaffDbMStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_SleepLevels_PatientDbMPatientId",
                schema: "supusr",
                table: "SleepLevels",
                column: "PatientDbMPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sleeps_PatientDbMPatientId",
                schema: "supusr",
                table: "Sleeps",
                column: "PatientDbMPatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Sleeps_SleepLevelDbMSleepLevelId",
                schema: "supusr",
                table: "Sleeps",
                column: "SleepLevelDbMSleepLevelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Appetites",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Moods",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Sleeps",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Users",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ActivityLevels",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "AppetiteLevels",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "MoodKinds",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "SleepLevels",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Patients",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Graphs",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Staffs",
                schema: "supusr");
        }
    }
}
