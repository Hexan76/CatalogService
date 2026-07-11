using Framework.BuildingBlock.Application.Contracts;
using Volo.Abp.Application.Dtos;

namespace CatalogService.BusinessTypes;

public class GetBusinessTypeRequest : EntityDto<long>, IFrameworkRequest<BusinessTypeModel>
{

}
