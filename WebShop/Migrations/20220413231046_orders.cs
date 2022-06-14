using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    public partial class orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_productId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_productId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "createdOn",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "productId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "productVal",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "updatedOn",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "Orderid",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "quantities",
                table: "Orders",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "quantities",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "createdOn",
                table: "Orders",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "productId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "productVal",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedOn",
                table: "Orders",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Orders_productId",
                table: "Orders",
                column: "productId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_productId",
                table: "Orders",
                column: "productId",
                principalTable: "Products",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
