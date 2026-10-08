using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Items;

public class SetDisabled : BaseEndpoint<SetItemDisabledRequest, ItemModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.PUT);
        Routes(CatalogServiceApiRoutes.ItemRoutes.SetDisabled);
        Tags([CatalogServiceApiTags.Item]);
        Options(c => c.WithTags([CatalogServiceApiTags.Item]));
        AllowAnonymous();
        Summary(s => s.Summary = "Enable or disable an item");
    }
}
