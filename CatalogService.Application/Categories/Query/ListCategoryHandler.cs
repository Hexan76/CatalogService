using CatalogService.Application;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class ListCategoryHandler(ICategoryRepository categoryRepository) : CatalogServiceAppService, IPaginatedCategoryHandler
{
    public async Task<MessageContract<ListCategoryResponse>> Handle(ListCategoryRequest request, CancellationToken cancellationToken)
    {
        var query = await categoryRepository.GetListWithFilesAsync(request.ParentId);

        var result = await AsyncExecuter.ToListAsync(query.Queryable);

        var response = new ListCategoryResponse();

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

        return MessageContract<ListCategoryResponse>.Success(response);
    }
}
