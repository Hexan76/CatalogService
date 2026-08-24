using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.Application.Contracts;
using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Menus;

public class Delete : BaseEndpoint<DeleteMenuRequest, BaseResponse<long>>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.DELETE);
        Routes(CatalogServiceApiRoutes.MenuRoutes.Delete);
        Tags([CatalogServiceApiTags.Menu]);
        Options(c => c.WithTags([CatalogServiceApiTags.Menu]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
