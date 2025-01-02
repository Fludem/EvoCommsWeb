using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EvoCommsWeb.WebPanel.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployeeAndRelatedModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClockingId = table.Column<int>(type: "INTEGER", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terminals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SerialNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DeviceModel = table.Column<int>(type: "INTEGER", nullable: false),
                    LastConnected = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clockings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ClockedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    TerminalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clockings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clockings_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clockings_Terminals_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TemplateData = table.Column<string>(type: "TEXT", nullable: false),
                    DeviceModel = table.Column<int>(type: "INTEGER", nullable: false),
                    DateDownloaded = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReceivedFromId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeTemplates_Terminals_ReceivedFromId",
                        column: x => x.ReceivedFromId,
                        principalTable: "Terminals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clockings_EmployeeId",
                table: "Clockings",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clockings_TerminalId",
                table: "Clockings",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTemplates_ReceivedFromId",
                table: "EmployeeTemplates",
                column: "ReceivedFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminals_SerialNumber",
                table: "Terminals",
                column: "SerialNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Clockings");

            migrationBuilder.DropTable(
                name: "EmployeeTemplates");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Terminals");
        }
    }
}
