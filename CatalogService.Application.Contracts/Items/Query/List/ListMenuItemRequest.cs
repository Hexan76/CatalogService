using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class ListMenuItemRequest : IFrameworkRequest<ListMenuItemResponse>
{
    public Guid VendorId { get; set; }
    public Guid? MenuId { get; set; }
}
