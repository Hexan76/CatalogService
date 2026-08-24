using System.Linq.Dynamic.Core;

using Framework.BuildingBlock.Data;
using Framework.BuildingBlock.Domain.Shared;

namespace CatalogService.BusinessTypes;

public interface IBusinessTypeRepository : IGenericRepository<BusinessType, long>
{
    Task<PagedResult<BusinessTypeWithFilesQueryETO>>
    GetPagedWithFilesAsync(
        FilterGroup filterGroup,
        int page = 1,
        int pageSize = 10,
        string sort = "");

    Task<PagedResult<BusinessTypeWithFilesQueryETO>>
    GetListWithFilesAsync();
}
