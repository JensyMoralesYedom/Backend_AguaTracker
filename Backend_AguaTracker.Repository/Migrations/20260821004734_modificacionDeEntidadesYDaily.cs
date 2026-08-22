using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_AguaTracker.Repository.Migrations
{
    /// <inheritdoc />
    public partial class modificacionDeEntidadesYDaily : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterIntakes_Users_UserId",
                table: "WaterIntakes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "WaterIntakes");

            migrationBuilder.DropColumn(
                name: "Height",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "WaterIntakes",
                newName: "dailyResumenId");

            migrationBuilder.RenameIndex(
                name: "IX_WaterIntakes_UserId",
                table: "WaterIntakes",
                newName: "IX_WaterIntakes_dailyResumenId");

            migrationBuilder.AddColumn<int>(
                name: "DefaultActivityLevel",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DailyResume",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    TotalIntake = table.Column<int>(type: "int", nullable: false),
                    ActivityLevel = table.Column<int>(type: "int", nullable: false),
                    IntakeGoal = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyResume", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DailyResume_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DailyResume_UserId",
                table: "DailyResume",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterIntakes_DailyResume_dailyResumenId",
                table: "WaterIntakes",
                column: "dailyResumenId",
                principalTable: "DailyResume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterIntakes_DailyResume_dailyResumenId",
                table: "WaterIntakes");

            migrationBuilder.DropTable(
                name: "DailyResume");

            migrationBuilder.DropColumn(
                name: "DefaultActivityLevel",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "dailyResumenId",
                table: "WaterIntakes",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_WaterIntakes_dailyResumenId",
                table: "WaterIntakes",
                newName: "IX_WaterIntakes_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "WaterIntakes",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "Users",
                type: "decimal(65,30)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterIntakes_Users_UserId",
                table: "WaterIntakes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
