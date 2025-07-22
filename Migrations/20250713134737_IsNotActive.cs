using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    /// <inheritdoc />
    public partial class IsNotActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Shelves",
                newName: "IsNotActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Books",
                newName: "IsNotActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsNotActive",
                table: "Shelves",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "IsNotActive",
                table: "Books",
                newName: "IsActive");
        }
    }
}
