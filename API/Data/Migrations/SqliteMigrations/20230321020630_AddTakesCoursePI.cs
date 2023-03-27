using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations.SqliteMigrations
{
    public partial class AddTakesCoursePI : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Takes",
                table: "Takes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePI",
                table: "CoursePI");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Takes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CoursePI",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Takes",
                table: "Takes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePI",
                table: "CoursePI",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "TakesCoursePI",
                columns: table => new
                {
                    TakesId = table.Column<int>(type: "INTEGER", nullable: false),
                    CoursePIId = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakesCoursePI", x => new { x.TakesId, x.CoursePIId });
                    table.ForeignKey(
                        name: "FK_TakesCoursePI_CoursePI_TakesId",
                        column: x => x.TakesId,
                        principalTable: "CoursePI",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakesCoursePI_Takes_TakesId",
                        column: x => x.TakesId,
                        principalTable: "Takes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Takes_StudentId_CourseId_SemesterId",
                table: "Takes",
                columns: new[] { "StudentId", "CourseId", "SemesterId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CoursePI_CourseId_PerformanceIndicatorId",
                table: "CoursePI",
                columns: new[] { "CourseId", "PerformanceIndicatorId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TakesCoursePI");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Takes",
                table: "Takes");

            migrationBuilder.DropIndex(
                name: "IX_Takes_StudentId_CourseId_SemesterId",
                table: "Takes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePI",
                table: "CoursePI");

            migrationBuilder.DropIndex(
                name: "IX_CoursePI_CourseId_PerformanceIndicatorId",
                table: "CoursePI");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Takes");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CoursePI");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Takes",
                table: "Takes",
                columns: new[] { "StudentId", "CourseId", "SemesterId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePI",
                table: "CoursePI",
                columns: new[] { "CourseId", "PerformanceIndicatorId" });
        }
    }
}
