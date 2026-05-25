using Framework.BuildingBlock.Domain.Shared;
using Framework.Localization;
using Template.Service.Domain.Shared;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace CatalogService.Domain.Shared;

[DependsOn(
    typeof(BuildingBlockDomainSharedModule)
    )]
public class CatalogServiceDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CatalogServiceDomainSharedModule>();
        });

        Configure<LocalizationResourceOptions>(options =>
        {
            options.AddResource<CatalogServiceResource>();
        });

    }
}
