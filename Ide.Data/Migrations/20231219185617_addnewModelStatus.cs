using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ide.Data.Migrations
{
    /// <inheritdoc />
    public partial class addnewModelStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "OrderPoducts");

            migrationBuilder.AddColumn<int>(
                name: "ProductTypeId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderTypeId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalAmount",
                table: "Orders",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderProductTypeId",
                table: "OrderPoducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCraeted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCraeted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DateCraeted = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDeleted = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPoducts_OrderProductTypeId",
                table: "OrderPoducts",
                column: "OrderProductTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderPoducts_OrderProductTypes_OrderProductTypeId",
                table: "OrderPoducts",
                column: "OrderProductTypeId",
                principalTable: "OrderProductTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderTypes_OrderTypeId",
                table: "Orders",
                column: "OrderTypeId",
                principalTable: "OrderTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products",
                column: "ProductTypeId",
                principalTable: "ProductTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderPoducts_OrderProductTypes_OrderProductTypeId",
                table: "OrderPoducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderTypes_OrderTypeId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductTypes_ProductTypeId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "OrderProductTypes");

            migrationBuilder.DropTable(
                name: "OrderTypes");

            migrationBuilder.DropTable(
                name: "ProductTypes");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductTypeId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderTypeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderPoducts_OrderProductTypeId",
                table: "OrderPoducts");

            migrationBuilder.DropColumn(
                name: "ProductTypeId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderTypeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderProductTypeId",
                table: "OrderPoducts");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "OrderPoducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
