namespace CatalogService.Items;

public class Update : BaseEndpoint<UpdateItemRequest, ItemModel>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.PUT);
        Routes(CatalogServiceApiRoutes.ItemRoutes.Update);
        Tags([CatalogServiceApiTags.Item]);
        Options(c => c.WithTags([CatalogServiceApiTags.Item]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
