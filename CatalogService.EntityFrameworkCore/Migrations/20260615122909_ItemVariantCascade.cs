using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogService.Migrations
{
    /// <inheritdoc />
    public partial class ItemVariantCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemVariant_Item_ItemId",
                table: "ItemVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Item_ItemId",
                table: "MenuItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVariant_Item_ItemId",
                table: "ItemVariant",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Item_ItemId",
                table: "MenuItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemVariant_Item_ItemId",
                table: "ItemVariant");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuItem_Item_ItemId",
                table: "MenuItem");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemVariant_Item_ItemId",
                table: "ItemVariant",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItem_Item_ItemId",
                table: "MenuItem",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
