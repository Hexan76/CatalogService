using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class ReSortMenuItemsRequest : IFrameworkRequest<ReSortMenuItemsResponse>
{
    public long VendorId { get; set; }
    public List<ReSortMenuItem> Items { get; set; } = [];
}
