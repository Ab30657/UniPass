using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations.SqliteMigrations
{
    public partial class ChangeCoursePIfromTakesPItoPI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakesCoursePIs_CoursePIs_TakesId",
                table: "TakesCoursePIs");

            migrationBuilder.RenameColumn(
                name: "CoursePIId",
                table: "TakesCoursePIs",
                newName: "PerformanceIndicatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCoursePIs_PerformanceIndicators_TakesId",
                table: "TakesCoursePIs",
                column: "TakesId",
                principalTable: "PerformanceIndicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakesCoursePIs_PerformanceIndicators_TakesId",
                table: "TakesCoursePIs");

            migrationBuilder.RenameColumn(
                name: "PerformanceIndicatorId",
                table: "TakesCoursePIs",
                newName: "CoursePIId");

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCoursePIs_CoursePIs_TakesId",
                table: "TakesCoursePIs",
                column: "TakesId",
                principalTable: "CoursePIs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
