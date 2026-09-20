using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Customizations;

public class GetCustomizationGroupsByVendor
    : BaseEndpoint<GetCustomizationGroupsByVendorRequest, GetCustomizationGroupsByVendorResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.CustomizationsGroupRoutes.GetByVendor);
        Tags([CatalogServiceApiTags.CustomizationGroup]);
        Options(c => c.WithTags([CatalogServiceApiTags.CustomizationGroup]));
        AllowAnonymous();
        Summary(s => s.Summary = "Get customization groups that belong to a vendor");
    }
}
