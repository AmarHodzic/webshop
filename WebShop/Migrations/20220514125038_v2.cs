using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    public partial class v2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_Orderid",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Orderid",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Orderid",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    ordersid = table.Column<int>(type: "int", nullable: false),
                    productsid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => new { x.ordersid, x.productsid });
                    table.ForeignKey(
                        name: "FK_ProductOrders_Orders_ordersid",
                        column: x => x.ordersid,
                        principalTable: "Orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Products_productsid",
                        column: x => x.productsid,
                        principalTable: "Products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_productsid",
                table: "ProductOrders",
                column: "productsid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.AddColumn<int>(
                name: "Orderid",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Orderid",
                table: "Products",
                column: "Orderid");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_Orderid",
                table: "Products",
                column: "Orderid",
                principalTable: "Orders",
                principalColumn: "id");
        }
    }
}
