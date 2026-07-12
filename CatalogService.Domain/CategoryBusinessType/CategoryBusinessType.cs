using System.ComponentModel.DataAnnotations.Schema;

using CatalogService.BusinessTypes;

using Volo.Abp.Domain.Entities;

namespace CatalogService.Categories;

[Table(nameof(CategoryBusinessType))]
public class CategoryBusinessType : Entity<long>
{
    public virtual Category Category { get; set; }
    public virtual long CategoryId { get; set; }
    public virtual BusinessType BusinessType { get; set; }
    public virtual long BusinessTypeId { get; set; }
}
