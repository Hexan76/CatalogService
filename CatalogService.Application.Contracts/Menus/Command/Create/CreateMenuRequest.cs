using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class CreateMenuRequest : IFrameworkRequest<MenuModel>
{
    public string Name { get; set; } = default!;
    public int SortOrder { get; set; }
    public Guid VendorId { get; set; }

}
