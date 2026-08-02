using CatalogService.Application;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class ListMenuHandler(IMenuRepository menuRepository) : CatalogServiceAppService, IListMenuHandler
{
    public async Task<MessageContract<ListMenuResponse>> Handle(ListMenuRequest request, CancellationToken cancellationToken)
    {
        var result = await menuRepository.GetListAsync();

        var response = new ListMenuResponse();

        var mappedList = new List<MenuModel>();


        return MessageContract<ListMenuResponse>.Success(response);
    }
}
