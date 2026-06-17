using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class SearchItemRequest : PagedAndResultRequestFramework, IFrameworkRequest<SearchItemResponse>
{
    public string? SearchTerm { get; set; }
    public Guid? BusinessTypeId { get; set; }
}
