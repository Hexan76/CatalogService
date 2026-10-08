using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Customizations;

public class Get : BaseEndpoint<GetCustomizationRequest, CustomizationModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.CustomizationRoutes.Get);
        Tags([CatalogServiceApiTags.Customization]);
        Options(c => c.WithTags([CatalogServiceApiTags.Customization]));
        AllowAnonymous();
        Summary(s => s.Summary = "Get a customization by id");
    }
}
