using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Customizations;

public class Search : BaseEndpoint<SearchCustomizationRequest, SearchCustomizationResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.CustomizationRoutes.Search);
        Tags([CatalogServiceApiTags.Customization]);
        Options(c => c.WithTags([CatalogServiceApiTags.Customization]));
        AllowAnonymous();
        Summary(s => s.Summary = "Search all customizations for a vendor");
    }
}
