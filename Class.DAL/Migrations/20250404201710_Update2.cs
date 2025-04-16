using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Update2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_AspNetUsers_StudentId",
                table: "Grades");

            migrationBuilder.DropForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades");

            migrationBuilder.DropIndex(
                name: "IX_Grades_StudentId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "Grades",
                newName: "HomeworkSubmissionId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_SubjectId",
                table: "Grades",
                newName: "IX_Grades_HomeworkSubmissionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_HomeworkSubmissions_HomeworkSubmissionId",
                table: "Grades",
                column: "HomeworkSubmissionId",
                principalTable: "HomeworkSubmissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Grades_HomeworkSubmissions_HomeworkSubmissionId",
                table: "Grades");

            migrationBuilder.RenameColumn(
                name: "HomeworkSubmissionId",
                table: "Grades",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Grades_HomeworkSubmissionId",
                table: "Grades",
                newName: "IX_Grades_SubjectId");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Grades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_AspNetUsers_StudentId",
                table: "Grades",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Grades_Subjects_SubjectId",
                table: "Grades",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
