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
                    { new Guid("137573c1-7167-4aa7-bb54-081c50bf740e"), "Swimming 🏊‍♂️", "Swimming", null, 7 },
                    { new Guid("2e527e31-6b74-4159-aa5a-a09337ec9369"), "Training 🏋️‍♂️", "Training", null, 9 },
                    { new Guid("7f46d164-d6d1-4b40-9a5d-261ffd3d0b53"), "Jogging 🏃‍♂️", "Jogging", null, 10 },
                    { new Guid("d2bb159e-24d5-4f9d-b1f3-a468237a4d97"), "Resting 🛌", "Resting", null, 1 },
                    { new Guid("ebe2de75-a469-40ab-bff4-f1c9d9483d4c"), "Take a Walk 🚶‍♂️", "Take a Walk", null, 5 },
                    { new Guid("efb7eeb9-c44e-4f94-bbad-073ef8b67bf2"), "Reading 📖", "Reading", null, 3 }
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "AppetiteLevels",
                columns: new[] { "AppetiteLevelId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
                    { new Guid("04361364-6838-4f97-9067-34d984845c53"), "Very Much 🍴", "Very Much", null, 10 },
                    { new Guid("0dd5ce4e-4b72-432f-9f64-44e38a461553"), "Didn't Eat At All 🤢", "Didn't Eat At All", null, 1 },
                    { new Guid("0e5ba2ab-c5d8-41ff-9a8a-6a9bd44752e4"), "Normal Appetite 🙂", "Normal", null, 5 },
                    { new Guid("91a2e6b9-162d-405f-872e-0496fe00b8ca"), "Little 🍽️", "Little", null, 3 },
                    { new Guid("c8e257be-1162-4df8-a60e-b6d43516c1ec"), "Medium 😋", "Medium", null, 7 }
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "MoodKinds",
                columns: new[] { "MoodKindId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
                    { new Guid("31345404-62d3-43c9-8281-bf6e07d651fe"), "Angry 😡", "Angry", null, 3 },
                    { new Guid("5aa612ed-8f6c-4cf4-8bb9-defd92f6571f"), "Bored 😒", "Bored", null, 4 },
                    { new Guid("7ac48910-7e40-4414-bec6-3a43b7696b2b"), "Lovely 😍", "Lovely", null, 7 },
                    { new Guid("81b5794f-8100-415e-b959-d30c68b4b1df"), "Depressed 😢", "Depressed", null, 1 },
                    { new Guid("d70d063d-dcef-448a-b86a-1be47fcc0b1a"), "Happy 😃", "Happy", null, 10 },
                    { new Guid("e55f0fb4-7a42-4256-9645-ed2fd4f25e0a"), "Excited 🤩", "Excited", null, 9 },
                    { new Guid("fc42bff8-2c9d-492c-bc53-6613b102dc2a"), "Sad 🙁", "Sad", null, 2 }
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "Patients",
                columns: new[] { "PatientId", "FirstName", "GraphId", "LastName", "PersonalNumber", "StaffDbMStaffId" },
                values: new object[,]
                {
                    { new Guid("1f9dfe00-d4ce-4faa-aad7-1339a400dd95"), "Alice", null, "Johnson", "19450801-4444", null },
                    { new Guid("336add03-5500-410a-8149-921cb04b907d"), "Charlie", null, "Davis", "19511231-16181", null },
                    { new Guid("38ce5816-b772-47a1-9dc9-fde96d711564"), "Bob", null, "Brown", "19501110-1331", null },
                    { new Guid("484dd015-d524-4d04-bfa5-15bc98551377"), "Jane", null, "Smith", "19610228-1212", null },
                    { new Guid("9df2a065-dde8-49fe-891a-dbade9a6d75a"), "Madi", null, "Alabama", "19560831-1111", null },
                    { new Guid("f3a15818-cee8-4b1c-9ef7-0d9cf6bd1807"), "John", null, "Doe", "19480516-2222", null }
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "SleepLevels",
                columns: new[] { "SleepLevelId", "Label", "Name", "PatientDbMPatientId", "Rating" },
                values: new object[,]
                {
                    { new Guid("1b0b987d-d4f6-436e-8fd1-105f8003826a"), "Medium Sleep Level 😐", "Medium", null, 6 },
                    { new Guid("2dedc702-494f-4ec9-889e-6a8d383e33a5"), "OK Sleep Level 🙂", "OK", null, 8 },
                    { new Guid("5f511d2c-689a-4722-a86a-4f88b4c4a4f6"), "Low Sleep Level 🙁", "Low", null, 5 },
                    { new Guid("8204c20b-b98f-40f7-b1dc-72d559f18a6a"), "Too much Sleep Level 😃", "Too much", null, 10 }
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
