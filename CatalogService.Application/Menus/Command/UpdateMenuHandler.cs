using CatalogService.Application;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class UpdateMenuHandler(IMenuRepository menuRepository) : CatalogServiceAppService, IUpdateMenuHandler
{
    public async Task<MessageContract<MenuModel>> Handle(UpdateMenuRequest request, CancellationToken cancellationToken)
    {
        var founded = await menuRepository.GetAsync(request.Id);

        ObjectMapper.Map(request, founded);

        var result = await menuRepository.UpdateAsync(founded);


        var response = ObjectMapper.Map<Menu, MenuModel>(result);

        return MessageContract<MenuModel>.Success(response);
    }
}
