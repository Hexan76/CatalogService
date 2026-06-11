using CatalogService.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CatalogService.BusinessTypes;

public class BusinessTypeConfig : IEntityTypeConfiguration<BusinessType>
{
    public void Configure(EntityTypeBuilder<BusinessType> builder)
    {
        builder.ToTable(nameof(BusinessType), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasMany(x => x.CategoryBusinessTypes)
            .WithOne(x => x.BusinessType)
            .HasForeignKey(x => x.BusinessTypeId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
    }
}
