using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class SearchItemWithPricingRequest : PagedAndResultRequestFramework, IFrameworkRequest<SearchItemResponse>
{
    public string? SearchTerm { get; set; }
    public long? BusinessTypeId { get; set; }
}
