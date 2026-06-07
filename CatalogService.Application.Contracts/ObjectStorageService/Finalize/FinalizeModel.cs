using CatalogService.ObjectStorages;

namespace CatalogService.ObjectStorageService;

public class FinalizeModel
{
    public Guid Id { get; set; }
    public StorageEntityType StorageEntityType { get; set; }
    public StorageAppType AppType { get; set; }
    public string EntityKey { get; set; }
    public string Role { get; set; }
    public string FileName { get; set; }
    public bool GenerateThumbnail { get; set; }
    public bool Watermark { get; set; }
    public List<ImageSize> Sizes { get; set; }
}
