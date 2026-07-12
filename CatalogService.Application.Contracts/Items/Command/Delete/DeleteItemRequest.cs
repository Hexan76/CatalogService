using Framework.BuildingBlock.Application.Contracts;
using Volo.Abp.Application.Dtos;

namespace CatalogService.Items;

public class DeleteItemRequest : EntityDto<long>, IFrameworkRequest<BaseResponse>
{
}
