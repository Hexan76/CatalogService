using AutoMapper;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class GetMenuHandler(IMenuRepository menuRepository, IMapper mapper) : IGetMenuHandler
{
    public async Task<MessageContract<MenuModel>> Handle(GetMenuRequest request, CancellationToken cancellationToken)
    {
        var founded = await menuRepository.FindAsync(request.Id);

        var response = mapper.Map<Menu, MenuModel>(founded);


        return MessageContract<MenuModel>.Success(response);

    }
}
