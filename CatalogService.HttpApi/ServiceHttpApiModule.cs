using Framework.BuildingBlock.HttpApi;
using Microsoft.Extensions.DependencyInjection;
using CatalogService.Application.Contracts;
using Volo.Abp.Modularity;

namespace CatalogService.HttpApi;

[DependsOn(
    typeof(CatalogServiceApplicationContractsModule),
    typeof(BuildingBlockHttpApiModule))]
public class CatalogServiceHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(CatalogServiceHttpApiModule).Assembly);
        });
    }

}
