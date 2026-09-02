using Framework.HttpClient.Abstractions;

namespace CatalogService.PricingService;

public class UpsertPriceRequest : IHttpRequest
{
    public HttpMethod Method => HttpMethod.Put;

    public string Route { get; set; } = "api/v1/prices";

    public long VendorId { get; set; }
    public long EntityId { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string? Reason { get; set; }
}
