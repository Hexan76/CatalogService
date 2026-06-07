namespace CatalogService.Domain;

public static class CatalogServiceDbProperties
{
    public static string DbTablePrefix { get; set; } = "Service";

    public const string? DbSchema  = null;

    public const string ConnectionStringName = "Write";
}
