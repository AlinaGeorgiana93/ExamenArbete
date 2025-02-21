﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

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
                name: "Employees",
                schema: "supusr",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    strRole = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateTable(
                name: "staff",
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
                name: "Mood",
                schema: "supusr",
                columns: table => new
                {
                    ZooId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zoos", x => x.ZooId);
                });

            migrationBuilder.CreateTable(
                name: "",
                schema: "supusr",
                columns: table => new
                {
                    CreditCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    strIssuer = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Issuer = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Number = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ExpirationYear = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ExpirationMonth = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    EnryptedToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreditCards", x => x.CreditCardId);
                    table.ForeignKey(
                        name: "FK_CreditCards_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "supusr",
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                schema: "supusr",
                columns: table => new
                {
                    AnimalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    strKind = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    strMood = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    ZooDbMZooId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Kind = table.Column<int>(type: "int", nullable: false),
                    Mood = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", nullable: true),
                    Seeded = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.AnimalId);
                    table.ForeignKey(
                        name: "FK_Animals_Zoos_ZooDbMZooId",
                        column: x => x.ZooDbMZooId,
                        principalSchema: "supusr",
                        principalTable: "Zoos",
                        principalColumn: "ZooId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDbMZooDbM",
                schema: "supusr",
                columns: table => new
                {
                    EmployeesDbMEmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ZoosDbMZooId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDbMZooDbM", x => new { x.EmployeesDbMEmployeeId, x.ZoosDbMZooId });
                    table.ForeignKey(
                        name: "FK_EmployeeDbMZooDbM_Employees_EmployeesDbMEmployeeId",
                        column: x => x.EmployeesDbMEmployeeId,
                        principalSchema: "supusr",
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeDbMZooDbM_Zoos_ZoosDbMZooId",
                        column: x => x.ZoosDbMZooId,
                        principalSchema: "supusr",
                        principalTable: "Zoos",
                        principalColumn: "ZooId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_ZooDbMZooId",
                schema: "supusr",
                table: "Animals",
                column: "ZooDbMZooId");

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_EmployeeId",
                schema: "supusr",
                table: "CreditCards",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDbMZooDbM_ZoosDbMZooId",
                schema: "supusr",
                table: "EmployeeDbMZooDbM",
                column: "ZoosDbMZooId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropTable(
                name: "Activity",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Mood",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Sleep",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Staff",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Appetiet",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Graph",
                schema: "supusr");

            migrationBuilder.DropTable(
                name: "Patient",
                schema: "supusr");
        }
        

    }
}
