using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class ListMenuItemRequest : IFrameworkRequest<ListMenuItemResponse>
{
    public long VendorId { get; set; }
    public long? MenuId { get; set; }
}
