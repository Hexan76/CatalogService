using CatalogService.Localization;

using Framework.BuildingBlock.Application.Contracts;

using Microsoft.EntityFrameworkCore;

using Volo.Abp;

namespace CatalogService.Items;

public class ReSortMenuItemsHandler(IItemRepository itemRepository) : IReSortMenuItemsHandler
{
    public async Task<MessageContract<ReSortMenuItemsResponse>> Handle(
        ReSortMenuItemsRequest request,
        CancellationToken cancellationToken)
    {
        var requested = request.Items ?? [];
        if (requested.Count == 0)
        {
            return Empty();
        }

        if (requested.Any(x => x.SortOrder < 0))
        {
            throw new BusinessException(
                    CatalogServiceErrorCodes.SortOrderCannotBeNegative,
                    CatalogServiceErrorCodes.SortOrderCannotBeNegative)
                .WithResource(typeof(CatalogServiceResources));
        }

        if (requested.Select(x => x.Id).Distinct().Count() != requested.Count)
        {
            throw Duplicate();
        }

        var ids = requested.Select(x => x.Id).ToList();
        var items = await itemRepository.GetQueryable()
            .Include(x => x.MenuItems)
            .Where(x => x.VendorId == request.VendorId && ids.Contains(x.Id))
            .ToListAsync(cancellationToken);

        if (items.Count != ids.Count)
        {
            throw new BusinessException(
                    CatalogServiceErrorCodes.ItemNotFound,
                    CatalogServiceErrorCodes.ItemNotFound)
                .WithResource(typeof(CatalogServiceResources));
        }

        var sortById = requested.ToDictionary(x => x.Id, x => x.SortOrder);

        foreach (var item in items)
        {
            item.SortOrder = sortById[item.Id];
        }

        await itemRepository.UpdateManyAsync(items, true, cancellationToken);

        var response = items
            .Select(item => new ReSortMenuItemResult
            {
                Id = item.Id,
                MenuId = item.MenuItems.FirstOrDefault()?.MenuId ?? 0,
                SortOrder = item.SortOrder
            })
            .OrderBy(x => x.MenuId)
            .ThenBy(x => x.SortOrder)
            .ThenBy(x => x.Id)
            .ToList();

        return MessageContract<ReSortMenuItemsResponse>.Success(new ReSortMenuItemsResponse
        {
            Items = response
        });
    }

    private static MessageContract<ReSortMenuItemsResponse> Empty()
        => MessageContract<ReSortMenuItemsResponse>.Success(new ReSortMenuItemsResponse
        {
            Items = []
        });

    private static BusinessException Duplicate()
        => new BusinessException(
                CatalogServiceErrorCodes.DuplicatePrioritiesNotAllowed,
                CatalogServiceErrorCodes.DuplicatePrioritiesNotAllowed)
            .WithResource(typeof(CatalogServiceResources));
}
