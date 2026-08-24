using CatalogService.Constants;
using CatalogService.Items.Query.Get;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Items;

public class GetItemsByIds : BaseEndpoint<GetItemsByIdsRequest, ListItemResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.POST);
        Routes(CatalogServiceApiRoutes.ItemRoutes.GetItemsByIds);
        Tags([CatalogServiceApiTags.Item]);
        Options(c => c.WithTags([CatalogServiceApiTags.Item]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
