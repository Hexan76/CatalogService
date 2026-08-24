using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Customizations;

public class Create : BaseEndpoint<CreateCustomizationRequest, CustomizationModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.POST);
        Routes(CatalogServiceApiRoutes.CustomizationRoutes.Default);
        Tags([CatalogServiceApiTags.Customization]);
        Options(c => c.WithTags([CatalogServiceApiTags.Customization]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
