using AutoMapper;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class BulkUpdateMenusHandler(MenuManager menuManager, IMapper mapper) : IBulkUpdateMenusHandler
{
    public async Task<MessageContract<ListMenuResponse>> Handle(BulkUpdateMenusRequest request, CancellationToken cancellationToken)
    {
        var updates = request.Menus
            .Select(x => new MenuBulkUpdate(x.Id, x.Name, x.SortOrder, x.VendorId, x.IsDisabled))
            .ToList();

        var menus = await menuManager.BulkUpdateAsync(updates, cancellationToken);
        var response = mapper.Map<List<MenuModel>>(menus);

        return MessageContract<ListMenuResponse>.Success(new ListMenuResponse
        {
            Items = response
        });
    }
}
