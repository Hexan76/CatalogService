using Framework.BuildingBlock.Application.Contracts;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Customizations;

public class GetCustomizationRequest : EntityDto<long>, IFrameworkRequest<CustomizationModel>
{
}
