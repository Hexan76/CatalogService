using Volo.Abp.Domain.Repositories;

namespace CatalogService.MenuItems;

public interface IMenuItemRepository : IRepository<MenuItem, Guid>
{
}
