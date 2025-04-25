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
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250425125838_miInitial.cs
                    { new Guid("20aafd06-abe6-40f3-8d4d-837595165536"), "Reading 📖", "Reading", null, 3 },
                    { new Guid("421c0dda-53f3-4326-90ec-1e2704feadf9"), "Swimming 🏊‍♂️", "Swimming", null, 7 },
                    { new Guid("5fba8640-2d9c-4ca0-98d2-e4865477f319"), "Jogging 🏃‍♂️", "Jogging", null, 10 },
                    { new Guid("7afe91db-8ebf-41ed-8a08-638da4614a07"), "Resting 🛌", "Resting", null, 1 },
                    { new Guid("ab243f87-6123-41ab-8a7b-5104f472e120"), "Training 🏋️‍♂️", "Training", null, 9 },
                    { new Guid("f088da8a-9f1d-464f-a733-e71f64050f00"), "Take a Walk 🚶‍♂️", "Take a Walk", null, 5 }
========
                    { new Guid("1008db17-22ad-4b1b-9e42-0874cfd4e2c8"), "Reading 📖", "Reading", null, 3 },
                    { new Guid("166ae686-49dc-40fa-90ed-18d23ca2b413"), "Jogging 🏃‍♂️", "Jogging", null, 10 },
                    { new Guid("310f8792-3706-4973-a5df-b60da3ed81e7"), "Swimming 🏊‍♂️", "Swimming", null, 7 },
                    { new Guid("9d891e97-ff1c-419f-bcfc-6d344afef121"), "Take a Walk 🚶‍♂️", "Take a Walk", null, 5 },
                    { new Guid("ad3dd9a5-64ce-46f9-827f-914cdecb6358"), "Training 🏋️‍♂️", "Training", null, 9 },
                    { new Guid("b2b42dc7-9009-4a32-96fc-c7db645b2c8b"), "Resting 🛌", "Resting", null, 1 }
