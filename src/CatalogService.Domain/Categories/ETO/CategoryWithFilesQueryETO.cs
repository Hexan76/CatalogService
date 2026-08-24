using CatalogService.Files;

namespace CatalogService.Categories;

public class CategoryWithFilesQueryETO
{
    public Category Category { get; set; } = default!;

    public List<FileEntity> Files { get; set; } = [];
}
