using CatalogService.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CatalogService.Categories;

public class CategoryConfig : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable(nameof(Category), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasOne(c => c.Parent)
            .WithMany(p => p.Children)
            .HasForeignKey(fk => fk.ParentId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
    }
}
