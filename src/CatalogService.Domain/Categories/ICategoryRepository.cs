using System.Linq.Dynamic.Core;

using Framework.BuildingBlock.Data;
using Framework.BuildingBlock.Domain.Shared;

namespace CatalogService.Categories;

public interface ICategoryRepository : IGenericRepository<Category, long>
{
    Task<PagedResult<CategoryWithFilesQueryETO>>
    GetPagedWithFilesAsync(
        FilterGroup filterGroup,
        int page = 1,
        int pageSize = 10,
        string sort = "");

    Task<PagedResult<CategoryWithFilesQueryETO>>
    GetListWithFilesAsync(long? parentId);
}
