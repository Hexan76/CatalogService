using CatalogService.Domain.Shared;
using Template.Service.Domain.Shared;
using Volo.Abp.Application.Services;

namespace CatalogService.Application;

public abstract class CatalogServiceAppService : ApplicationService
{
    protected CatalogServiceAppService()
    {
        LocalizationResource = typeof(CatalogServiceResource);
        ObjectMapperContext = typeof(CatalogServiceApplicationModule);
    }
}
