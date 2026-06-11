using CatalogService.Domain;
using CatalogService.Menus;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CatalogService.Categories;

public class MenuItemConfig : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable(nameof(MenuItem), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();


    }
}
