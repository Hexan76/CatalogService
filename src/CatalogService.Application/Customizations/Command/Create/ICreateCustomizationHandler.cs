using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public interface ICreateCustomizationHandler : IFrameworkRequestHandler<CreateCustomizationRequest, CustomizationModel>
{
}
