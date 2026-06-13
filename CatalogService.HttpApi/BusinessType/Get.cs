
namespace CatalogService.BusinessTypes;

public class Get : BaseEndpoint<GetBusinessTypeRequest, BusinessTypeModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.BusinessTypeRoutes.Default);
        Tags([CatalogServiceApiTags.BusinessType]);
        Options(c => c.WithTags([CatalogServiceApiTags.BusinessType]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
