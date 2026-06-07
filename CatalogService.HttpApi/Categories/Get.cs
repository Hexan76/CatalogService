
namespace CatalogService.Locations;

public class Get : BaseEndpoint<GetCategoryRequest, CategoryModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.CategoryRoutes.Default);
        Tags([CatalogServiceApiTags.Category]);
        Options(c => c.WithTags([CatalogServiceApiTags.Category]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}