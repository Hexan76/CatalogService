namespace CatalogService.ObjectStorageService;

public class FileUrlModel
{
    public string Role { get; set; }

    public IDictionary<string, object> Variants { get; set; }
}
