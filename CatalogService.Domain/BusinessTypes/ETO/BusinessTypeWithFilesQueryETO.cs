using CatalogService.Files;

namespace CatalogService.BusinessTypes;

public class BusinessTypeWithFilesQueryETO
{
    public BusinessType BusinessType { get; set; } = default!;

    public List<FileEntity> Files { get; set; } = [];
}
