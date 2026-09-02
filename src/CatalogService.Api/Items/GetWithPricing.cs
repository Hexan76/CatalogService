using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Items;

public class GetWithPricing : BaseEndpoint<GetItemWithPricingRequest, ItemModel>
{
    public override void Configure()
    {
        base.Configure();
        Version(2);
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.ItemRoutes.Get);
        Tags([CatalogServiceApiTags.Item]);
        Options(c => c.WithTags([CatalogServiceApiTags.Item]));
        AllowAnonymous();
        Summary(s => s.Summary = "Get item by id with prices from the pricing service");
    }
}
