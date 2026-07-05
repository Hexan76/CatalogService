
namespace CatalogService.Customizations;

public class GetItemCustomizations : BaseEndpoint<GetItemCustomizationsRequest, GetItemCustomizationsResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.CustomizationRoutes.GetItemCustomizations);
        Tags([CatalogServiceApiTags.Customization]);
        Options(c => c.WithTags([CatalogServiceApiTags.Customization]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
