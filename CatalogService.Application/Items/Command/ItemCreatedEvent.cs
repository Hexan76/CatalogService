using Framework.RabbitMQ;

namespace CatalogService.Items.Command;

[RabbitExchange("items.events")]
[RabbitQueue("items.created")]
[RabbitRoutingKey("items.created")]
public sealed class ItemCreatedEvent
{
    public long ItemId { get; init; }

    public string ItemName { get; init; } = string.Empty;
}
