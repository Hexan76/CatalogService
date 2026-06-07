namespace CatalogService.ObjectStorageService;

public class FinalizeItemResponse
{
    public Guid Id { get; set; }

    public string FileName { get; set; } = null!;
    public string URL { get; set; } = null!;

    public long Size { get; set; } = 0;
    public string Extension { get; set; } = "";

    public string MimeType { get; set; } = null!;
    public IDictionary<string, object> Variants { get; set; }

}
