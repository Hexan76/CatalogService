using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.Categories;

using Framework.BuildingBlock.Domain;

using Volo.Abp.Domain.Entities.Auditing;

namespace CatalogService.BusinessTypes;

[Table(nameof(BusinessType))]
public class BusinessType : AuditedAggregateRoot<long>, IHasDisabled
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }
    public virtual ICollection<CategoryBusinessType> CategoryBusinessTypes { get; set; } = [];
}
