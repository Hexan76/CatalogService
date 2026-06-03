using CatalogService.Domain;
using CatalogService.MenuItemCategories;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CatalogService.MenuItems;

public class MenuItemCategoriesConfig : IEntityTypeConfiguration<MenuItemCategory>
{
    public void Configure(EntityTypeBuilder<MenuItemCategory> builder)
    {
        builder.ToTable(nameof(MenuItemCategory), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasKey(x => new
        {
            x.MenuItemId,
            x.CategoryId
        });


        builder.HasOne(x => x.MenuItem)
            .WithMany(x => x.MenuItemCategories)
            .HasForeignKey(x => x.MenuItemId);

        builder.HasOne(x => x.Category)
            .WithMany(x => x.MenuItemCategories)
            .HasForeignKey(x => x.CategoryId);
    }
}
