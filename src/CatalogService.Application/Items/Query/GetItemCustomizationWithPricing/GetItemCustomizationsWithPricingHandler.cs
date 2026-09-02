using CatalogService.PricingService;

using Framework.BuildingBlock.Application.Contracts;

using MediatR;

namespace CatalogService.Items;

public class GetItemCustomizationsWithPricingHandler(
    IMediator mediator,
    IItemRepository itemRepository,
    CurrentPriceClient currentPriceClient) : IGetItemCustomizationsWithPricingHandler
{
    public async Task<MessageContract<GetItemCustomizationsResponse>> Handle(
        GetItemCustomizationsWithPricingRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(
            new GetItemCustomizationsRequest { ItemId = request.ItemId },
            cancellationToken);

        if (response.Result?.Items is { Count: > 0 })
        {
            var item = await itemRepository.FindAsync(request.ItemId, cancellationToken);
            var vendorId = item?.VendorId ?? 0;

            await currentPriceClient.ApplyAsync(response.Result.Items, vendorId, cancellationToken);
        }

        return response;
    }
}
