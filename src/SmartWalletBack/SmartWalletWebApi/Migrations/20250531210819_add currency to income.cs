using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartWalletWebApi.Migrations
{
    /// <inheritdoc />
    public partial class addcurrencytoincome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Incomes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Incomes");
        }
    }
}
