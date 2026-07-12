using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Customizations;

using Volo.Abp.Domain.Entities.Auditing;

namespace CatalogService.CategoryCustomization;


[Table(nameof(CustomizationGroup))]
public class CustomizationGroup : AuditedAggregateRoot<long>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public long? ParentId { get; set; }
    public virtual ICollection<Customization> Customizations { get; set; }
}
