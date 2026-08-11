using CatalogService.Application;
using CatalogService.Items.Query.Get;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class GetItemsByIdsHandler(IItemRepository itemRepository) : CatalogServiceAppService, IGetItemsByIdsHandler
{
    public async Task<MessageContract<ListItemResponse>> Handle(GetItemsByIdsRequest request, CancellationToken cancellationToken)
    {
        var query = await itemRepository.GetQueryableAsync();

        query = query.Where(x => request.Ids.Contains(x.Id));

        var result = await AsyncExecuter.ToListAsync(query, cancellationToken);


        var response = new ListItemResponse()
        {
            Items = ObjectMapper.Map<List<Item>, List<ItemModel>>(result)
        };


        return MessageContract<ListItemResponse>.Success(response);
    }
}
