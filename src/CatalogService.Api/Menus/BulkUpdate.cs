using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Menus;

public class BulkUpdate : BaseEndpoint<BulkUpdateMenusRequest, ListMenuResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.PUT);
        Routes(CatalogServiceApiRoutes.MenuRoutes.Bulk);
        Tags([CatalogServiceApiTags.Menu]);
        Options(c => c.WithTags([CatalogServiceApiTags.Menu]));
        AllowAnonymous();
        Summary(s => s.Summary = "Bulk update menus, including sort order");
    }
}
