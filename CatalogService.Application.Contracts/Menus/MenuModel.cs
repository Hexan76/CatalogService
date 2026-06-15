using Volo.Abp.Application.Dtos;

namespace CatalogService.Menus;

public class MenuModel : EntityDto<Guid>
{
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public bool IsDisabled { get; set; }

}
