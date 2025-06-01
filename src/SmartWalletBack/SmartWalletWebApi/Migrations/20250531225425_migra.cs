using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartWalletWebApi.Migrations
{
    /// <inheritdoc />
    public partial class migra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Payments_PaymentId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Payments_PaymentId",
                table: "Products",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Payments_PaymentId",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "Products",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Payments_PaymentId",
                table: "Products",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }
    }
}
