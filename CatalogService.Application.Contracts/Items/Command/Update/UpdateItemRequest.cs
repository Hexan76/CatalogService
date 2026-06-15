using Framework.BuildingBlock.Application.Contracts;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Items;

public class UpdateItemRequest : EntityDto<Guid>, IFrameworkRequest<ItemModel>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ImageId { get; set; }
    public Guid? ParentId { get; set; }
}
