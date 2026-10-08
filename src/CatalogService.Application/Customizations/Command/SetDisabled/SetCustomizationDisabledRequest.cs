using Framework.BuildingBlock.Application.Contracts;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Customizations;

public class SetCustomizationDisabledRequest : EntityDto<long>, IFrameworkRequest<CustomizationModel>
{
    public bool IsDisabled { get; set; }
}
