using CatalogService.Application;
using Framework.BuildingBlock.Application;
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class PagiantedCategoryHandler(ICategoryRepository categoryRepository) : CatalogServiceAppService, IPaginatedCategoryHandler
{
    public async Task<MessageContract<PaginatedCategoryResponse>> Handle(PaginatedCategoryRequest request, CancellationToken cancellationToken)
    {
        var paged = await categoryRepository.PaginationPagingAsync(request.FilterGroup.ToDomain(), request.Page, request.PageSize);

        var result = await AsyncExecuter.ToListAsync(paged.Queryable);

        var response = new PaginatedCategoryResponse();

        ObjectMapper.Map(result,response.Items);

        response.TotalCount = paged.RowCount;

        return new ResultApi<PaginatedCategoryResponse>
        {
            Result = response
        };
    }
}
