using Framework.BuildingBlock.Application.Contracts;
using Volo.Abp.Application.Dtos;

namespace CatalogService.BusinessTypes;

public class DeleteBusinessTypeRequest : EntityDto<long>, IFrameworkRequest<BaseResponse>
{
}
