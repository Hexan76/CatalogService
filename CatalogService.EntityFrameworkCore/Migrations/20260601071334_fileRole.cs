using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Migrations
{
    /// <inheritdoc />
    public partial class fileRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerUrl",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IconUrl",
                table: "Category");

            migrationBuilder.RenameColumn(
                name: "ParentId",
                table: "FileEntity",
                newName: "EntityId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "FileEntity",
                newName: "Role");

            migrationBuilder.AddColumn<string>(
                name: "EntityType",
                table: "FileEntity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "FileEntity",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "FileEntity",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "FileEntity");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "FileEntity");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "FileEntity");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "FileEntity",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "EntityId",
                table: "FileEntity",
                newName: "ParentId");

            migrationBuilder.AddColumn<string>(
                name: "BannerUrl",
                table: "Category",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IconUrl",
                table: "Category",
                type: "text",
                nullable: true);
        }
    }
}
