
namespace CatalogService.Items;

public class Get : BaseEndpoint<GetItemRequest, ItemModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.ItemRoutes.Get);
        Tags([CatalogServiceApiTags.Item]);
        Options(c => c.WithTags([CatalogServiceApiTags.Item]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
