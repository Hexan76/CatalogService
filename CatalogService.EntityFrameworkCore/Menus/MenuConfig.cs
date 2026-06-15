using CatalogService.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CatalogService.Menus;

public class MenuConfig : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.ToTable(nameof(Menu), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasMany(x => x.MenuItems)
            .WithOne(x => x.Menu)
            .HasForeignKey(x => x.MenuId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
    }
}
