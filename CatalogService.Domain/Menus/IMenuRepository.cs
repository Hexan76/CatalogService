using Volo.Abp.Domain.Repositories;

namespace CatalogService.Menus;

public interface IMenuRepository : IRepository<Menu, Guid>
{
}
