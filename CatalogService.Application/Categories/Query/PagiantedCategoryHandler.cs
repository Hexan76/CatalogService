using CatalogService.Application;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application;
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class PagiantedCategoryHandler(ICategoryRepository categoryRepository) : CatalogServiceAppService, IPaginatedCategoryHandler
{
    public async Task<MessageContract<PaginatedCategoryResponse>> Handle(PaginatedCategoryRequest request, CancellationToken cancellationToken)
    {
        var paged = await categoryRepository.GetPagedWithFilesAsync(request.FilterGroup.ToDomain(), request.Page, request.PageSize);
        var result = await AsyncExecuter.ToListAsync(paged.Queryable);

        var response = new PaginatedCategoryResponse();

        var mappedList = new List<CategoryModel>();
        foreach (var item in result)
        {
            var catMap = ObjectMapper.Map<Category, CategoryModel>(item.Category);

            var files = item.Files
                .OrderBy(x => x.Priority);

            foreach (var file in files)
            {
                catMap.Image = new FileUrlModel
                {
                    Role = file.Role,
                    Variants = file.Variants,
                };

            }

            mappedList.Add(catMap);
        }
        response.Items = mappedList;

        response.TotalCount = paged.RowCount;

        return MessageContract<PaginatedCategoryResponse>.Success(response);
    }
}
