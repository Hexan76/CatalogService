
using Framework.BuildingBlock.Application.Contracts;
using CatalogService.Application;

namespace CatalogService.Categories;

public class CreateCategoryHandler(ICategoryRepository categoryRepository) : CatalogServiceAppService, ICreateCategoryHandler
{
    public async Task<MessageContract<CategoryModel>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {

        var createItem = ObjectMapper.Map<CreateCategoryRequest, Category>(request);

        var result = await categoryRepository.InsertAsync(createItem);

        var response = ObjectMapper.Map<Category, CategoryModel>(result);

        return new ResultApi<CategoryModel>
        {
            Result = response
        };
    }
}
