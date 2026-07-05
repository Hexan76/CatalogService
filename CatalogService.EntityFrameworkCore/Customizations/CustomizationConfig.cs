using CatalogService.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Volo.Abp.EntityFrameworkCore.Modeling;

namespace CatalogService.Customizations;

public class CustomizationConfig : IEntityTypeConfiguration<Customization>
{
    public void Configure(EntityTypeBuilder<Customization> builder)
    {
        builder.ToTable(nameof(Customization), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.CustomizationGroup)
            .WithMany(x => x.Customizations)
            .HasForeignKey(x => x.CustomizationGroupId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
