using Framework.BuildingBlock.Domain.Shared;
using Volo.Abp.Domain.Repositories;

namespace CatalogService.MenuItems;

public interface IMenuItemRepository : IRepository<MenuItem, Guid>
{
    Task<(IEnumerable<MenuItem> Items, int Total)> PaginatedFilterAsync(FilterGroup filterGroup, int skip = 0, int totalCount = 10, string sort = "");
    //cusomize method
}
