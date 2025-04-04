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
                name: "ActivityLevels",
                schema: "supusr",
                columns: table => new
                {
                    ActivityLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLevels", x => x.ActivityLevelId);
                });

            migrationBuilder.CreateTable(
                name: "AppetiteLevels",
                schema: "supusr",
                columns: table => new
                {
                    AppetiteLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppetiteLevels", x => x.AppetiteLevelId);
                });

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
                name: "MoodKinds",
                schema: "supusr",
                columns: table => new
                {
                    MoodKindId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoodKinds", x => x.MoodKindId);
                });

            migrationBuilder.CreateTable(
                name: "SleepLevels",
                schema: "supusr",
                columns: table => new
                {
                    SleepLevelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Label = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SleepLevels", x => x.SleepLevelId);
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
                columns: new[] { "ActivityLevelId", "Label", "Name", "Rating" },
                values: new object[,]
                {
                    { new Guid("26b1c18f-bb3d-4ca3-8e0e-c550b3b03539"), "Medium Activity Level 🏃‍♂️", "Medium", 5 },
                    { new Guid("2e79ea14-e45c-46fb-9030-07d8a7686470"), "High Activity Level 🏋️‍♂️", "High", 7 },
                    { new Guid("5de07224-5432-47ae-bc41-2a043da9e8ad"), "Low Activity Level 🚶‍♂️", "Low", 3 },
                    { new Guid("6ea213c0-c88c-4680-93ff-49ff5871ba29"), "Very Low Activity Level 🛌", "Very Low", 1 },
                    { new Guid("af4a4ae7-6808-4e0c-afd8-a98f81b368d0"), "Very High Activity Level 🏆", "Very High", 10 }
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "AppetiteLevels",
                columns: new[] { "AppetiteLevelId", "Label", "Name", "Rating" },
                values: new object[,]
                {
                    { new Guid("47f57c33-61df-41d5-b2cc-1792bcb2d571"), "Normal Appetite 🙂", "Normal", 5 },
                    { new Guid("55bba8ce-347a-4bf7-85fe-3c0f45ca4f35"), "Little 🍽️", "Little", 3 },
                    { new Guid("906a24b4-3234-4c57-9aea-d0a9f3ecd27e"), "Very Much 🍴", "Very Much", 10 },
                    { new Guid("a33fdd6c-ca91-47c1-b781-1fc2a7010ddf"), "Didn't Eat At All 🤢", "Didn't Eat At All", 1 },
                    { new Guid("ad256cd8-b83d-42a2-8a93-d64de710e259"), "Medium 😋", "Medium", 7 }
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "MoodKinds",
                columns: new[] { "MoodKindId", "Label", "Name", "Rating" },
                values: new object[,]
                {
                    { new Guid("1e556430-3e79-4ebe-b4eb-6446037b25a9"), "Very High Mood Level 😃", "Very High", 10 },
                    { new Guid("92781c24-9df1-411b-842f-73dd4f6b2ee4"), "High Mood Level 🙂", "High", 7 },
                    { new Guid("9d4bd287-4d25-468b-a8ee-7f545d95b2b3"), "Low Mood Level 🙁", "Low", 3 },
                    { new Guid("b21f8f13-bc38-44ea-ad94-e7c8dc7c594a"), "Very Low Mood Level 😞", "Very Low", 1 },
                    { new Guid("ba86e8bd-a544-4c1e-83c9-f42971615a05"), "Medium Mood Level 😐", "Medium", 5 }
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "Patients",
                columns: new[] { "PatientId", "FirstName", "GraphId", "LastName", "PersonalNumber", "StaffDbMStaffId" },
                values: new object[,]
                {
                    { new Guid("4404e802-bb6c-4d76-8db5-c040087ec298"), "Charlie", null, "Davis", "19511231-16181", null },
                    { new Guid("4f5d9636-fbe5-4d7d-bf3f-b5f0f4c7ff6e"), "Jane", null, "Smith", "19610228-1212", null },
                    { new Guid("522c0259-1e95-4269-bc9f-72021582313e"), "John", null, "Doe", "19480516-2222", null },
                    { new Guid("cb73d3ac-e8c9-4bd7-be83-7c18195f600a"), "Alice", null, "Johnson", "19450801-4444", null },
                    { new Guid("d46870d6-0d00-4587-bff6-f46ca07fad55"), "Madi", null, "Alabama", "19560831-1111", null },
                    { new Guid("fb69b799-ec0b-4f73-be09-3b43458d5672"), "Bob", null, "Brown", "19501110-1331", null }
                });

            migrationBuilder.InsertData(
                schema: "supusr",
                table: "Staffs",
                columns: new[] { "StaffId", "FirstName", "LastName", "PersonalNumber" },
                values: new object[,]
                {
                    { new Guid("0fc08fe5-2bd1-4fee-8f43-e78739ed367c"), "John", "Doe", "19900516-2222" },
                    { new Guid("3aadbd49-3d73-4445-885f-a6a46f56a3ff"), "Alice", "Johnson", "19931001-4444" },
                    { new Guid("6b81166d-b4cc-468c-b4b3-b4ce17df377a"), "Jane", "Smith", "19610228-1212" },
                    { new Guid("6ba2566d-1e6d-40be-83ca-da396eb957d6"), "Madi", "Alabama", "19800613-1111" },
                    { new Guid("7cc4c70c-af96-4621-b17e-f5bcc5da6d03"), "Moris", "Andre", "19750105-1111" }
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
                name: "Patients",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "SleepLevels",
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
