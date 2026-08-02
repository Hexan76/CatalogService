using CatalogService.Domain.Shared;

using Framework.BuildingBlock.Domain;
using Framework.RabbitMQ;

using Microsoft.Extensions.DependencyInjection;

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
        context.Services.AddHttpClientFramework(context.Configuration);
        
        context.Services.AddFrameworkRabbitMQ(context.Configuration);
    }
}
