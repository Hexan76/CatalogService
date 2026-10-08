using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public interface IGetCustomizationHandler : IFrameworkRequestHandler<GetCustomizationRequest, CustomizationModel>
{
}
