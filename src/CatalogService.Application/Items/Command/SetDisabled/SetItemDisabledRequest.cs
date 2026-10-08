using Framework.BuildingBlock.Application.Contracts;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Items;

public class SetItemDisabledRequest : EntityDto<long>, IFrameworkRequest<ItemModel>
{
    public bool IsDisabled { get; set; }
}
