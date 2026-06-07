using Framework.BuildingBlock.Application;
using Microsoft.Extensions.DependencyInjection;
using CatalogService.Application.Contracts;
using CatalogService.Domain;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace CatalogService.Application;

[DependsOn(
    typeof(AbpLuckyPennyAutoMapperModule),
    typeof(BuildingBlockApplicationModule),
    typeof(CatalogServiceDomainModule),
    typeof(CatalogServiceApplicationContractsModule)
    )]
public class CatalogServiceApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<CatalogServiceApplicationModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<CatalogServiceApplicationModule>(validate: true);
        });

        context.Services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssembly(typeof(CatalogServiceApplicationModule).Assembly);
        });

    }
}
