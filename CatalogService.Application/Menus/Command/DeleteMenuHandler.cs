
//using Framework.BuildingBlock.Application.Contracts;

//namespace CatalogService.Menus;

//public class DeleteMenuHandler(ICategoryRepository categoryRepository) : IDeleteMenuHandler
//{
//    public async Task<MessageContract<BaseResponse>> Handle(DeleteMenuRequest request, CancellationToken cancellationToken)
//    {
//        var founded = await categoryRepository.FindAsync(request.Id);

//        await categoryRepository.DeleteAsync(founded);

//        return MessageContract<BaseResponse>.Success(new BaseResponse() { Id = request.Id });

//    }
//}
