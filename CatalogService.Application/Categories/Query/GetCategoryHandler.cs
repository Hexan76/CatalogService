using CatalogService.Application;
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class GetCategoryHandler(ICategoryRepository categoryRepository) : CatalogServiceAppService, IGetCategoryHandler
{
    public async Task<MessageContract<CategoryModel>> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
    {
        var founded = await categoryRepository.FindAsync(request.Id);

        var response = ObjectMapper.Map<Category, CategoryModel>(founded);


        return new ResultApi<CategoryModel>
        {
            Result = response
        };
    }
}
