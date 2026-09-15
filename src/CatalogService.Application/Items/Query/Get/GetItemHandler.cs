using AutoMapper;

using CatalogService.PricingService;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class GetItemHandler(
    IItemRepository itemRepository,
    IMapper mapper,
    CurrentPriceClient currentPriceClient) : IGetItemHandler
{
    public async Task<MessageContract<ItemModel>> Handle(GetItemRequest request, CancellationToken cancellationToken)
    {
        var founded = await itemRepository.FindAsync(request.Id);

        var response = mapper.Map<Item, ItemModel>(founded);

        if (response is not null)
        {
            await currentPriceClient.ApplyAsync(response, cancellationToken);
        }

        return MessageContract<ItemModel>.Success(response);

    }
}
