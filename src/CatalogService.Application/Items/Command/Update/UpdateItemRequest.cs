using System.ComponentModel.DataAnnotations;

using Framework.BuildingBlock.Application.Contracts;

using Volo.Abp.Application.Dtos;

namespace CatalogService.Items;

public class UpdateItemRequest : EntityDto<long>, IFrameworkRequest<ItemModel>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public Guid? ImageId { get; set; }
    public int DiscountPrecent { get; set; }
    public bool HasDiscount { get; set; }
    public int SortOrder { get; set; }
    public long PreparingTime { get; set; }

    [MaxLength(500)]
    public string Ingredients { get; set; } = default!;
    public string[] IngredientsArray { get; set; } = default!;
    public long[] CusomizationIds { get; set; } = [];
    public ICollection<UpdateItemRequest> Variants { get; set; }
}
