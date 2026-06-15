using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class ListMenuRequest : IFrameworkRequest<ListMenuResponse>
{
    public Guid? ParentId { get; set; }
}
