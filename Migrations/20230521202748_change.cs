using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiWeb.Migrations
{
    /// <inheritdoc />
    public partial class change : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cliente",
                newName: "cedula");

            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Cliente",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Cliente");

            migrationBuilder.RenameColumn(
                name: "cedula",
                table: "Cliente",
                newName: "Id");
        }
    }
}
