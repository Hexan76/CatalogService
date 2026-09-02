using CatalogService.PricingService;

using Framework.BuildingBlock.Application.Contracts;

using MediatR;

namespace CatalogService.Items;

public class GetItemWithPricingHandler(
    IMediator mediator,
    CurrentPriceClient currentPriceClient) : IGetItemWithPricingHandler
{
    public async Task<MessageContract<ItemModel>> Handle(
        GetItemWithPricingRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(
            new GetItemRequest { Id = request.Id },
            cancellationToken);

        if (response.Result is not null)
        {
            await currentPriceClient.ApplyAsync(response.Result, cancellationToken);
        }

        return response;
    }
}
