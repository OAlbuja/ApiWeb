using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWeb.Migrations
{
    /// <inheritdoc />
    public partial class factura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Producto",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Producto",
                newName: "description");

            migrationBuilder.AddColumn<int>(
                name: "cantidad",
                table: "Producto",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "cantidad",
                table: "Producto");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Producto",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Producto",
                newName: "Description");
        }
    }
}
