using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebShop.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Userid",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Userid",
                table: "Orders",
                column: "Userid");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_Userid",
                table: "Orders",
                column: "Userid",
                principalTable: "Users",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_Userid",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_Userid",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "Userid",
                table: "Orders");
        }
    }
}
