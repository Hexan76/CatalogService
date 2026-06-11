using CatalogService.Application;
using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public class GetBusinessTypeHandler(IBusinessTypeRepository businessTypeRepository) : CatalogServiceAppService, IGetBusinessTypeHandler
{
    public async Task<MessageContract<BusinessTypeModel>> Handle(GetBusinessTypeRequest request, CancellationToken cancellationToken)
    {
        var founded = await businessTypeRepository.FindAsync(request.Id);

        var response = ObjectMapper.Map<BusinessType, BusinessTypeModel>(founded);


        return MessageContract<BusinessTypeModel>.Success(response);
        
    }
}
