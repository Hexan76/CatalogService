using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Customizations;

public class SetDisabled : BaseEndpoint<SetCustomizationDisabledRequest, CustomizationModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.PUT);
        Routes(CatalogServiceApiRoutes.CustomizationRoutes.SetDisabled);
        Tags([CatalogServiceApiTags.Customization]);
        Options(c => c.WithTags([CatalogServiceApiTags.Customization]));
        AllowAnonymous();
        Summary(s => s.Summary = "Enable or disable a customization");
    }
}
