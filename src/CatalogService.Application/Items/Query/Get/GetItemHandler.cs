using AutoMapper;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class GetItemHandler(IItemRepository itemRepository, IMapper mapper) : IGetItemHandler
{
    public async Task<MessageContract<ItemModel>> Handle(GetItemRequest request, CancellationToken cancellationToken)
    {
        var founded = await itemRepository.FindAsync(request.Id);

        var response = mapper.Map<Item, ItemModel>(founded);


        return MessageContract<ItemModel>.Success(response);

    }
}
