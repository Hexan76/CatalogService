using CatalogService.Constants;

using Framework.RabbitMQ;

namespace CatalogService.Items;

[RabbitExchange(BrokerExchanges.CatalogEvents)]
[RabbitQueue(BrokerQueues.Items)]
[RabbitRoutingKey(BrokerRoutingKeys.Created)]
public record ItemCreatedEvent(ItemModel item)
{
}

