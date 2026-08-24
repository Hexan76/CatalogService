using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Customizations;

public class GetItemCustomizationsByVendor : BaseEndpoint<GetItemCustomizationsByVendorRequest, GetItemCustomizationsByVendorResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.CustomizationRoutes.GetVendorCustomizations);
        Tags([CatalogServiceApiTags.Customization]);
        Options(c => c.WithTags([CatalogServiceApiTags.Customization]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