>>>>>>>> main:DbContext/Migrations/SqlServerDbContext/20250425121319_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "AppetiteLevels",
                columns: new[] { "AppetiteLevelId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250425125838_miInitial.cs
                    { new Guid("21fbfa46-7f8e-488d-afd0-94682ffc6491"), "Little 🍽️", "Little", null, 3 },
                    { new Guid("34415b2f-e435-4c5e-b4c7-0a9d9dbae1be"), "Didn't Eat At All 🤢", "Didn't Eat At All", null, 1 },
                    { new Guid("68d91aab-81fd-4cc9-bd55-1a4393899654"), "Very Much 🍴", "Very Much", null, 10 },
                    { new Guid("b47986f7-3de7-472b-8298-5a5b3992df35"), "Normal Appetite 🙂", "Normal", null, 5 },
                    { new Guid("cf9c1611-607e-4108-a9f9-6b4a163f442f"), "Medium 😋", "Medium", null, 7 }
========
                    { new Guid("360ca274-46ef-4781-b693-49fa80abe255"), "Didn't Eat At All 🤢", "Didn't Eat At All", null, 1 },
                    { new Guid("539cd269-b262-413d-a9b4-e5588e1d65ac"), "Normal Appetite 🙂", "Normal", null, 5 },
                    { new Guid("bd8659af-87d1-44e5-a958-d8d835ca847b"), "Medium 😋", "Medium", null, 7 },
                    { new Guid("cc1ed023-8350-4d3f-bb9c-79b3e9b95d63"), "Very Much 🍴", "Very Much", null, 10 },
                    { new Guid("e349ecf6-8ca5-4e35-84e2-2179d3c1b9ae"), "Little 🍽️", "Little", null, 3 }
>>>>>>>> main:DbContext/Migrations/SqlServerDbContext/20250425121319_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "MoodKinds",
                columns: new[] { "MoodKindId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250425125838_miInitial.cs
                    { new Guid("0dc9d577-9522-41b6-934f-f06f63530eb5"), "Angry 😡", "Angry", null, 3 },
                    { new Guid("246659c7-f485-4694-9779-47d40b9bd93d"), "Lovely 😍", "Lovely", null, 7 },
                    { new Guid("2a1cd39e-f513-4d1b-81e3-3d10a7ee57f9"), "Depressed 😢", "Depressed", null, 1 },
                    { new Guid("ad8b1149-b15a-449f-84e5-7dcc8fe039fd"), "Bored 😒", "Bored", null, 4 },
                    { new Guid("bc1528c7-2a58-4d4c-b5c3-99289d5a331a"), "Happy 😃", "Happy", null, 10 },
                    { new Guid("e2f39d61-e6de-4af8-b0a6-9696a89bb67b"), "Sad 🙁", "Sad", null, 2 },
                    { new Guid("f2e16e77-7192-4d0a-bc40-5f64bf6efcf3"), "Excited 🤩", "Excited", null, 9 }
========
                    { new Guid("490469f2-2f5d-4889-a463-2a1325be8b0f"), "Lovely 😍", "Lovely", null, 7 },
                    { new Guid("536edeeb-d52f-45f3-aa61-86296641b2ff"), "Excited 🤩", "Excited", null, 9 },
                    { new Guid("8080f81d-d6e4-4ea7-bed7-21774078338b"), "Happy 😃", "Happy", null, 10 },
                    { new Guid("b65eeb6e-21eb-4c8f-a733-c6f9bf1f5f78"), "Bored 😒", "Bored", null, 4 },
                    { new Guid("ca30b352-d2c3-41b1-9f16-773a6ff75c93"), "Angry 😡", "Angry", null, 3 },
                    { new Guid("e5261c43-8c2d-48a4-b6eb-b84cd8a1aeaf"), "Depressed 😢", "Depressed", null, 1 },
                    { new Guid("fda7b84e-d289-413b-9ff6-3b4f7899b20b"), "Sad 🙁", "Sad", null, 2 }
>>>>>>>> main:DbContext/Migrations/SqlServerDbContext/20250425121319_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "Patients",
                columns: new[] { "PatientId", "FirstName", "GraphId", "LastName", "PersonalNumber", "StaffDbMStaffId" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250425125838_miInitial.cs
                    { new Guid("07b881f0-4b8f-4eee-a2dc-89e55ffaca52"), "Alice", null, "Johnson", "19450801-4444", null },
                    { new Guid("460e36a0-5124-4dd6-bb54-a269ed6f77b7"), "Jane", null, "Smith", "19610228-1212", null },
                    { new Guid("46481e46-7d32-4ede-a4b3-ad594b30f4aa"), "Charlie", null, "Davis", "19511231-16181", null },
                    { new Guid("48eb7bdb-c2e9-47de-99e5-d8e72b526e36"), "Madi", null, "Alabama", "19560831-1111", null },
                    { new Guid("bc3cccad-bf72-4703-a26c-f47bd0dde673"), "Bob", null, "Brown", "19501110-1331", null },
                    { new Guid("fa55cb17-1dc8-4abb-997e-ae79f85bdbf6"), "John", null, "Doe", "19480516-2222", null }
========
                    { new Guid("1abdf2e0-86f1-4e71-9520-86c64e34c8e9"), "Jane", null, "Smith", "19610228-1212", null },
                    { new Guid("1b6eb373-b284-41d8-8d1f-ffd950aa77b3"), "Bob", null, "Brown", "19501110-1331", null },
                    { new Guid("69cff37c-33d4-4a6f-99d2-ea94598982f2"), "John", null, "Doe", "19480516-2222", null },
                    { new Guid("75863f8d-e5b3-498a-8ab7-ff0c4425cf49"), "Charlie", null, "Davis", "19511231-16181", null },
                    { new Guid("7e0e5929-cea5-4ec9-bb70-4b17fbf7fb01"), "Alice", null, "Johnson", "19450801-4444", null },
                    { new Guid("88f0635b-0774-4ea9-bff9-6477cf4a8bbb"), "Madi", null, "Alabama", "19560831-1111", null }
>>>>>>>> main:DbContext/Migrations/SqlServerDbContext/20250425121319_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "SleepLevels",
                columns: new[] { "SleepLevelId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250425125838_miInitial.cs
                    { new Guid("2b9cdaf5-abe6-4e6c-bdb4-e53b4ed916de"), "Too much Sleep Level 😃", "Too much", null, 10 },
                    { new Guid("3a008486-0120-4c51-89a1-215abf90dbd0"), "OK Sleep Level 🙂", "OK", null, 8 },
                    { new Guid("821d9094-0eb4-4c54-9693-1ba7dbbd793c"), "Low Sleep Level 🙁", "Low", null, 5 },
                    { new Guid("f16c48f9-5229-4a27-9910-bbf770aea592"), "Medium Sleep Level 😐", "Medium", null, 6 }
========
                    { new Guid("500bca2f-2a15-48b6-aa8f-b8ea0054e0ba"), "Low Sleep Level 🙁", "Low", null, 5 },
                    { new Guid("5ccc70d1-6884-47e5-8d57-7ec21963a53b"), "Too much Sleep Level 😃", "Too much", null, 10 },
                    { new Guid("a29b6d21-f356-42da-8cd7-91a1658aba2d"), "Medium Sleep Level 😐", "Medium", null, 6 },
                    { new Guid("a9531344-4de0-422b-94a2-c9b4a6758a7e"), "OK Sleep Level 🙂", "OK", null, 8 }
>>>>>>>> main:DbContext/Migrations/SqlServerDbContext/20250425121319_miInitial.cs
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
