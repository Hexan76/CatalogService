using AutoMapper;

using CatalogService.Items.Query.Get;

using Framework.BuildingBlock.Application.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Items;

public class GetItemsByIdsHandler(IItemRepository itemRepository, IMapper mapper) : IGetItemsByIdsHandler
{
    public async Task<MessageContract<ListItemResponse>> Handle(GetItemsByIdsRequest request, CancellationToken cancellationToken)
    {
        var query = itemRepository.GetQueryable();

        query = query.Where(x => request.Ids.Contains(x.Id));

        var result = await query.ToListAsync(cancellationToken);


        var response = new ListItemResponse()
        {
            Items = mapper.Map<List<Item>, List<ItemModel>>(result)
        };


        return MessageContract<ListItemResponse>.Success(response);
    }
}
