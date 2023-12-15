using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ide.Data.Migrations
{
    /// <inheritdoc />
    public partial class urunbasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ShoppingBaskets_ShoppingBasketId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ShoppingBasketId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShoppingBasketId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductShoppingBasket",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "int", nullable: false),
                    ShoppingBasketsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductShoppingBasket", x => new { x.ProductsId, x.ShoppingBasketsId });
                    table.ForeignKey(
                        name: "FK_ProductShoppingBasket_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductShoppingBasket_ShoppingBaskets_ShoppingBasketsId",
                        column: x => x.ShoppingBasketsId,
                        principalTable: "ShoppingBaskets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductShoppingBasket_ShoppingBasketsId",
                table: "ProductShoppingBasket",
                column: "ShoppingBasketsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductShoppingBasket");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingBasketId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ShoppingBasketId",
                table: "Products",
                column: "ShoppingBasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ShoppingBaskets_ShoppingBasketId",
                table: "Products",
                column: "ShoppingBasketId",
                principalTable: "ShoppingBaskets",
                principalColumn: "Id");
        }
    }
}
