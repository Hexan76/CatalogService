using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public class BusinessTypeRoutes : BaseRoutes
{
    public BusinessTypeRoutes(string Prefix, string routeBase) : base(Prefix, routeBase)
    {
        List = $"{Default}/list";
    }
    public readonly string List;
}
