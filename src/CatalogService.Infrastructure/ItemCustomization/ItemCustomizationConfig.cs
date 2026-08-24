using CatalogService.Domain;
using CatalogService.Items;

using Framework.BuildingBlock.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Categories;

public class ItemCustomizationConfig : IEntityTypeConfiguration<ItemCustomization>
{
    public void Configure(EntityTypeBuilder<ItemCustomization> builder)
    {
        builder.ToTable(nameof(ItemCustomization), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new
        {
            x.ItemId,
            x.CustomizationId
        }).IsUnique();

        builder.HasOne(x => x.Item)
            .WithMany(x => x.Customizations)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Customization)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CustomizationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
