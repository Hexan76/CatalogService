
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public class DeleteCategoryHandler(ICategoryRepository categoryRepository) : IDeleteCategoryHandler
{
    public async Task<MessageContract<BaseResponse>> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        var founded = await categoryRepository.FindAsync(request.Id);

        await categoryRepository.DeleteAsync(founded);

        return MessageContract<BaseResponse>.Success(new BaseResponse() { Id = request.Id });

    }
}
