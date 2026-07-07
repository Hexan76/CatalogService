namespace CatalogService.Customizations;

public class GetItemCustomizationsByVendorResponse
{
    public ICollection<CustomizationGroupModel> Items { get; set; } = [];
}
