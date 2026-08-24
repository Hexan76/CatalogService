using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.Application.Contracts;
using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Items;

public class Delete : BaseEndpoint<DeleteItemRequest, BaseResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.DELETE);
        Routes(CatalogServiceApiRoutes.ItemRoutes.Delete);
        Tags([CatalogServiceApiTags.Item]);
        Options(c => c.WithTags([CatalogServiceApiTags.Item]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
