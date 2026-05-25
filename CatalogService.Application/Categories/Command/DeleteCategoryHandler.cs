
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class DeleteCategoryHandler(ICategoryRepository categoryRepository) : IDeleteCategoryHandler
{
    public async Task<MessageContract<BaseResponse>> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        var founded = await categoryRepository.GetAsync(request.Id);

        await categoryRepository.DeleteAsync(founded);

        return new ResultApi<BaseResponse>
        {
            Result = new BaseResponse
            {
                Id = request.Id
            },

        };
    }
}
