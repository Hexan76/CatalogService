using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Items;

public class SearchItemWithPricing : BaseEndpoint<SearchItemWithPricingRequest, SearchItemResponse>
{
    public override void Configure()
    {
        base.Configure();
        Version(2);
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.ItemRoutes.Search);
        Tags([CatalogServiceApiTags.Item]);
        Options(c => c.WithTags([CatalogServiceApiTags.Item]));
        AllowAnonymous();
        Summary(s => s.Summary = "Search items with prices from the pricing service");
    }
}
