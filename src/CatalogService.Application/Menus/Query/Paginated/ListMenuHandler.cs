using AutoMapper;

using Framework.BuildingBlock.Application.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Menus;

public class ListMenuHandler(IMenuRepository menuRepository, IMapper mapper) : IListMenuHandler
{
    public async Task<MessageContract<ListMenuResponse>> Handle(ListMenuRequest request, CancellationToken cancellationToken)
    {
        var result = await menuRepository.GetQueryable()
            .Where(x => x.VendorId == request.VendorId)
            .ToListAsync();

        var mappedList = mapper.Map<List<MenuModel>>(result);


        return MessageContract<ListMenuResponse>.Success(new ListMenuResponse() { Items = mappedList });
    }
}
