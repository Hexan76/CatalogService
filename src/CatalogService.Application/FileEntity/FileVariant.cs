namespace CatalogService.ObjectStorageService;

public class FileVariant
{
    public string Type { get; set; } = null!;

    public string Url { get; set; } = null!;

    public long Size { get; set; }
}
