using Framework.RabbitMQ;

using Microsoft.Extensions.Logging;

namespace CatalogService.Items.Command;

[RabbitQueue("items.created")]
[RabbitExchange("items.events")]
[RabbitRoutingKey("items.created")]
public class ItemConsumerEvent(ILogger<ItemConsumerEvent> logger) : IMessageConsumer<ItemCreatedEvent2>
{
    public Task ConsumeAsync(Message<ItemCreatedEvent2> message, ConsumeContext context, CancellationToken cancellationToken = default)
    {
        logger.LogInformation($"ItemId:{message.Payload.ItemId} - ItemName:{message.Payload.ItemName}");
        return Task.CompletedTask;
    }
}

public class ItemCreatedEvent2
{
    public long ItemId { get; init; }

    public string ItemName { get; init; } = string.Empty;
}
