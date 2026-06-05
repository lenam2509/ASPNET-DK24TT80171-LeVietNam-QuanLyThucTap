using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordToLecturer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Lecturers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Lecturers");
        }
    }
}
