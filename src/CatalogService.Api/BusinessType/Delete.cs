using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.Application.Contracts;
using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

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
