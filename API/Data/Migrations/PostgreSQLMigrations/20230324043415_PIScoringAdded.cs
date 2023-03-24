using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.Migrations.PostgreSQLMigrations
{
    public partial class PIScoringAdded : Migration
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
                name: "FK_TakeAssignment_Assignments_AssignmentId",
                table: "TakeAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_TakeAssignment_Students_StudentId",
                table: "TakeAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_TakeQuestion_Answers_AnswerId",
                table: "TakeQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TakeQuestion_Questions_QuestionId",
                table: "TakeQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TakeQuestion_TakeAssignment_TakeAssignmentId",
                table: "TakeQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Takes",
                table: "Takes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakeQuestion",
                table: "TakeQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakeAssignment",
                table: "TakeAssignment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePI",
                table: "CoursePI");

            migrationBuilder.RenameTable(
                name: "TakeQuestion",
                newName: "TakeQuestions");

            migrationBuilder.RenameTable(
                name: "TakeAssignment",
                newName: "TakeAssignments");

            migrationBuilder.RenameTable(
                name: "CoursePI",
                newName: "CoursePIs");

            migrationBuilder.RenameIndex(
                name: "IX_TakeQuestion_TakeAssignmentId",
                table: "TakeQuestions",
                newName: "IX_TakeQuestions_TakeAssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_TakeQuestion_QuestionId",
                table: "TakeQuestions",
                newName: "IX_TakeQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_TakeQuestion_AnswerId",
                table: "TakeQuestions",
                newName: "IX_TakeQuestions_AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_TakeAssignment_StudentId",
                table: "TakeAssignments",
                newName: "IX_TakeAssignments_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_TakeAssignment_AssignmentId",
                table: "TakeAssignments",
                newName: "IX_TakeAssignments_AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePI_PerformanceIndicatorId",
                table: "CoursePIs",
                newName: "IX_CoursePIs_PerformanceIndicatorId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Takes",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "FullMarks",
                table: "Assignments",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "CoursePIs",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<int>(
                name: "FullPIScore",
                table: "CoursePIs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Takes",
                table: "Takes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeQuestions",
                table: "TakeQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeAssignments",
                table: "TakeAssignments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePIs",
                table: "CoursePIs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AssignmentPIs",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "integer", nullable: false),
                    PerformanceIndicatorId = table.Column<int>(type: "integer", nullable: false),
                    FullScore = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignmentPIs", x => new { x.AssignmentId, x.PerformanceIndicatorId });
                    table.ForeignKey(
                        name: "FK_AssignmentPIs_Assignments_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignmentPIs_PerformanceIndicators_PerformanceIndicatorId",
                        column: x => x.PerformanceIndicatorId,
                        principalTable: "PerformanceIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PIScores",
                columns: table => new
                {
                    PerformanceIndicatorId = table.Column<int>(type: "integer", nullable: false),
                    TakeAssignmentId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "TakesCoursePIs",
                columns: table => new
                {
                    TakesId = table.Column<int>(type: "integer", nullable: false),
                    PerformanceIndicatorId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TakesCoursePIs", x => new { x.TakesId, x.PerformanceIndicatorId });
                    table.ForeignKey(
                        name: "FK_TakesCoursePIs_PerformanceIndicators_PerformanceIndicatorId",
                        column: x => x.PerformanceIndicatorId,
                        principalTable: "PerformanceIndicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TakesCoursePIs_Takes_TakesId",
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
                name: "IX_CoursePIs_CourseId_PerformanceIndicatorId",
                table: "CoursePIs",
                columns: new[] { "CourseId", "PerformanceIndicatorId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignmentPIs_PerformanceIndicatorId",
                table: "AssignmentPIs",
                column: "PerformanceIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_PIScores_PerformanceIndicatorId",
                table: "PIScores",
                column: "PerformanceIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_TakesCoursePIs_PerformanceIndicatorId",
                table: "TakesCoursePIs",
                column: "PerformanceIndicatorId");

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
                name: "FK_TakeAssignments_Assignments_AssignmentId",
                table: "TakeAssignments",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakeAssignments_Students_StudentId",
                table: "TakeAssignments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakeQuestions_Answers_AnswerId",
                table: "TakeQuestions",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakeQuestions_Questions_QuestionId",
                table: "TakeQuestions",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakeQuestions_TakeAssignments_TakeAssignmentId",
                table: "TakeQuestions",
                column: "TakeAssignmentId",
                principalTable: "TakeAssignments",
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
                name: "FK_TakeAssignments_Assignments_AssignmentId",
                table: "TakeAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TakeAssignments_Students_StudentId",
                table: "TakeAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_TakeQuestions_Answers_AnswerId",
                table: "TakeQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_TakeQuestions_Questions_QuestionId",
                table: "TakeQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_TakeQuestions_TakeAssignments_TakeAssignmentId",
                table: "TakeQuestions");

            migrationBuilder.DropTable(
                name: "AssignmentPIs");

            migrationBuilder.DropTable(
                name: "PIScores");

            migrationBuilder.DropTable(
                name: "TakesCoursePIs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Takes",
                table: "Takes");

            migrationBuilder.DropIndex(
                name: "IX_Takes_StudentId_CourseId_SemesterId",
                table: "Takes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakeQuestions",
                table: "TakeQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakeAssignments",
                table: "TakeAssignments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CoursePIs",
                table: "CoursePIs");

            migrationBuilder.DropIndex(
                name: "IX_CoursePIs_CourseId_PerformanceIndicatorId",
                table: "CoursePIs");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Takes");

            migrationBuilder.DropColumn(
                name: "FullMarks",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "CoursePIs");

            migrationBuilder.DropColumn(
                name: "FullPIScore",
                table: "CoursePIs");

            migrationBuilder.RenameTable(
                name: "TakeQuestions",
                newName: "TakeQuestion");

            migrationBuilder.RenameTable(
                name: "TakeAssignments",
                newName: "TakeAssignment");

            migrationBuilder.RenameTable(
                name: "CoursePIs",
                newName: "CoursePI");

            migrationBuilder.RenameIndex(
                name: "IX_TakeQuestions_TakeAssignmentId",
                table: "TakeQuestion",
                newName: "IX_TakeQuestion_TakeAssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_TakeQuestions_QuestionId",
                table: "TakeQuestion",
                newName: "IX_TakeQuestion_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_TakeQuestions_AnswerId",
                table: "TakeQuestion",
                newName: "IX_TakeQuestion_AnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_TakeAssignments_StudentId",
                table: "TakeAssignment",
                newName: "IX_TakeAssignment_StudentId");

            migrationBuilder.RenameIndex(
                name: "IX_TakeAssignments_AssignmentId",
                table: "TakeAssignment",
                newName: "IX_TakeAssignment_AssignmentId");

            migrationBuilder.RenameIndex(
                name: "IX_CoursePIs_PerformanceIndicatorId",
                table: "CoursePI",
                newName: "IX_CoursePI_PerformanceIndicatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Takes",
                table: "Takes",
                columns: new[] { "StudentId", "CourseId", "SemesterId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeQuestion",
                table: "TakeQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeAssignment",
                table: "TakeAssignment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CoursePI",
                table: "CoursePI",
                columns: new[] { "CourseId", "PerformanceIndicatorId" });

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
                name: "FK_TakeAssignment_Assignments_AssignmentId",
                table: "TakeAssignment",
                column: "AssignmentId",
                principalTable: "Assignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakeAssignment_Students_StudentId",
                table: "TakeAssignment",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakeQuestion_Answers_AnswerId",
                table: "TakeQuestion",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakeQuestion_Questions_QuestionId",
                table: "TakeQuestion",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TakeQuestion_TakeAssignment_TakeAssignmentId",
                table: "TakeQuestion",
                column: "TakeAssignmentId",
                principalTable: "TakeAssignment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
