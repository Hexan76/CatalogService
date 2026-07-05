using CatalogService.CategoryCustomization;
using CatalogService.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CatalogService.Customizations;

public class CustomizationGroupConfig : IEntityTypeConfiguration<CustomizationGroup>
{
    public void Configure(EntityTypeBuilder<CustomizationGroup> builder)
    {
        builder.ToTable(nameof(CustomizationGroup), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasKey(x => x.Id);
    }
}
