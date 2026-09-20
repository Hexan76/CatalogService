using CatalogService.Customizations;
using CatalogService.Items;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.PricingService;

public class CurrentPriceClient(IHttpClientService httpClientService)
{
    public const string DefaultCurrency = "AFN";

    public async Task ApplyAsync(ItemModel item, CancellationToken cancellationToken = default)
    {
        if (item is null)
        {
            return;
        }

        await ApplyAsync([item], cancellationToken);
    }

    public async Task ApplyAsync(IEnumerable<ItemModel> items, CancellationToken cancellationToken = default)
    {
        var list = items.Where(x => x is not null).ToList();
        if (list.Count == 0)
        {
            return;
        }

        var ids = list.SelectMany(CollectIds).Distinct().Where(id => id > 0).ToList();
        var prices = await GetByIdsAsync(ids, cancellationToken);

        foreach (var item in list)
        {
            ApplyToItem(item, prices);
        }
    }

    public async Task ApplyAsync(SearchItemModel item, CancellationToken cancellationToken = default)
    {
        if (item is null)
        {
            return;
        }

        await ApplyAsync([item], cancellationToken);
    }

    public async Task ApplyAsync(IEnumerable<SearchItemModel> items, CancellationToken cancellationToken = default)
    {
        var list = items.Where(x => x is not null).ToList();
        if (list.Count == 0)
        {
            return;
        }

        var ids = list.SelectMany(CollectIds).Distinct().Where(id => id > 0).ToList();
        var prices = await GetByIdsAsync(ids, cancellationToken);

        foreach (var item in list)
        {
            ApplyToSearchItem(item, prices);
        }
    }

    public async Task ApplyAsync(
        IEnumerable<CustomizationGroupModel> groups,
        long vendorId,
        CancellationToken cancellationToken = default)
    {
        var groupList = groups.ToList();
        var ids = groupList
            .SelectMany(g => g.Customizations)
            .Select(c => c.Id)
            .Where(id => id > 0)
            .Distinct()
            .ToList();

        var prices = await GetByIdsAsync(ids, cancellationToken);

        foreach (var group in groupList)
        {
            foreach (var customization in group.Customizations)
            {
                var amount = ResolveAmount(prices, customization.Id, vendorId);
                if (amount.HasValue)
                {
                    customization.Price = amount.Value;
                }
            }
        }
    }

    private async Task<List<GetCurrentPriceResponse>> GetByIdsAsync(
        List<long> ids,
        CancellationToken cancellationToken)
    {
        if (ids.Count == 0)
        {
            return [];
        }

        try
        {
            var response = await httpClientService.SendAsync<ApiResult<GetPricesByIdsResponse>>(
                new GetPricesByIdsRequest { Ids = ids },
                cancellationToken,
                ResponseType.Custom,
                "PricingClient");

            return response?.Result?.Items?.ToList() ?? [];
        }
        catch
        {
            return [];
        }
    }

    private static IEnumerable<long> CollectIds(ItemModel item)
    {
        yield return item.Id;

        foreach (var variant in item.Variants)
        {
            yield return variant.Id;
        }

        foreach (var group in item.CustomizationGroups)
        {
            foreach (var customization in group.Customizations)
            {
                yield return customization.Id;
            }
        }
    }

    private static IEnumerable<long> CollectIds(SearchItemModel item)
    {
        yield return item.Id;

        foreach (var variant in item.Variants)
        {
            yield return variant.Id;
        }
    }

    private static void ApplyToItem(ItemModel item, List<GetCurrentPriceResponse> prices)
    {
        var amount = ResolveAmount(prices, item.Id, item.VendorId);
        if (amount.HasValue)
        {
            item.Price = (double)amount.Value;
        }

        foreach (var variant in item.Variants)
        {
            var vendorId = variant.VendorId != 0 ? variant.VendorId : item.VendorId;
            var variantAmount = ResolveAmount(prices, variant.Id, vendorId);
            if (variantAmount.HasValue)
            {
                variant.Price = (double)variantAmount.Value;
            }
        }

        foreach (var group in item.CustomizationGroups)
        {
            foreach (var customization in group.Customizations)
            {
                var customizationAmount = ResolveAmount(prices, customization.Id, item.VendorId);
                if (customizationAmount.HasValue)
                {
                    customization.Price = customizationAmount.Value;
                }
            }
        }
    }

    private static void ApplyToSearchItem(SearchItemModel item, List<GetCurrentPriceResponse> prices)
    {
        var amount = ResolveAmount(prices, item.Id, item.VendorId);
        if (amount.HasValue)
        {
            item.Price = amount.Value;
        }

        foreach (var variant in item.Variants)
        {
            var vendorId = variant.VendorId != 0 ? variant.VendorId : item.VendorId;
            var variantAmount = ResolveAmount(prices, variant.Id, vendorId);
            if (variantAmount.HasValue)
            {
                variant.Price = (double)variantAmount.Value;
            }
        }
    }

    private static decimal? ResolveAmount(
        List<GetCurrentPriceResponse> prices,
        long entityId,
        long vendorId)
    {
        var matches = prices.Where(x => x.EntityId == entityId).ToList();
        if (matches.Count == 0)
        {
            return null;
        }

        var match =
            matches.FirstOrDefault(x =>
                x.VendorId == vendorId &&
                string.Equals(x.Currency, DefaultCurrency, StringComparison.OrdinalIgnoreCase))
            ?? matches.FirstOrDefault(x =>
                string.Equals(x.Currency, DefaultCurrency, StringComparison.OrdinalIgnoreCase))
            ?? matches.FirstOrDefault(x => x.VendorId == vendorId)
            ?? matches.FirstOrDefault();

        return match?.Amount;
    }
}
