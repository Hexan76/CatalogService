using Framework.BuildingBlock.Repositories;

namespace CatalogService.Files;

public interface IFileRepository : IRepositoryFramework<FileEntity, Guid>
{
}
