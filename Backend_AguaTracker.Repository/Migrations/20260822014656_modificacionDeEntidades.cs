using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_AguaTracker.Repository.Migrations
{
    /// <inheritdoc />
    public partial class modificacionDeEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterIntakes_DailyResume_dailyResumenId",
                table: "WaterIntakes");

            migrationBuilder.RenameColumn(
                name: "dailyResumenId",
                table: "WaterIntakes",
                newName: "DailyResumenId");

            migrationBuilder.RenameIndex(
                name: "IX_WaterIntakes_dailyResumenId",
                table: "WaterIntakes",
                newName: "IX_WaterIntakes_DailyResumenId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterIntakes_DailyResume_DailyResumenId",
                table: "WaterIntakes",
                column: "DailyResumenId",
                principalTable: "DailyResume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterIntakes_DailyResume_DailyResumenId",
                table: "WaterIntakes");

            migrationBuilder.RenameColumn(
                name: "DailyResumenId",
                table: "WaterIntakes",
                newName: "dailyResumenId");

            migrationBuilder.RenameIndex(
                name: "IX_WaterIntakes_DailyResumenId",
                table: "WaterIntakes",
                newName: "IX_WaterIntakes_dailyResumenId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterIntakes_DailyResume_dailyResumenId",
                table: "WaterIntakes",
                column: "dailyResumenId",
                principalTable: "DailyResume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
