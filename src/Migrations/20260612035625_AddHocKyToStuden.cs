using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InternshipManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddHocKyToStuden : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HocKy",
                table: "Students",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HocKy",
                table: "Students");
        }
    }
}
