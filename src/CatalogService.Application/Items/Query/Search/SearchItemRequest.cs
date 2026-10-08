using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class SearchItemRequest : PagedAndResultRequestFramework, IFrameworkRequest<SearchItemResponse>
{
    public long? VendorId { get; set; }
    public long? MenuId { get; set; }
    public string? SearchTerm { get; set; }
    public long? BusinessTypeId { get; set; }
    public bool IsDisabled { get; set; } = false;


}
