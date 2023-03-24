using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations.SqliteMigrations
{
    public partial class AddTakesCoursePIasDbSetCheck : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursePI_Courses_CourseId",
                table: "CoursePI");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursePI_PerformanceIndicators_PerformanceIndicatorId",
                table: "CoursePI");

            migrationBuilder.DropForeignKey(
                name: "FK_TakesCoursePI_CoursePI_TakesId",
                table: "TakesCoursePI");

            migrationBuilder.DropForeignKey(
                name: "FK_TakesCoursePI_Takes_TakesId",
                table: "TakesCoursePI");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakesCoursePI",
                table: "TakesCoursePI");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePI",
                table: "CoursePI");

            migrationBuilder.RenameTable(
                name: "TakesCoursePI",
                newName: "TakesCoursePIs");

            migrationBuilder.RenameTable(
                name: "CoursePI",
                newName: "CoursePIs");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePI_PerformanceIndicatorId",
                table: "CoursePIs",
                newName: "IX_CoursePIs_PerformanceIndicatorId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePI_CourseId_PerformanceIndicatorId",
                table: "CoursePIs",
                newName: "IX_CoursePIs_CourseId_PerformanceIndicatorId");

            migrationBuilder.AddColumn<int>(
                name: "FullPIScore",
                table: "CoursePIs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakesCoursePIs",
                table: "TakesCoursePIs",
                columns: new[] { "TakesId", "CoursePIId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePIs",
                table: "CoursePIs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePIs_Courses_CourseId",
                table: "CoursePIs",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePIs_PerformanceIndicators_PerformanceIndicatorId",
                table: "CoursePIs",
                column: "PerformanceIndicatorId",
                principalTable: "PerformanceIndicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCoursePIs_CoursePIs_TakesId",
                table: "TakesCoursePIs",
                column: "TakesId",
                principalTable: "CoursePIs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCoursePIs_Takes_TakesId",
                table: "TakesCoursePIs",
                column: "TakesId",
                principalTable: "Takes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CoursePIs_Courses_CourseId",
                table: "CoursePIs");

            migrationBuilder.DropForeignKey(
                name: "FK_CoursePIs_PerformanceIndicators_PerformanceIndicatorId",
                table: "CoursePIs");

            migrationBuilder.DropForeignKey(
                name: "FK_TakesCoursePIs_CoursePIs_TakesId",
                table: "TakesCoursePIs");

            migrationBuilder.DropForeignKey(
                name: "FK_TakesCoursePIs_Takes_TakesId",
                table: "TakesCoursePIs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakesCoursePIs",
                table: "TakesCoursePIs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePIs",
                table: "CoursePIs");

            migrationBuilder.DropColumn(
                name: "FullPIScore",
                table: "CoursePIs");

            migrationBuilder.RenameTable(
                name: "TakesCoursePIs",
                newName: "TakesCoursePI");

            migrationBuilder.RenameTable(
                name: "CoursePIs",
                newName: "CoursePI");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePIs_PerformanceIndicatorId",
                table: "CoursePI",
                newName: "IX_CoursePI_PerformanceIndicatorId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePIs_CourseId_PerformanceIndicatorId",
                table: "CoursePI",
                newName: "IX_CoursePI_CourseId_PerformanceIndicatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakesCoursePI",
                table: "TakesCoursePI",
                columns: new[] { "TakesId", "CoursePIId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePI",
                table: "CoursePI",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePI_Courses_CourseId",
                table: "CoursePI",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CoursePI_PerformanceIndicators_PerformanceIndicatorId",
                table: "CoursePI",
                column: "PerformanceIndicatorId",
                principalTable: "PerformanceIndicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCoursePI_CoursePI_TakesId",
                table: "TakesCoursePI",
                column: "TakesId",
                principalTable: "CoursePI",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakesCoursePI_Takes_TakesId",
                table: "TakesCoursePI",
                column: "TakesId",
                principalTable: "Takes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
