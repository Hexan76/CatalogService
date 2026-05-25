using System.ComponentModel.DataAnnotations.Schema;

using Framework.BuildingBlock.Domain;

using Volo.Abp.Domain.Entities.Auditing;

namespace CatalogService.Categories;

[Table(nameof(Category))]
public class Category : AuditedAggregateRoot<Guid>, IHasDisabled
{
    public string Name { get; set; } = default!;
    public string Slug { get; set; } = default!;
    public string? Description { get; set; }
    public int SortOrder { get; set; }
    public string? IconUrl { get; set; }
    public string? BannerUrl { get; set; }
    public bool IsDisabled { get; set; }
    public Guid? ParentId { get; set; }
    public virtual Category? Parent { get; set; }
    public virtual ICollection<Category>? Children { get; set; }
}
