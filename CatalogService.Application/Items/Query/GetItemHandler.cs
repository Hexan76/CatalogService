using CatalogService.Application;
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class GetItemHandler(ICategoryRepository categoryRepository) : CatalogServiceAppService, IGetItemHandler
{
    public async Task<MessageContract<ItemModel>> Handle(GetItemRequest request, CancellationToken cancellationToken)
    {
        var founded = await categoryRepository.FindAsync(request.Id);

        var response = ObjectMapper.Map<Category, CategoryModel>(founded);


        return MessageContract<CategoryModel>.Success(response);
        
    }
}
