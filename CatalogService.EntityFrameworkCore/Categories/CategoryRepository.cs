using System.Linq.Dynamic.Core;

using CatalogService.EntityFrameworkCore;
using CatalogService.Files;

using Framework.BuildingBlock.Domain.Shared;
using Framework.BuildingBlock.Repositories;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.Categories;

public class CategoryRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, Category, Guid>, ICategoryRepository
{
    public CategoryRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<PagedResult<CategoryWithFilesQueryResult>> GetListWithFilesAsync(Guid? parentId)
    {
        var dbContext = await GetDbContextAsync();

        var categoriesQuery = dbContext.Set<Category>().AsQueryable();


        var query =
            from category in categoriesQuery.Where(x => x.ParentId == parentId)
            join file in dbContext.Set<FileEntity>()
                    .Where(x => x.EntityType == nameof(Category))
                on category.Id equals file.EntityId into files
            select new CategoryWithFilesQueryResult
            {
                Category = category,

                Files = files
                    .OrderBy(x => x.Priority)
                    .ToList()
            };

        var totalCount =
            await AsyncExecuter.CountAsync(categoriesQuery);


        return new PagedResult<CategoryWithFilesQueryResult>
        {
            RowCount = totalCount,
            Queryable = query
        };

    }

    public async Task<PagedResult<CategoryWithFilesQueryResult>>
        GetPagedWithFilesAsync(
            FilterGroup filterGroup,
            int page = 1,
            int pageSize = 10,
            string sort = "")
    {
        var dbContext = await GetDbContextAsync();

        var categoriesQuery = dbContext.Set<Category>().AsQueryable()
                .ApplyFilter(filterGroup)
                .ApplySort(sort);

        var query =
            from category in categoriesQuery
            join file in dbContext.Set<FileEntity>()
                    .Where(x => x.EntityType == nameof(Category))
                on category.Id equals file.EntityId into files
            select new CategoryWithFilesQueryResult
            {
                Category = category,

                Files = files
                    .OrderBy(x => x.Priority)
                    .ToList()
            };

        var totalCount =
            await AsyncExecuter.CountAsync(categoriesQuery);


        return new PagedResult<CategoryWithFilesQueryResult>
        {
            RowCount = totalCount,
            Queryable = query
        };
    }
}
