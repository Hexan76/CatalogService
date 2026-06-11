using CatalogService.Application;
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class GetCategoryGetMenuHandlerHandler(ICategoryRepository categoryRepository) : CatalogServiceAppService, IGetMenuHandler
{
    public async Task<MessageContract<MenuModel>> Handle(GetMenuRequest request, CancellationToken cancellationToken)
    {
        var founded = await categoryRepository.FindAsync(request.Id);

        var response = ObjectMapper.Map<Category, CategoryModel>(founded);


        return MessageContract<CategoryModel>.Success(response);
        
    }
}
