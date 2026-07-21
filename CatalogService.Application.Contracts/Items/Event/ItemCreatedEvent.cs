using CatalogService.Constants;

using Framework.RabbitMQ;

namespace CatalogService.Items;

[RabbitExchange(BrokerExchanges.CatalogEvents)]
[RabbitRoutingKey("item.created")]
public record ItemCreatedEvent(ItemModel item)
{
}

