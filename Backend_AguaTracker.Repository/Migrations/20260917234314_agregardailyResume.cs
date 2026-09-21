using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_AguaTracker.Repository.Migrations
{
    /// <inheritdoc />
    public partial class agregardailyResume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyResume_Users_UserId",
                table: "DailyResume");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterIntakes_DailyResume_DailyResumenId",
                table: "WaterIntakes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyResume",
                table: "DailyResume");

            migrationBuilder.RenameTable(
                name: "DailyResume",
                newName: "DailyResumes");

            migrationBuilder.RenameIndex(
                name: "IX_DailyResume_UserId",
                table: "DailyResumes",
                newName: "IX_DailyResumes_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyResumes",
                table: "DailyResumes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyResumes_Users_UserId",
                table: "DailyResumes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterIntakes_DailyResumes_DailyResumenId",
                table: "WaterIntakes",
                column: "DailyResumenId",
                principalTable: "DailyResumes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyResumes_Users_UserId",
                table: "DailyResumes");

            migrationBuilder.DropForeignKey(
                name: "FK_WaterIntakes_DailyResumes_DailyResumenId",
                table: "WaterIntakes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DailyResumes",
                table: "DailyResumes");

            migrationBuilder.RenameTable(
                name: "DailyResumes",
                newName: "DailyResume");

            migrationBuilder.RenameIndex(
                name: "IX_DailyResumes_UserId",
                table: "DailyResume",
                newName: "IX_DailyResume_UserId");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Name",
                keyValue: null,
                column: "Name",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Users",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DailyResume",
                table: "DailyResume",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyResume_Users_UserId",
                table: "DailyResume",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WaterIntakes_DailyResume_DailyResumenId",
                table: "WaterIntakes",
                column: "DailyResumenId",
                principalTable: "DailyResume",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
