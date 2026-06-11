using System.Linq.Dynamic.Core;

using CatalogService.EntityFrameworkCore;
using CatalogService.Files;

using Framework.BuildingBlock.Domain.Shared;
using Framework.BuildingBlock.Repositories;

using Microsoft.EntityFrameworkCore;

using Volo.Abp.EntityFrameworkCore;

namespace CatalogService.BusinessTypes;

public class BusinessTypeRepository : EfCoreRepositoryFramework<CatalogServiceDbContext, BusinessType, Guid>, IBusinessTypeRepository
{
    public BusinessTypeRepository(IDbContextProvider<CatalogServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<PagedResult<BusinessTypeWithFilesQueryETO>> GetListWithFilesAsync()
    {
        var dbContext = await GetDbContextAsync();

        var categoriesQuery = dbContext.Set<BusinessType>().AsQueryable();


        var query =
            from category in categoriesQuery
            join file in dbContext.Set<FileEntity>()
                    .Where(x => x.EntityType == nameof(BusinessType))
                on category.Id equals file.EntityId into files
            select new BusinessTypeWithFilesQueryETO
            {
                BusinessType = category,

                Files = files
                    .OrderBy(x => x.Priority)
                    .ToList()
            };

        return new PagedResult<BusinessTypeWithFilesQueryETO>
        {
            Queryable = query
        };

    }

    public async Task<PagedResult<BusinessTypeWithFilesQueryETO>>
        GetPagedWithFilesAsync(
            FilterGroup filterGroup,
            int page = 1,
            int pageSize = 10,
            string sort = "")
    {
        var dbContext = await GetDbContextAsync();

        var categoriesQuery = dbContext.Set<BusinessType>().AsQueryable()
                .ApplyFilter(filterGroup)
                .ApplySort(sort);

        var query =
            from category in categoriesQuery
            join file in dbContext.Set<FileEntity>()
                    .Where(x => x.EntityType == nameof(BusinessType))
                on category.Id equals file.EntityId into files
            select new BusinessTypeWithFilesQueryETO
            {
                BusinessType = category,

                Files = files
                    .OrderBy(x => x.Priority)
                    .ToList()
            };

        var totalCount =
            await AsyncExecuter.CountAsync(categoriesQuery);


        return new PagedResult<BusinessTypeWithFilesQueryETO>
        {
            RowCount = totalCount,
            Queryable = query
        };
    }
}
