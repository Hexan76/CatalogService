namespace CatalogService.Constants;

public class BrokerExchanges
{
    public const string CatalogEvents = "catalog.events";
    public const string InventoryEvents = "inventory.events";
}
public class BrokerQueues
{
    public const string Items = "items";
    public const string InventoryChanged = "inventory.changed";
}
public class BrokerRoutingKeys
{
    public const string Created = "created";
    public const string InventoryChanged = "inventory.changed";
}

