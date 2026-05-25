using Framework.BuildingBlock.Domain;
using CatalogService.Domain.Shared;
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

    }
}
