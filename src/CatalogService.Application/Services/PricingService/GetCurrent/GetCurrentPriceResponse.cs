namespace CatalogService.PricingService;

public class GetCurrentPriceResponse
{
    public long Id { get; set; }
    public long VendorId { get; set; }
    public long EntityId { get; set; }
    public PriceEntityType EntityType { get; set; }
    public decimal Amount { get; set; }
    public string Currency { get; set; } = string.Empty;
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
}
