
//using Framework.BuildingBlock.Application.Contracts;

//namespace CatalogService.Items;

//public class DeleteItemHandler(ICategoryRepository categoryRepository) : IDeleteItemHandler
//{
//    public async Task<MessageContract<BaseResponse>> Handle(DeleteItemRequest request, CancellationToken cancellationToken)
//    {
//        var founded = await categoryRepository.FindAsync(request.Id);

//        await categoryRepository.DeleteAsync(founded);

//        return MessageContract<BaseResponse>.Success(new BaseResponse() { Id = request.Id });

//    }
//}
