using Volo.Abp.Reflection;

namespace CatalogService.Permissions;

public class CatalogServicePermissions
{
    public const string GroupName = "Service";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CatalogServicePermissions));
    }
}
