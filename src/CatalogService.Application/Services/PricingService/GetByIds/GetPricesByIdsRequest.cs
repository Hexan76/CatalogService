using Framework.HttpClient.Abstractions;

namespace CatalogService.PricingService;

public class GetPricesByIdsRequest : IHttpRequest
{
    public HttpMethod Method => HttpMethod.Post;

    public string Route { get; set; } = "api/v1/prices/by-ids";

    public List<long> Ids { get; set; } = [];
}
