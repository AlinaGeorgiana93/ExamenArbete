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
                    PersonalNumber = table.Column<string>(type: "nvarchar(200)", nullable: true)
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
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250423181259_miInitial.cs
                    { new Guid("04e9aca4-c07b-41a0-9bbb-cb73fa445eca"), "Reading 📖", "Reading", null, 3 },
                    { new Guid("45eefd8e-4e6f-4a04-9324-0735f65fadd3"), "Swimming 🏊‍♂️", "Swimming", null, 7 },
                    { new Guid("4b3369d6-0d61-4291-a881-abfc029ce2f1"), "Jogging 🏃‍♂️", "Jogging", null, 10 },
                    { new Guid("609c6682-64b0-4879-ae26-01f2674d9d00"), "Training 🏋️‍♂️", "Training", null, 9 },
                    { new Guid("803b7531-4896-4bd1-9668-b1f052a3575e"), "Resting 🛌", "Resting", null, 1 },
                    { new Guid("e93a5173-9234-4ce0-a3d3-1f2f163f0968"), "Take a Walk 🚶‍♂️", "Take a Walk", null, 5 }
========
                    { new Guid("05fe1460-90e5-49f0-8b0c-7ecf27d1b9b1"), "Jogging 🏃‍♂️", "Jogging", 10 },
                    { new Guid("0e97df90-5d21-436b-86a2-8f48bb79c7fc"), "Training 🏋️‍♂️", "Training", 9 },
                    { new Guid("1867ef24-a234-434a-83a1-587b4020265c"), "Reading 📖", "Reading", 3 },
                    { new Guid("62545164-4872-4fc1-ba92-c47d660bc3a8"), "Take a Walk 🚶‍♂️", "Take a Walk", 5 },
                    { new Guid("65b0bff4-8825-4de9-b63a-86902a53b953"), "Resting 🛌", "Resting", 1 },
                    { new Guid("807a8f79-1621-476a-b70f-3b10199daf9d"), "Swimming 🏊‍♂️", "Swimming", 7 }
