using System.Linq.Dynamic.Core;

using CatalogService.EntityFrameworkCore;
using CatalogService.Files;

using Framework.BuildingBlock.Domain.Shared;
using Framework.BuildingBlock.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Categories;

public class CategoryRepository : EfGenericRepository<CatalogServiceDbContext, Category, long>, ICategoryRepository
{
    private CatalogServiceDbContext _context;
    public CategoryRepository(CatalogServiceDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<PagedResult<CategoryWithFilesQueryETO>> GetListWithFilesAsync(long? parentId)
    {
        var dbContext = _context;

        var categoriesQuery = dbContext.Set<Category>().AsQueryable();


        var query =
            from category in categoriesQuery.Where(x => x.ParentId == parentId)
            join file in dbContext.Set<FileEntity>()
                    .Where(x => x.EntityType == nameof(Category))
                on category.Id equals file.EntityId into files
            select new CategoryWithFilesQueryETO
            {
                Category = category,

                Files = files
                    .OrderBy(x => x.Priority)
                    .ToList()
            };

        return new PagedResult<CategoryWithFilesQueryETO>
        {
            Queryable = query
        };

    }

    public async Task<PagedResult<CategoryWithFilesQueryETO>>
        GetPagedWithFilesAsync(
            FilterGroup filterGroup,
            int page = 1,
            int pageSize = 10,
            string sort = "")
    {
        var dbContext = _context;

        var categoriesQuery = dbContext.Set<Category>().AsQueryable()
                .ApplyFilter(filterGroup)
                .ApplySort(sort);

        var query =
            from category in categoriesQuery
            join file in dbContext.Set<FileEntity>()
                    .Where(x => x.EntityType == nameof(Category))
                on category.Id equals file.EntityId into files
            select new CategoryWithFilesQueryETO
            {
                Category = category,

                Files = files
                    .OrderBy(x => x.Priority)
                    .ToList()
            };

        var totalCount = await categoriesQuery.CountAsync();


        return new PagedResult<CategoryWithFilesQueryETO>
        {
            RowCount = totalCount,
            Queryable = query
        };
    }
}
