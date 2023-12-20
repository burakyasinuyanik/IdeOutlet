using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ide.Data.Migrations
{
    /// <inheritdoc />
    public partial class addRemaningStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RemainingStock",
                table: "Products",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RemainingStock",
                table: "Products");
        }
    }
}
