using CatalogService.Constants;
using CatalogService.Settings.Query;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

namespace CatalogService.Settings;


public class Get : BaseEndpointWithoutRequest<GetSettingRequest, GetSettingResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.SettingRoutes.Default);
        Tags([CatalogServiceApiTags.Settings]);
        Options(c => c.WithTags([CatalogServiceApiTags.Settings]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
