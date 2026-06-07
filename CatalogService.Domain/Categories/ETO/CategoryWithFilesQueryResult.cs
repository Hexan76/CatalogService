using CatalogService.Files;

namespace CatalogService.Categories;

public class CategoryWithFilesQueryResult
{
    public Category Category { get; set; } = default!;

    public List<FileEntity> Files { get; set; } = [];
}
