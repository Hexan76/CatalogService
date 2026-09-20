namespace CatalogService.Customizations;

public class GetCustomizationGroupsByVendorResponse
{
    public ICollection<CustomizationGroupModel> Items { get; set; } = [];
}
