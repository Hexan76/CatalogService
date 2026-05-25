using Framework.BuildingBlock.Application.Contracts;
using CatalogService.Domain.Shared;
using Volo.Abp.Modularity;

namespace CatalogService.Application.Contracts;

[DependsOn(
    typeof(CatalogServiceDomainSharedModule),
    typeof(BuildingBlockApplicationContractsModule)
    )]
public class CatalogServiceApplicationContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {

    }
}
