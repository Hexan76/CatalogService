using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Items;

public class SearchItem : BaseEndpoint<SearchItemRequest, SearchItemResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.ItemRoutes.Search);
        Tags([CatalogServiceApiTags.Item]);
        Options(c => c.WithTags([CatalogServiceApiTags.Item]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
