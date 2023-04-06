using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations.SqliteMigrations
{
    public partial class RenameFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PIScores_PerformanceIndicators_PerformanceIndicatorId",
                table: "PIScores");

            migrationBuilder.DropForeignKey(
                name: "FK_PIScores_TakeAssignments_TakeAssignmentId",
                table: "PIScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PIScores",
                table: "PIScores");

            migrationBuilder.RenameTable(
                name: "PIScores",
                newName: "TakeAssignmentPIScores");

            migrationBuilder.RenameColumn(
                name: "FullPIScore",
                table: "CoursePIs",
                newName: "PIFullMarks");

            migrationBuilder.RenameIndex(
                name: "IX_PIScores_PerformanceIndicatorId",
                table: "TakeAssignmentPIScores",
                newName: "IX_TakeAssignmentPIScores_PerformanceIndicatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeAssignmentPIScores",
                table: "TakeAssignmentPIScores",
                columns: new[] { "TakeAssignmentId", "PerformanceIndicatorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_TakeAssignmentPIScores_PerformanceIndicators_PerformanceIndicatorId",
                table: "TakeAssignmentPIScores",
                column: "PerformanceIndicatorId",
                principalTable: "PerformanceIndicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakeAssignmentPIScores_TakeAssignments_TakeAssignmentId",
                table: "TakeAssignmentPIScores",
                column: "TakeAssignmentId",
                principalTable: "TakeAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TakeAssignmentPIScores_PerformanceIndicators_PerformanceIndicatorId",
                table: "TakeAssignmentPIScores");

            migrationBuilder.DropForeignKey(
                name: "FK_TakeAssignmentPIScores_TakeAssignments_TakeAssignmentId",
                table: "TakeAssignmentPIScores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakeAssignmentPIScores",
                table: "TakeAssignmentPIScores");

            migrationBuilder.RenameTable(
                name: "TakeAssignmentPIScores",
                newName: "PIScores");

            migrationBuilder.RenameColumn(
                name: "PIFullMarks",
                table: "CoursePIs",
                newName: "FullPIScore");

            migrationBuilder.RenameIndex(
                name: "IX_TakeAssignmentPIScores_PerformanceIndicatorId",
                table: "PIScores",
                newName: "IX_PIScores_PerformanceIndicatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PIScores",
                table: "PIScores",
                columns: new[] { "TakeAssignmentId", "PerformanceIndicatorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PIScores_PerformanceIndicators_PerformanceIndicatorId",
                table: "PIScores",
                column: "PerformanceIndicatorId",
                principalTable: "PerformanceIndicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PIScores_TakeAssignments_TakeAssignmentId",
                table: "PIScores",
                column: "TakeAssignmentId",
                principalTable: "TakeAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
