using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cube_C___API.Migrations
{
    /// <inheritdoc />
    public partial class fixdeliveredsupplierorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delivered",
                table: "SupplierOrders",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivered",
                table: "SupplierOrders");
        }
    }
}
