using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessClientsManager.Migrations
{
    /// <inheritdoc />
    public partial class AddLogObjectColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NextObject",
                table: "Log",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrevObject",
                table: "Log",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NextObject",
                table: "Log");

            migrationBuilder.DropColumn(
                name: "PrevObject",
                table: "Log");
        }
    }
}
