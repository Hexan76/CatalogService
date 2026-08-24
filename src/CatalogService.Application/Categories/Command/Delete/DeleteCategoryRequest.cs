using Framework.BuildingBlock.Application.Contracts;
using Volo.Abp.Application.Dtos;

namespace CatalogService.Categories;

public class DeleteCategoryRequest : EntityDto<long>, IFrameworkRequest<BaseResponse>
{
}
