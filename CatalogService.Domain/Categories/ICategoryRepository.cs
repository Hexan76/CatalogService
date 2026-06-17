using System.Linq.Dynamic.Core;

using Framework.BuildingBlock.Domain.Shared;
using Framework.BuildingBlock.Repositories;

namespace CatalogService.Categories;

public interface ICategoryRepository : IRepositoryFramework<Category, Guid>
{
    Task<PagedResult<CategoryWithFilesQueryETO>>
    GetPagedWithFilesAsync(
        FilterGroup filterGroup,
        int page = 1,
        int pageSize = 10,
        string sort = "");

    Task<PagedResult<CategoryWithFilesQueryETO>>
    GetListWithFilesAsync(Guid? parentId);
}
