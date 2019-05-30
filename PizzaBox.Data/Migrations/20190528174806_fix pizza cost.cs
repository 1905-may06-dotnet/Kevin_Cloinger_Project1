using Microsoft.EntityFrameworkCore.Migrations;

namespace PizzaBox.Data.Migrations
{
    public partial class fixpizzacost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Cost",
                table: "Pizza",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_OrderId",
                table: "Pizza",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_Order_OrderId",
                table: "Pizza",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_Order_OrderId",
                table: "Pizza");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_OrderId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Pizza");
        }
    }
}
