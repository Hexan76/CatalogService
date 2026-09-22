using CatalogService.Constants;

using Framework.RabbitMQ;

namespace CatalogService.Events;

[RabbitExchange(BrokerExchanges.InventoryEvents)]
[RabbitQueue(BrokerQueues.InventoryChanged)]
[RabbitRoutingKey(BrokerRoutingKeys.InventoryChanged)]

public record InventoryChangedEvent(
    long VendorId,
    long EntityId,
    string EntityType,
    int Quantity,
    string TransactionType);

