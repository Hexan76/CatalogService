using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public interface ISetItemDisabledHandler : IFrameworkRequestHandler<SetItemDisabledRequest, ItemModel>
{
}
