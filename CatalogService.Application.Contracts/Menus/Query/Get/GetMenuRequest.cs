using Framework.BuildingBlock.Application.Contracts;
using Volo.Abp.Application.Dtos;

namespace CatalogService.Menus;

public class GetMenuRequest : EntityDto<Guid>, IFrameworkRequest<MenuModel>
{

}
