using AutoMapper;

using CatalogService.PricingService;

using Framework.BuildingBlock.Application.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Items;

public class GetItemHandler(
    IItemRepository itemRepository,
    IMapper mapper,
    CurrentPriceClient currentPriceClient) : IGetItemHandler
{
    public async Task<MessageContract<ItemModel>> Handle(GetItemRequest request, CancellationToken cancellationToken)
    {
        var founded = await itemRepository.GetQueryable()
            .Include(x => x.MenuItems)
                .ThenInclude(x => x.Menu)
            .Include(x => x.Variants)
                .ThenInclude(x => x.MenuItems)
                .ThenInclude(x => x.Menu)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var response = mapper.Map<Item, ItemModel>(founded);

        if (response is not null)
        {
            await currentPriceClient.ApplyAsync(response, cancellationToken);
        }

        return MessageContract<ItemModel>.Success(response);

    }
}
