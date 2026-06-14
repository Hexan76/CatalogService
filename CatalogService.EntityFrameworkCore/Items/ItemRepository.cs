using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

using CatalogService.EntityFrameworkCore;

using Framework.BuildingBlock.Domain.Shared;
using Framework.BuildingBlock.Repositories;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Items;

public class ItemRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, Item, Guid>, IItemRepository
{
    public ItemRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Item>> WithDetailsAsync(params Expression<Func<Item, object>>[] propertySelectors)
    {
        return (await GetQueryableAsync())
            .Include(x => x.MenuItems);
    }

    public override async Task<PagedResult<Item>> PaginationPagingAsync(FilterGroup filterGroup, int page = 1, int pageSize = 10, string sort = "")
    {
        page = page <= 0 ? 1 : page;
        pageSize = pageSize <= 0 ? 10 : pageSize;

        var query = (await GetQueryableAsync())
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
