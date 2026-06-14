using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class ListItemRequest : IFrameworkRequest<ListItemResponse>
{
    public Guid? ParentId { get; set; }
}
