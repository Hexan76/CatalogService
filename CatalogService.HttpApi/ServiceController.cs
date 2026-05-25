using Template.Service.Domain.Shared;
using Volo.Abp.AspNetCore.Mvc;

namespace CatalogService.HttpApi;

public abstract class CatalogServiceController : AbpControllerBase
{
    protected CatalogServiceController()
    {
        LocalizationResource = typeof(CatalogServiceResource);
    }
}
