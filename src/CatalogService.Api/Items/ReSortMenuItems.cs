using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Items;

public class ReSortMenuItems : BaseEndpoint<ReSortMenuItemsRequest, ReSortMenuItemsResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.PATCH);
        Routes(CatalogServiceApiRoutes.ItemRoutes.ReSortMenuItems);
        Tags([CatalogServiceApiTags.Item]);
        Options(c => c.WithTags([CatalogServiceApiTags.Item]));
        AllowAnonymous();
        Summary(s => s.Summary = "Re-sort item priority. Each item keeps its current menu.");
    }
}
