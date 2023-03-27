using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations.SqliteMigrations
{
    public partial class AddAssignmentPI2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentPI_Assignments_AssignmentId",
                table: "AssignmentPI");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentPI_PerformanceIndicators_PerformanceIndicatorId",
                table: "AssignmentPI");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentPI",
                table: "AssignmentPI");

            migrationBuilder.RenameTable(
                name: "AssignmentPI",
                newName: "AssignmentPIs");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentPI_PerformanceIndicatorId",
                table: "AssignmentPIs",
                newName: "IX_AssignmentPIs_PerformanceIndicatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentPIs",
                table: "AssignmentPIs",
                columns: new[] { "AssignmentId", "PerformanceIndicatorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentPIs_Assignments_AssignmentId",
                table: "AssignmentPIs",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentPIs_PerformanceIndicators_PerformanceIndicatorId",
                table: "AssignmentPIs",
                column: "PerformanceIndicatorId",
                principalTable: "PerformanceIndicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentPIs_Assignments_AssignmentId",
                table: "AssignmentPIs");

            migrationBuilder.DropForeignKey(
                name: "FK_AssignmentPIs_PerformanceIndicators_PerformanceIndicatorId",
                table: "AssignmentPIs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignmentPIs",
                table: "AssignmentPIs");

            migrationBuilder.RenameTable(
                name: "AssignmentPIs",
                newName: "AssignmentPI");

            migrationBuilder.RenameIndex(
                name: "IX_AssignmentPIs_PerformanceIndicatorId",
                table: "AssignmentPI",
                newName: "IX_AssignmentPI_PerformanceIndicatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignmentPI",
                table: "AssignmentPI",
                columns: new[] { "AssignmentId", "PerformanceIndicatorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentPI_Assignments_AssignmentId",
                table: "AssignmentPI",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AssignmentPI_PerformanceIndicators_PerformanceIndicatorId",
                table: "AssignmentPI",
                column: "PerformanceIndicatorId",
                principalTable: "PerformanceIndicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