>>>>>>>> Userlogin-error-resolve:DbContext/Migrations/SqlServerDbContext/20250424082640_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "AppetiteLevels",
                columns: new[] { "AppetiteLevelId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250423181259_miInitial.cs
                    { new Guid("0b69eaa0-47cb-4e02-a2c6-f12837ee5664"), "Didn't Eat At All 🤢", "Didn't Eat At All", null, 1 },
                    { new Guid("56e3b4e0-22b2-4992-ae59-40e3d2b526be"), "Normal Appetite 🙂", "Normal", null, 5 },
                    { new Guid("577348d7-3039-42ae-8f55-44cc61832914"), "Very Much 🍴", "Very Much", null, 10 },
                    { new Guid("9a11d776-8133-49b0-96c3-c82ae3f7f5d5"), "Little 🍽️", "Little", null, 3 },
                    { new Guid("ddc09840-b31a-48f2-8ab4-0e1499abb54b"), "Medium 😋", "Medium", null, 7 }
========
                    { new Guid("8532b8f7-73b7-4e19-a448-b3295334d7a4"), "Very Much 🍴", "Very Much", 10 },
                    { new Guid("8ae632ad-d731-47cd-ae0a-ec511b570df8"), "Little 🍽️", "Little", 3 },
                    { new Guid("a18c9346-eab8-4070-8585-bf8053ec1976"), "Medium 😋", "Medium", 7 },
                    { new Guid("bd3294be-4466-4d26-a8da-2df953100ba1"), "Normal Appetite 🙂", "Normal", 5 },
                    { new Guid("ff394870-be8d-448a-b6a3-5038360d5977"), "Didn't Eat At All 🤢", "Didn't Eat At All", 1 }
>>>>>>>> Userlogin-error-resolve:DbContext/Migrations/SqlServerDbContext/20250424082640_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "MoodKinds",
                columns: new[] { "MoodKindId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250423181259_miInitial.cs
                    { new Guid("26e8e141-a1f4-41ca-b9fc-05cf8cbe0616"), "Angry 😡", "Angry", null, 3 },
                    { new Guid("68c8f4d3-cb65-40de-9a0c-2ce3573d3d11"), "Excited 🤩", "Excited", null, 9 },
                    { new Guid("a622103f-0771-4196-9d2c-96e6323b8409"), "Bored 😒", "Bored", null, 4 },
                    { new Guid("c12f55f5-ea19-42a2-948e-5f267bbf741f"), "Sad 🙁", "Sad", null, 2 },
                    { new Guid("c8ad9ef3-21a1-40b6-bbf2-cf23cab1e5f7"), "Happy 😃", "Happy", null, 10 },
                    { new Guid("d1a6d258-c906-4336-bdb9-077d06604741"), "Lovely 😍", "Lovely", null, 7 },
                    { new Guid("faea145e-01bf-4b58-af08-30c940c4c905"), "Depressed 😢", "Depressed", null, 1 }
========
                    { new Guid("32f5ddf5-275b-4723-b344-e0a941eae8f8"), "Bored 😒", "Bored", 4 },
                    { new Guid("4525d82e-ff94-4a2c-bcfe-4b19a3d65d05"), "Lovely 😍", "Lovely", 7 },
                    { new Guid("49eba48c-c9b6-401e-9cdc-9b755bc51b10"), "Angry 😡", "Angry", 3 },
                    { new Guid("61967eb2-e900-46bd-9150-c718fddf011e"), "Sad 🙁", "Sad", 2 },
                    { new Guid("644ca500-7847-4dc4-8dfb-db99157f8194"), "Happy 😃", "Happy", 10 },
                    { new Guid("c35db38a-a179-4bb7-aa53-e39f12bbd3ec"), "Depressed 😢", "Depressed", 1 },
                    { new Guid("eab15868-aa6b-4dbd-91cc-fdc9d6bcfe6c"), "Excited 🤩", "Excited", 9 }
>>>>>>>> Userlogin-error-resolve:DbContext/Migrations/SqlServerDbContext/20250424082640_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "Patients",
                columns: new[] { "PatientId", "FirstName", "GraphId", "LastName", "PersonalNumber", "StaffDbMStaffId" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250423181259_miInitial.cs
                    { new Guid("04cd3287-2b4a-4392-bebb-75858184394c"), "Madi", null, "Alabama", "19560831-1111", null },
                    { new Guid("0eb70961-b98d-4093-9068-2e47e75360fb"), "Alice", null, "Johnson", "19450801-4444", null },
                    { new Guid("55c3ce79-d30a-4b9f-93ce-0612a389e8f5"), "John", null, "Doe", "19480516-2222", null },
                    { new Guid("622ed5fd-fceb-433c-ba0f-a08861de4c56"), "Bob", null, "Brown", "19501110-1331", null },
                    { new Guid("a7e34c59-559b-4904-b507-a4c1f85bb690"), "Jane", null, "Smith", "19610228-1212", null },
                    { new Guid("f8deebe1-b069-4b4e-b122-64e9d58d836e"), "Charlie", null, "Davis", "19511231-16181", null }
========
                    { new Guid("047d0804-3bcd-4c60-b862-ba69266c83ee"), "Charlie", null, "Davis", "19511231-16181", null },
                    { new Guid("061153fc-78d9-409f-8662-25f162bd2bcc"), "Bob", null, "Brown", "19501110-1331", null },
                    { new Guid("66306187-c844-4a1d-acdd-352a36d268d5"), "Alice", null, "Johnson", "19450801-4444", null },
                    { new Guid("ab83108c-808d-4a1a-9db6-9acf49ef2543"), "Jane", null, "Smith", "19610228-1212", null },
                    { new Guid("bfd07c5b-a54a-4eb9-a436-9be9547662c5"), "Madi", null, "Alabama", "19560831-1111", null },
                    { new Guid("ce776f78-3b7b-41bc-a008-8ab900d11c93"), "John", null, "Doe", "19480516-2222", null }
>>>>>>>> Userlogin-error-resolve:DbContext/Migrations/SqlServerDbContext/20250424082640_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "SleepLevels",
                columns: new[] { "SleepLevelId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250423181259_miInitial.cs
                    { new Guid("10cb79f5-c226-4be8-9438-ec39b2c52488"), "Too much Sleep Level 😃", "Too much", null, 10 },
                    { new Guid("152b08b0-6988-4978-8836-98164371a8a7"), "Low Sleep Level 🙁", "Low", null, 5 },
                    { new Guid("15a4cfe0-0bcd-4136-a0be-8338d6bf8f30"), "OK Sleep Level 🙂", "OK", null, 8 },
                    { new Guid("7381a972-bf07-41e7-ab2c-53ba1c86d100"), "Medium Sleep Level 😐", "Medium", null, 6 }
========
                    { new Guid("3eac0d4a-b287-496b-89ad-6123eaff68e5"), "Medium Sleep Level 😐", "Medium", 6 },
                    { new Guid("7490c8ca-cbdd-46c7-8457-fd398260688f"), "Too much Sleep Level 😃", "Too much", 10 },
                    { new Guid("a504581f-febc-4c3c-a792-328b8bd2b633"), "Low Sleep Level 🙁", "Low", 5 },
                    { new Guid("d1eb02f4-3943-496f-92be-7767b1183431"), "OK Sleep Level 🙂", "OK", 8 }
>>>>>>>> Userlogin-error-resolve:DbContext/Migrations/SqlServerDbContext/20250424082640_miInitial.cs
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "Staffs",
                columns: new[] { "StaffId", "FirstName", "LastName", "PersonalNumber" },
                values: new object[,]
                {
<<<<<<<< HEAD:DbContext/Migrations/SqlServerDbContext/20250423181259_miInitial.cs
                    { new Guid("0bc53a1c-e2fa-4907-b14b-9fb51f76188b"), "Jane", "Smith", "19610228-1212" },
                    { new Guid("21aa906d-05dd-4f59-a189-41890c85b735"), "Alice", "Johnson", "19931001-4444" },
                    { new Guid("222f78a9-e094-4296-861a-a08957ed03f7"), "Madi", "Alabama", "19800613-1111" },
                    { new Guid("272669c5-d8da-4fe9-b30e-6cb7112492f6"), "John", "Doe", "19900516-2222" },
                    { new Guid("847e9f16-37d6-4fcb-8e23-da6be2523134"), "Moris", "Andre", "19750105-1111" }
========
                    { new Guid("4529ab6c-b384-42ab-98ca-daecb9e62dc8"), "Madi", "Alabama", "19800613-1111" },
                    { new Guid("55d210a3-7de2-4821-bf3e-1df73c5d81d8"), "Jane", "Smith", "19610228-1212" },
                    { new Guid("803b3d5e-dc70-402c-87d5-cff15ab94dbe"), "Moris", "Andre", "19750105-1111" },
                    { new Guid("93fbe84f-5d85-489a-86f8-7324a8071398"), "John", "Doe", "19900516-2222" },
                    { new Guid("b9bcbe2c-bb5e-43ff-b5aa-30ece722be86"), "Alice", "Johnson", "19931001-4444" }
>>>>>>>> Userlogin-error-resolve:DbContext/Migrations/SqlServerDbContext/20250424082640_miInitial.cs
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
