using Framework.BuildingBlock.HttpApi;
using Microsoft.AspNetCore.Http;
using CatalogService.Constants;
using CatalogService.Categories;

namespace CatalogService.Locations;

public class Paginated : BaseEndpoint<PaginatedCategoryRequest, PaginatedCategoryResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.POST);
        Routes(CatalogServiceApiRoutes.CategoryRoutes.GetPaginated);
        Tags([CatalogServiceApiTags.Category]);
        Options(c => c.WithTags([CatalogServiceApiTags.Category]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
