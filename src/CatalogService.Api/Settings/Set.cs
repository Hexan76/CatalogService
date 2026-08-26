using CatalogService.Constants;
using CatalogService.Settings.Command;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

namespace CatalogService.Settings;

public class Set : BaseEndpoint<SetSettingsRequest, SettingModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.PUT);
        Routes(CatalogServiceApiRoutes.SettingRoutes.Default);
        Tags([CatalogServiceApiTags.Settings]);
        Options(c => c.WithTags([CatalogServiceApiTags.Settings]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
