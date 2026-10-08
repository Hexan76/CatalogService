using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public interface ISearchCustomizationHandler : IFrameworkRequestHandler<SearchCustomizationRequest, SearchCustomizationResponse>
{
}
