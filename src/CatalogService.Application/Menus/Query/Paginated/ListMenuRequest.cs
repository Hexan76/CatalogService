using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class ListMenuRequest : IFrameworkRequest<ListMenuResponse>
{
    public long VendorId { get; set; }
    public long? ParentId { get; set; }
}
