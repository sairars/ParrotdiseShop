using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParrotdiseShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovePriceColumnFromShoppingCartItemsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShoppingCartItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ShoppingCartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
