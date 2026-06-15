
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class DeleteItemHandler(IItemRepository itemRepository) : IDeleteItemHandler
{
    public async Task<MessageContract<BaseResponse>> Handle(DeleteItemRequest request, CancellationToken cancellationToken)
    {
        var founded = await itemRepository.GetAsync(request.Id);

        await itemRepository.DeleteAsync(founded);

        return MessageContract<BaseResponse>.Success(new BaseResponse() { Id = request.Id });

    }
}
