using Framework.BuildingBlock.Application.Contracts;
using CatalogService.Application;

namespace CatalogService.Categories;

public class UpdateCategoryHandler(ICategoryRepository categoryRepository) : CatalogServiceAppService, IUpdateCategoryHandler
{
    public async Task<MessageContract<CategoryModel>> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {
        var createItem = ObjectMapper.Map<UpdateCategoryRequest, Category>(request);


        var result = await categoryRepository.UpdateAsync(createItem);

        var response = ObjectMapper.Map<Category, CategoryModel>(result);

        return new ResultApi<CategoryModel>
        {
            Result = response
        };
    }
}
