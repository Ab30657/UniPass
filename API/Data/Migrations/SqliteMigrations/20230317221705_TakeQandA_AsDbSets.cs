using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations.SqliteMigrations
{
    public partial class TakeQandA_AsDbSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "PK_TakeQuestion",
                table: "TakeQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakeAssignment",
                table: "TakeAssignment");

            migrationBuilder.RenameTable(
                name: "TakeQuestion",
                newName: "TakeQuestions");

            migrationBuilder.RenameTable(
                name: "TakeAssignment",
                newName: "TakeAssignments");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeQuestions",
                table: "TakeQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeAssignments",
                table: "TakeAssignments",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakeQuestions",
                table: "TakeQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TakeAssignments",
                table: "TakeAssignments");

            migrationBuilder.RenameTable(
                name: "TakeQuestions",
                newName: "TakeQuestion");

            migrationBuilder.RenameTable(
                name: "TakeAssignments",
                newName: "TakeAssignment");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeQuestion",
                table: "TakeQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TakeAssignment",
                table: "TakeAssignment",
                column: "Id");

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
