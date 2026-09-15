namespace CatalogService.PricingService;

public class GetPricesByIdsResponse
{
    public ICollection<GetCurrentPriceResponse> Items { get; set; } = [];
}
