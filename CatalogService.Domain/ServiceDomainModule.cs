using CatalogService.Domain.Shared;

using Framework.BuildingBlock.Domain;

using Microsoft.Extensions.DependencyInjection
using Volo.Abp.Modularity;

namespace CatalogService.Domain;

[DependsOn(
    typeof(BuildingBlockDomainModule),
    typeof(CatalogServiceDomainSharedModule)
)]
public class CatalogServiceDomainModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var objectStorageUrl = context.Configuration.GetSection("ObjectStorageService");
        context.Services.AddHttpClientFramework(objectStorageUrl["BaseUrl"]);
        
    }
}
