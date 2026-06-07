namespace CatalogService.Locations;

public class List : BaseEndpoint<ListCategoryRequest, ListCategoryResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.POST);
        Routes(CatalogServiceApiRoutes.CategoryRoutes.List);
        Tags([CatalogServiceApiTags.Category]);
        Options(c => c.WithTags([CatalogServiceApiTags.Category]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
