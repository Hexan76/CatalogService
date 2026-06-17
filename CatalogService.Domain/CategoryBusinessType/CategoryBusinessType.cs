using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.BusinessTypes;

using Volo.Abp.Domain.Entities;

namespace CatalogService.Categories;

[Table(nameof(CategoryBusinessType))]
public class CategoryBusinessType : Entity<Guid>
{
    public virtual Category Category { get; set; }
    public virtual Guid CategoryId { get; set; }
    public virtual BusinessType BusinessType { get; set; }
    public virtual Guid BusinessTypeId { get; set; }
}
