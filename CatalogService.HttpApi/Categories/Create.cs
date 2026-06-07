using CatalogService.Categories;
using CatalogService.Constants;
using Framework.BuildingBlock.HttpApi;
using Microsoft.AspNetCore.Http;

namespace CatalogService.Locations;

public class Create : BaseEndpoint<CreateCategoryRequest, CategoryModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.POST);
        Routes(CatalogServiceApiRoutes.CategoryRoutes.Default);
        Tags([CatalogServiceApiTags.Category]);
        Options(c => c.WithTags([CatalogServiceApiTags.Category]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
