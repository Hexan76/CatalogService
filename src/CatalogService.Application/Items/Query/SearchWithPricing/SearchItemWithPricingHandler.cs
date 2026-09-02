using CatalogService.PricingService;

using Framework.BuildingBlock.Application.Contracts;

using MediatR;

namespace CatalogService.Items;

public class SearchItemWithPricingHandler(
    IMediator mediator,
    CurrentPriceClient currentPriceClient) : ISearchItemWithPricingHandler
{
    public async Task<MessageContract<SearchItemResponse>> Handle(
        SearchItemWithPricingRequest request,
        CancellationToken cancellationToken)
    {
        var response = await mediator.Send(
            new SearchItemRequest
            {
                SearchTerm = request.SearchTerm,
                BusinessTypeId = request.BusinessTypeId,
                Page = request.Page,
                PageSize = request.PageSize
            },
            cancellationToken);

        if (response.Result?.Items is not null)
        {
            foreach (var item in response.Result.Items)
            {
                await currentPriceClient.ApplyAsync(item, cancellationToken);
            }
        }

        return response;
    }
}
