using CatalogService.Domain;
using CatalogService.Menus;

using Framework.BuildingBlock.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Categories;

public class MenuItemConfig : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable(nameof(MenuItem), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();


        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .ValueGeneratedOnAdd()
                ;


        builder.HasIndex(x => new
        {
            x.ItemId,
            x.MenuId
        }).IsUnique();

        builder.HasOne(p => p.Menu)
            .WithMany(c => c.MenuItems)
            .HasForeignKey(fk => fk.MenuId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

        builder.HasOne(p => p.Item)
            .WithMany(c => c.MenuItems)
            .HasForeignKey(fk => fk.ItemId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);

    }
}
