
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class DeleteMenuHandler(IMenuRepository menuRepository) : IDeleteMenuHandler
{
    public async Task<MessageContract<BaseResponse<long>>> Handle(DeleteMenuRequest request, CancellationToken cancellationToken)
    {
        var founded = await menuRepository.FindAsync(request.Id);

        await menuRepository.DeleteAsync(founded, false, cancellationToken);

        return MessageContract<BaseResponse<long>>.Success(new BaseResponse<long>() { Id = request.Id });

    }
}
