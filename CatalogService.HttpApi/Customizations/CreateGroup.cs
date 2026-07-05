namespace CatalogService.Customizations;

public class CreateGroup : BaseEndpoint<CreateCustomizationGroupRequest, CustomizationGroupModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.POST);
        Routes(CatalogServiceApiRoutes.CustomizationsGroupRoutes.Default);
        Tags([CatalogServiceApiTags.CustomizationGroup]);
        Options(c => c.WithTags([CatalogServiceApiTags.CustomizationGroup]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
