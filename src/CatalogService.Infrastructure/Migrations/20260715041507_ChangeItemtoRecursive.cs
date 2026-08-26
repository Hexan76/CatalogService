using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CatalogService.Migrations
{
    /// <inheritdoc />
    public partial class ChangeItemtoRecursive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<long>(
                name: "ParentId",
                table: "Item",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VendorId",
                table: "Customization",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Item_ParentId",
                table: "Item",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Item_ParentId",
                table: "Item",
                column: "ParentId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Item_ParentId",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ParentId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "VendorId",
                table: "Customization");

        }
    }
}
