
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public class DeleteBusinessTypeHandler(IBusinessTypeRepository categoryRepository) : IDeleteBusinessTypeHandler
{
    public async Task<MessageContract<BaseResponse>> Handle(DeleteBusinessTypeRequest request, CancellationToken cancellationToken)
    {
        var founded = await categoryRepository.FindAsync(request.Id);

        await categoryRepository.DeleteAsync(founded);

        return MessageContract<BaseResponse>.Success(new BaseResponse());

    }
}
