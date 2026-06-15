using CatalogService.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CatalogService.Categories;

public class CategoryBusinessTypeConfig : IEntityTypeConfiguration<CategoryBusinessType>
{
    public void Configure(EntityTypeBuilder<CategoryBusinessType> builder)
    {
        builder.ToTable(nameof(CategoryBusinessType), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();


        builder.HasOne(p => p.BusinessType)
            .WithMany(c => c.CategoryBusinessTypes)
            .HasForeignKey(fk => fk.BusinessTypeId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.CategoryBusinessTypes)
            .HasForeignKey(fk => fk.CategoryId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

    }
}
