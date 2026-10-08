using AutoMapper;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class SetItemDisabledHandler(ItemManager itemManager, IMapper mapper) : ISetItemDisabledHandler
{
    public async Task<MessageContract<ItemModel>> Handle(SetItemDisabledRequest request, CancellationToken cancellationToken)
    {
        var item = await itemManager.SetDisabledAsync(request.Id, request.IsDisabled);
        var response = mapper.Map<Item, ItemModel>(item);
        return MessageContract<ItemModel>.Success(response);
    }
}
