using CatalogService.Application;
using CatalogService.Categories;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public class GetBusinessTypeHandler(IBusinessTypeRepository businessTypeRepository) : CatalogServiceAppService, IGetBusinessTypeHandler
{
    public async Task<MessageContract<BusinessTypeModel>> Handle(GetBusinessTypeRequest request, CancellationToken cancellationToken)
    {
        var founded = await businessTypeRepository.FindAsync(request.Id, true);

        var response = ObjectMapper.Map<BusinessType, BusinessTypeModel>(founded);
        response.Categories = ObjectMapper.Map<List<Category>, List<CategoryModel>>(founded.CategoryBusinessTypes.Select(x => x.Category).ToList());

        return MessageContract<BusinessTypeModel>.Success(response);

    }
}
