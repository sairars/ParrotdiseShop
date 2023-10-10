using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParrotdiseShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RePopulateCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(@$"INSERT INTO Categories(Name, DisplayOrder) VALUES('Treats', 1)");
            //migrationBuilder.Sql(@$"INSERT INTO Categories(Name, DisplayOrder) VALUES('Toys', 2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql($"DELETE FROM dbo.Categories WHERE Name IN ('Treats', 'Toys')");
        }
    }
}
