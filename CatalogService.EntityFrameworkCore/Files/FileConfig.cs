using System.Text.Json;

using CatalogService.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Volo.Abp.EntityFrameworkCore.Modeling;

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
                v => (IDictionary<string, object>)
                    JsonSerializer.Deserialize<Dictionary<string, object>>(v)!);
    }
}
