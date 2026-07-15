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
            migrationBuilder.DropTable(
                name: "ItemVariant");

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

            migrationBuilder.CreateTable(
                name: "ItemVariant",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    DiscountPrecent = table.Column<int>(type: "integer", nullable: false),
                    HasDiscount = table.Column<bool>(type: "boolean", nullable: false),
                    IsDisabled = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    SortOrder = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemVariant_Item_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemVariant_ItemId",
                table: "ItemVariant",
                column: "ItemId");
        }
    }
}
