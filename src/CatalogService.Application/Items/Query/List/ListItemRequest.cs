using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class ListItemRequest : IFrameworkRequest<ListItemResponse>
{
    public long? ParentId { get; set; }
}
