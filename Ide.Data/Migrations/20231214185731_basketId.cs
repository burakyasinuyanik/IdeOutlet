using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ide.Data.Migrations
{
    /// <inheritdoc />
    public partial class basketId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingBaskets_Users_AppUserId",
                table: "ShoppingBaskets");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingBaskets_AppUserId",
                table: "ShoppingBaskets");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "ShoppingBaskets");

            migrationBuilder.AddColumn<int>(
                name: "ShoppingBasketId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ShoppingBasketId",
                table: "Users",
                column: "ShoppingBasketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ShoppingBaskets_ShoppingBasketId",
                table: "Users",
                column: "ShoppingBasketId",
                principalTable: "ShoppingBaskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ShoppingBaskets_ShoppingBasketId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ShoppingBasketId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ShoppingBasketId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "ShoppingBaskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingBaskets_AppUserId",
                table: "ShoppingBaskets",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingBaskets_Users_AppUserId",
                table: "ShoppingBaskets",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
