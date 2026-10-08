using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public interface ISetCustomizationDisabledHandler : IFrameworkRequestHandler<SetCustomizationDisabledRequest, CustomizationModel>
{
}
