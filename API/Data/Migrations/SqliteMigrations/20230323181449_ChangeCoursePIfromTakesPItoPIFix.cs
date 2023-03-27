using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations.SqliteMigrations
{
    public partial class ChangeCoursePIfromTakesPItoPIFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakesCoursePIs_PerformanceIndicators_TakesId",
                table: "TakesCoursePIs");

            migrationBuilder.CreateIndex(
                name: "IX_TakesCoursePIs_PerformanceIndicatorId",
                table: "TakesCoursePIs",
                column: "PerformanceIndicatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCoursePIs_PerformanceIndicators_PerformanceIndicatorId",
                table: "TakesCoursePIs",
                column: "PerformanceIndicatorId",
                principalTable: "PerformanceIndicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakesCoursePIs_PerformanceIndicators_PerformanceIndicatorId",
                table: "TakesCoursePIs");

            migrationBuilder.DropIndex(
                name: "IX_TakesCoursePIs_PerformanceIndicatorId",
                table: "TakesCoursePIs");

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCoursePIs_PerformanceIndicators_TakesId",
                table: "TakesCoursePIs",
                column: "TakesId",
                principalTable: "PerformanceIndicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
