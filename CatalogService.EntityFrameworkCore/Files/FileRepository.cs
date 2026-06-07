using CatalogService.EntityFrameworkCore;
using Framework.BuildingBlock.Repositories;
using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Files;

public class FileRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, FileEntity, Guid>, IFileRepository
{
    public FileRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
