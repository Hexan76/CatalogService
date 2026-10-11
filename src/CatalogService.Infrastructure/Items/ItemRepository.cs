using System.Linq.Dynamic.Core;

using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.Domain.Shared;
using Framework.BuildingBlock.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Items;

public class ItemRepository : EfGenericRepository<CatalogServiceDbContext, Item, long>, IItemRepository
{
    public ItemRepository(CatalogServiceDbContext context) : base(context)
    {
    }

    public Task<Item?> FindWithDetailsAsync(long id, CancellationToken cancellationToken = default)
        => GetQueryable()
            .Include(x => x.ItemCategories)
            .Include(x => x.MenuItems)
            .Include(x => x.Customizations)
            .Include(x => x.Variants)
                .ThenInclude(x => x.Customizations)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public override async Task<PagedResult<Item>> PaginationPagingAsync(FilterGroup filterGroup, IQueryable<Item>? externalQuery, int page = 1, int pageSize = 10, string sort = "")
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        var query = externalQuery ?? GetQueryable()
            .AsNoTracking()
            .Include(x => x.Variants)
            .ApplyFilter(filterGroup)
            .ApplySort(sort);

        var totalCount = await query.CountAsync();

        var skip = (page - 1) * pageSize;

        var pagedQuery = query
            .Skip(skip)
            .Take(pageSize);

        var pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

        return new PagedResult<Item>
        {
            Queryable = pagedQuery,
            RowCount = totalCount,
            PageCount = pageCount,
            CurrentPage = page,
            PageSize = pageSize
        };
    }
}
