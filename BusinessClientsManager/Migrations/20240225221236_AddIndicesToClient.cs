using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessClientsManager.Migrations
{
    /// <inheritdoc />
    public partial class AddIndicesToClient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BusinessClient",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessClient_Address",
                table: "BusinessClient",
                column: "Address",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BusinessClient_Name",
                table: "BusinessClient",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BusinessClient_Address",
                table: "BusinessClient");

            migrationBuilder.DropIndex(
                name: "IX_BusinessClient_Name",
                table: "BusinessClient");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "BusinessClient",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
