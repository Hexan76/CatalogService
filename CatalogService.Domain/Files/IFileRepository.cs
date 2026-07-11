using Framework.BuildingBlock.Repositories;

namespace CatalogService.Files;

public interface IFileRepository : IRepositoryFramework<FileEntity, long>
{
    Task<Dictionary<long, FileEntity>> GetPrimaryFilesAsync(
           IEnumerable<long> entityIds,
           CancellationToken cancellationToken = default);
}
