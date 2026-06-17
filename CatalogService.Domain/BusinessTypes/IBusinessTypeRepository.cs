using System.Linq.Dynamic.Core;

using Framework.BuildingBlock.Domain.Shared;
using Framework.BuildingBlock.Repositories;

namespace CatalogService.BusinessTypes;

public interface IBusinessTypeRepository : IRepositoryFramework<BusinessType, Guid>
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
