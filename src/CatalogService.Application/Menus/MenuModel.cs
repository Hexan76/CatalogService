using Volo.Abp.Application.Dtos;

namespace CatalogService.Menus;

public class MenuModel : EntityDto<long>
{
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public long VendorId { get; set; }
    public bool IsDisabled { get; set; }

}
