using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations.SqliteMigrations
{
    public partial class AddPIScore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FullMarks",
                table: "Assignments",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PIScores",
                columns: table => new
                {
                    PerformanceIndicatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    TakeAssignmentId = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PIScores", x => new { x.TakeAssignmentId, x.PerformanceIndicatorId });
                    table.ForeignKey(
                        name: "FK_PIScores_PerformanceIndicators_PerformanceIndicatorId",
                        column: x => x.PerformanceIndicatorId,
                        principalTable: "PerformanceIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PIScores_TakeAssignments_TakeAssignmentId",
                        column: x => x.TakeAssignmentId,
                        principalTable: "TakeAssignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PIScores_PerformanceIndicatorId",
                table: "PIScores",
                column: "PerformanceIndicatorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PIScores");

            migrationBuilder.DropColumn(
                name: "FullMarks",
                table: "Assignments");
        }
    }
}
