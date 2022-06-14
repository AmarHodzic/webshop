using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Orders_ordersid",
                table: "ProductOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Products_productsid",
                table: "ProductOrders");

            migrationBuilder.RenameColumn(
                name: "productsid",
                table: "ProductOrders",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "ordersid",
                table: "ProductOrders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_productsid",
                table: "ProductOrders",
                newName: "IX_ProductOrders_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Orders_OrderId",
                table: "ProductOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Products_ProductId",
                table: "ProductOrders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Orders_OrderId",
                table: "ProductOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Products_ProductId",
                table: "ProductOrders");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductOrders",
                newName: "productsid");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "ProductOrders",
                newName: "ordersid");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductOrders",
                newName: "IX_ProductOrders_productsid");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Orders_ordersid",
                table: "ProductOrders",
                column: "ordersid",
                principalTable: "Orders",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Products_productsid",
                table: "ProductOrders",
                column: "productsid",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
