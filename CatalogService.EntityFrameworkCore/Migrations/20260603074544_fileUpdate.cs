using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Migrations
{
    /// <inheritdoc />
    public partial class fileUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Variants",
                table: "FileEntity",
                type: "jsonb",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Variants",
                table: "FileEntity");
        }
    }
}
