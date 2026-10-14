using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class BulkUpdateMenusRequest : IFrameworkRequest<ListMenuResponse>
{
    public List<MenuModel> Menus { get; set; } = [];
}
