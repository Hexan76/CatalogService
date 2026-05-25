using Template.Service.Domain.Shared;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace CatalogService.Permissions;

public class CatalogServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(CatalogServicePermissions.GroupName, L("Permission:Service"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CatalogServiceResource>(name);
    }
}
