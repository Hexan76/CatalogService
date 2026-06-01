using System.ComponentModel.DataAnnotations.Schema;

using Volo.Abp.Domain.Entities;

namespace CatalogService.Files;

[Table(nameof(FileEntity))]
public class FileEntity : Entity<Guid>
{
    public string FileName  { get; set; } = default!;
    public string Url { get; set; } = default!;
    public int Priority { get; set; } = default!;
    public string EntityType { get; set; }
    public string Role { get; set; }
    public Guid EntityId { get; set; }
    
}
