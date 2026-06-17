using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public class Delete : BaseEndpoint<DeleteBusinessTypeRequest, BaseResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.DELETE);
        Routes(CatalogServiceApiRoutes.BusinessTypeRoutes.Delete);
        Tags([CatalogServiceApiTags.BusinessType]);
        Options(c => c.WithTags([CatalogServiceApiTags.BusinessType]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
