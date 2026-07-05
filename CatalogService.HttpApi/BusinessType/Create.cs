namespace CatalogService.BusinessTypes;

public class Create : BaseEndpoint<CreateBusinessTypeRequest, BusinessTypeModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.POST);
        Validator<CreateBusinessTypeModelValidation>();
        Routes(CatalogServiceApiRoutes.BusinessTypeRoutes.Default);
        Tags([CatalogServiceApiTags.BusinessType]);
        Options(c => c.WithTags([CatalogServiceApiTags.BusinessType]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
