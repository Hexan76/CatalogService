using CatalogService.Constants;

using FastEndpoints;

using Framework.BuildingBlock.HttpApi;

using Microsoft.AspNetCore.Http;

namespace CatalogService.BusinessTypes;

public class List : BaseEndpointWithoutRequest<ListBusinessTypeRequest, ListBusinessTypeResponse>
{
    public override void Configure()
    {
        base.Configure();
        Verbs(Http.GET);
        Routes(CatalogServiceApiRoutes.BusinessTypeRoutes.List);
        Tags([CatalogServiceApiTags.BusinessType]);
        Options(c => c.WithTags([CatalogServiceApiTags.BusinessType]));
        //Policies();
        //Permissions();
        AllowAnonymous();
    }
}
