
using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Menus;

public class Get : BaseEndpoint<GetMenuRequest, MenuModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.MenuRoutes.Get);
        Tags([CatalogServiceApiTags.Menu]);
        Options(c => c.WithTags([CatalogServiceApiTags.Menu]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
