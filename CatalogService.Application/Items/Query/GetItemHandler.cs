using CatalogService.Application;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class GetItemHandler(IItemRepository itemRepository) : CatalogServiceAppService, IGetItemHandler
{
    public async Task<MessageContract<ItemModel>> Handle(GetItemRequest request, CancellationToken cancellationToken)
    {
        var founded = await itemRepository.FindAsync(request.Id);

        var response = ObjectMapper.Map<Item, ItemModel>(founded);


        return MessageContract<ItemModel>.Success(response);

    }
}
