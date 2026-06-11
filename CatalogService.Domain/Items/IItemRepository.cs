using Volo.Abp.Domain.Repositories;

namespace CatalogService.Items;

public interface IItemRepository : IRepository<Item, Guid>
{
}
