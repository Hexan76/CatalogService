namespace CatalogService.Customizations;

public class GetItemCustomizationsResponse
{
    public ICollection<CustomizationGroupModel> Items { get; set; } = [];
}
