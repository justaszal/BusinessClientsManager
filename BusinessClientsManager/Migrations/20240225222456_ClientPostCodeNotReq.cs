using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessClientsManager.Migrations
{
    /// <inheritdoc />
    public partial class ClientPostCodeNotReq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessClient_Postcode_PostcodeName",
                table: "BusinessClient");

            migrationBuilder.AlterColumn<string>(
                name: "PostcodeName",
                table: "BusinessClient",
                type: "nvarchar(10)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessClient_Postcode_PostcodeName",
                table: "BusinessClient",
                column: "PostcodeName",
                principalTable: "Postcode",
                principalColumn: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusinessClient_Postcode_PostcodeName",
                table: "BusinessClient");

            migrationBuilder.AlterColumn<string>(
                name: "PostcodeName",
                table: "BusinessClient",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BusinessClient_Postcode_PostcodeName",
                table: "BusinessClient",
                column: "PostcodeName",
                principalTable: "Postcode",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
