using CatalogService.Files;

using Volo.Abp.DependencyInjection;

namespace CatalogService.Categories;

public class FileManager(IFileRepository fileRepository) : ITransientDependency
{
    public async Task<FileEntity> AddRelatedFileWithEntity(Guid? fileTempId, Guid entityId, string entityType, string url, string fileName, string role)
    {
        var item = new FileEntity()
        {
            FileName = fileName,
            EntityType = entityType,
            EntityId = entityId,
            Url = url,
            Role = role,
        };
        return await fileRepository.InsertAsync(item);
    }
}
