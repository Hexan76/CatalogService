using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class Delete : BaseEndpoint<DeleteMenuRequest, BaseResponse>
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
