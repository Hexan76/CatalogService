using Framework.HttpClient.Abstractions;

namespace CatalogService.PricingService;

public class GetCurrentPriceRequest : IHttpRequest
{
    public HttpMethod Method => HttpMethod.Get;

    public string Route { get; set; } = "api/v1/prices/current";

    public long VendorId { get; set; }
    public long EntityId { get; set; }
    public string Currency { get; set; } = CurrentPriceClient.DefaultCurrency;
}
