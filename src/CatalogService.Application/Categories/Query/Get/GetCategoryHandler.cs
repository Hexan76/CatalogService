using AutoMapper;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class GetCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IGetCategoryHandler
{
    public async Task<MessageContract<CategoryModel>> Handle(GetCategoryRequest request, CancellationToken cancellationToken)
    {
        var founded = await categoryRepository.FindAsync(request.Id);

        var response = mapper.Map<Category, CategoryModel>(founded);


        return MessageContract<CategoryModel>.Success(response);

    }
}
