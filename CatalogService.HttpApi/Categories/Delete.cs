using Framework.BuildingBlock.Application.Contracts;
using Framework.BuildingBlock.HttpApi;
using Microsoft.AspNetCore.Http;
using CatalogService.Constants;
using CatalogService.Categories;

namespace CatalogService.Locations;

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
