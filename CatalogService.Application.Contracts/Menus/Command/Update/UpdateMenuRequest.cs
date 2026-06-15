using Framework.BuildingBlock.Application.Contracts;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Menus;

public class UpdateMenuRequest : EntityDto<Guid>, IFrameworkRequest<MenuModel>
{
    public string Name { get; set; } = default!;
    public int SortOrder { get; set; }
}
