using CatalogService.Customizations;
using CatalogService.Items;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.PricingService;

public class CurrentPriceClient(IHttpClientService httpClientService)
{
    public const string DefaultCurrency = "AFN";

    public async Task<decimal?> GetAmountAsync(
        long vendorId,
        long entityId,
        CancellationToken cancellationToken = default)
    {
        if (vendorId <= 0 || entityId <= 0)
        {
            return null;
        }

        try
        {
            var response = await httpClientService.SendAsync<ApiResult<GetCurrentPriceResponse>>(
                new GetCurrentPriceRequest
                {
                    VendorId = vendorId,
                    EntityId = entityId,
                    Currency = DefaultCurrency
                },
                cancellationToken,
                ResponseType.Custom,
                "PricingClient");

            return response?.Result?.Amount;
        }
        catch
        {
            return null;
        }
    }

    public async Task ApplyAsync(ItemModel item, CancellationToken cancellationToken = default)
    {
        var amount = await GetAmountAsync(item.VendorId, item.Id, cancellationToken);
        if (amount.HasValue)
        {
            item.Price = (double)amount.Value;
        }

        foreach (var variant in item.Variants)
        {
            var vendorId = variant.VendorId != 0 ? variant.VendorId : item.VendorId;
            var variantAmount = await GetAmountAsync(vendorId, variant.Id, cancellationToken);
            if (variantAmount.HasValue)
            {
                variant.Price = (double)variantAmount.Value;
            }
        }

        foreach (var group in item.CustomizationGroups)
        {
            foreach (var customization in group.Customizations)
            {
                var customizationAmount = await GetAmountAsync(item.VendorId, customization.Id, cancellationToken);
                if (customizationAmount.HasValue)
                {
                    customization.Price = customizationAmount.Value;
                }
            }
        }
    }

    public async Task ApplyAsync(SearchItemModel item, CancellationToken cancellationToken = default)
    {
        var amount = await GetAmountAsync(item.VendorId, item.Id, cancellationToken);
        if (amount.HasValue)
        {
            item.Price = amount.Value;
        }

        foreach (var variant in item.Variants)
        {
            var vendorId = variant.VendorId != 0 ? variant.VendorId : item.VendorId;
            var variantAmount = await GetAmountAsync(vendorId, variant.Id, cancellationToken);
            if (variantAmount.HasValue)
            {
                variant.Price = (double)variantAmount.Value;
            }
        }
    }

    public async Task ApplyAsync(
        IEnumerable<CustomizationGroupModel> groups,
        long vendorId,
        CancellationToken cancellationToken = default)
    {
        foreach (var group in groups)
        {
            foreach (var customization in group.Customizations)
            {
                var amount = await GetAmountAsync(vendorId, customization.Id, cancellationToken);
                if (amount.HasValue)
                {
                    customization.Price = amount.Value;
                }
            }
        }
    }
}
