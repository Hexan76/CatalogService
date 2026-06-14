using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class Delete : BaseEndpoint<DeleteCategoryRequest, BaseResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.DELETE);
        Routes(CatalogServiceApiRoutes.CategoryRoutes.Delete);
        Tags([CatalogServiceApiTags.Category]);
        Options(c => c.WithTags([CatalogServiceApiTags.Category]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
