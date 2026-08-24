using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public interface ICreateCustomizationGroupHandler : IFrameworkRequestHandler<CreateCustomizationGroupRequest, CustomizationGroupModel>
{
}
