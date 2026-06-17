using CatalogService.Domain;
using CatalogService.Items;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CatalogService.Categories;

public class ItemCategoryConfig : IEntityTypeConfiguration<ItemCategory>
{
    public void Configure(EntityTypeBuilder<ItemCategory> builder)
    {
        builder.ToTable(nameof(ItemCategory), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new
        {
            x.ItemId,
            x.CategoryId
        }).IsUnique();

        builder.HasOne(x => x.Item)
            .WithMany(x => x.ItemCategories)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.ItemCategories)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
