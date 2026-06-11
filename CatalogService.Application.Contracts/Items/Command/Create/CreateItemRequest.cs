using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class CreateItemRequest : IFrameworkRequest<ItemModel>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ImageId { get; set; }
    public Guid? ParentId { get; set; }

}
