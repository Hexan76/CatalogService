using Volo.Abp.Application.Dtos;

namespace CatalogService.Items;

public class ReSortMenuItemResult : EntityDto<long>
{
    public long MenuId { get; set; }
    public int SortOrder { get; set; }
}
