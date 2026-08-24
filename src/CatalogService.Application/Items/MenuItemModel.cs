using Volo.Abp.Application.Dtos;

namespace CatalogService.Items;

public class MenuItemModel : EntityDto<long>
{
    public string MenuTitle { get; set; }
    public ICollection<ItemModel> Items { get; set; }
}
