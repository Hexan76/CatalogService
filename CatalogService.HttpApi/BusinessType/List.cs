namespace CatalogService.BusinessTypes;

public class List : BaseEndpointWithoutRequest<ListBusinessTypeRequest, ListBusinessTypeResponse>
{
    public override void Configure()
    {
        //base.Configure();
        Version(1);
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.BusinessTypeRoutes.List);
        Tags([CatalogServiceApiTags.BusinessType]);
        Options(c => c.WithTags([CatalogServiceApiTags.BusinessType]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
