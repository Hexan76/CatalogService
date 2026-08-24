using System.Text.Json;

using CatalogService.Domain;

using Framework.BuildingBlock.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Files;

public class FileConfig : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        builder.ToTable(nameof(FileEntity), CatalogServiceDbProperties.DbSchema);
        builder.ConfigureByConvention();

        builder.Property(x => x.Variants)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<Dictionary<string, object>>(v)!);
    }
}
