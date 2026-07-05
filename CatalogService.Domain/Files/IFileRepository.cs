using Framework.BuildingBlock.Repositories;

namespace CatalogService.Files;

public interface IFileRepository : IRepositoryFramework<FileEntity, Guid>
{
    Task<Dictionary<Guid, FileEntity>> GetPrimaryFilesAsync(
           IEnumerable<Guid> entityIds,
           CancellationToken cancellationToken = default);
}
