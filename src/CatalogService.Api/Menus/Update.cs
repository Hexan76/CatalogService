using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.Menus;

public class Update : BaseEndpoint<UpdateMenuRequest, MenuModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.PUT);
        Routes(CatalogServiceApiRoutes.MenuRoutes.Update);
        Tags([CatalogServiceApiTags.Menu]);
        Options(c => c.WithTags([CatalogServiceApiTags.Menu]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
