using CatalogService.Categories;
using CatalogService.Constants;
using Framework.BuildingBlock.HttpApi;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CatalogService.Locations;

public class Update : BaseEndpoint<UpdateCategoryRequest, CategoryModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.PUT);
        Routes(CatalogServiceApiRoutes.CategoryRoutes.Update);
        Tags([CatalogServiceApiTags.Category]);
        Options(c => c.WithTags([CatalogServiceApiTags.Category]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
