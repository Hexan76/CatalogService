using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items.Query.Get;

public class GetItemsByIdsRequest : IFrameworkRequest<ListItemResponse>
{
    public long[] Ids { get; set; }
}
