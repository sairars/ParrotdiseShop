using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParrotdiseShop.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameToProvinceInAspNetUsersTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Orders",
                newName: "Province");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "AspNetUsers",
                newName: "Province");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Orders",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "AspNetUsers",
                newName: "State");
        }
    }
}
