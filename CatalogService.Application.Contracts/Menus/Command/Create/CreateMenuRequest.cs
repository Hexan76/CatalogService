using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class CreateMenuRequest : IFrameworkRequest<MenuModel>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ImageId { get; set; }
    public Guid? ParentId { get; set; }

}
