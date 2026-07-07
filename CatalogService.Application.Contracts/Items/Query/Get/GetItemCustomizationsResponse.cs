using CatalogService.Customizations;

namespace CatalogService.Items;

public class GetItemCustomizationsResponse
{
    public ICollection<CustomizationGroupModel> Items { get; set; } = [];
}
