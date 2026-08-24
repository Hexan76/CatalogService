using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class DeleteMenuRequest : IFrameworkRequest<BaseResponse<long>>
{
    public long Id { get; set; }

}
