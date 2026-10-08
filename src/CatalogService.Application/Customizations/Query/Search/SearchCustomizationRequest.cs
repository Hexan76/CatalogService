using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class SearchCustomizationRequest : PagedAndResultRequestFramework, IFrameworkRequest<SearchCustomizationResponse>
{
    public long VendorId { get; set; }
    public long? GroupId { get; set; }
    public string? SearchTerm { get; set; }
    public bool? IsDisabled { get; set; }
}
