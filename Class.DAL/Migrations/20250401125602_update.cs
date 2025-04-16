using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.DAL.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassSubjects",
                table: "ClassSubjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassSubjects",
                table: "ClassSubjects",
                columns: new[] { "SubjectId", "ClassId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ClassSubjects",
                table: "ClassSubjects");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ClassSubjects",
                table: "ClassSubjects",
                columns: new[] { "SubjectId", "ClassId", "TeacherId" });
        }
    }
}
