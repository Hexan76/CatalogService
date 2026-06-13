namespace CatalogService.BusinessTypes;

public class Update : BaseEndpoint<UpdateBusinessTypeRequest, BusinessTypeModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.PUT);
        Routes(CatalogServiceApiRoutes.BusinessTypeRoutes.Update);
        Tags([CatalogServiceApiTags.BusinessType]);
        Options(c => c.WithTags([CatalogServiceApiTags.BusinessType]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
