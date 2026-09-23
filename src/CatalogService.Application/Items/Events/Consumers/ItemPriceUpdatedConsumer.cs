using CatalogService.Constants;
using CatalogService.Customizations;
using CatalogService.Events;

using Framework.RabbitMQ;

namespace CatalogService.Items;

[RabbitExchange(BrokerExchanges.InventoryEvents)]
[RabbitQueue(BrokerQueues.InventoryChanged)]
[RabbitRoutingKey(BrokerRoutingKeys.InventoryChanged)]
public class ItemPriceUpdatedConsumer(IItemRepository itemRepository,
                                      ICustomizationRepository customizationRepository,
                                      CustomizationManager customizationManager,
                                      ItemManager itemManager) : IMessageConsumer<InventoryChangedEvent>
{
    public async Task ConsumeAsync(Message<InventoryChangedEvent> message, ConsumeContext context, CancellationToken cancellationToken = default)
    {
        switch (message.Payload.EntityType)
        {
            case "Item":

                var founded = await itemRepository.FindAsync(message.Payload.EntityId);

                if (founded is null)
                    return;

                founded.AvailableQuantity = message.Payload.Quantity;
                founded.IsOutofStock = message.Payload.isOutofStock;
                await itemManager.UpdateAsync(founded);

                break;
            default:
                var foundedCustomization = await customizationRepository.FindAsync(message.Payload.EntityId);

                if (foundedCustomization is null)
                    return;

                foundedCustomization.AvailableQuantity = message.Payload.Quantity;
                await customizationManager.UpdateAsync(foundedCustomization);
                break;
        }

    }
}
