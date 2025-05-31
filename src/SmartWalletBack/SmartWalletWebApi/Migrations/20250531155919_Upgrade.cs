using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartWalletWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Upgrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Payments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Payments");
        }
    }
}
