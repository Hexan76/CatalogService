using AutoMapper;

using CatalogService.Categories;
using CatalogService.Files;
using CatalogService.ObjectStorageService;
using CatalogService.PricingService;

using Framework.BuildingBlock.Application.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Items;

public class ListMenuItemHandler(
    IItemRepository itemRepository,
    IFileRepository fileRepository,
    IMapper mapper,
    CurrentPriceClient currentPriceClient) :  IListMenuItemHandler
{
    public async Task<MessageContract<ListMenuItemResponse>> Handle(ListMenuItemRequest request, CancellationToken cancellationToken)
    {
        var query =  itemRepository.GetQueryable()
            .Where(x => x.VendorId == request.VendorId);
            
        query= query.Include(x => x.MenuItems)
                .ThenInclude(mi => mi.Menu)
            .Include(x => x.Variants)
                .ThenInclude(x => x.MenuItems)
                .ThenInclude(x => x.Menu)
            .Include(x => x.Customizations)
            .ThenInclude(x => x.Customization)
            .ThenInclude(x => x.CustomizationGroup)
            .Include(x => x.ItemCategories)
            .ThenInclude(x => x.Category)
            .Include(x => x.Variants)
            .ThenInclude(x => x.ItemCategories)
            .ThenInclude(x => x.Category);

        if (request.MenuId.HasValue)
        {
            query = query.Where(x => x.MenuItems.Any(mi => mi.MenuId == request.MenuId.Value));
        }

        var items = await query.ToListAsync(cancellationToken);

        var menuGroups = items
            .SelectMany(item => item.MenuItems
                .Where(mi => !request.MenuId.HasValue || mi.MenuId == request.MenuId.Value)
                .Select(mi => new { mi.MenuId, mi.Menu, Item = item }))
            .GroupBy(x => new { x.MenuId, x.Menu.Name, x.Menu.SortOrder })
            .OrderBy(g => g.Key.SortOrder)
            .ThenBy(g => g.Key.MenuId)
            .Select(g => new MenuItemModel
            {
                Id = g.Key.MenuId,
                MenuTitle = g.Key.Name,
                Items = mapper.Map<List<Item>, List<ItemModel>>(
                    g.OrderBy(x => x.Item.SortOrder).ThenBy(x => x.Item.Id).Select(x => x.Item).ToList())
            })
            .ToList();

        var itemIds = menuGroups
            .SelectMany(x => x.Items)
            .Select(x => x.Id)
            .Distinct();

        var files = await fileRepository.GetPrimaryFilesAsync(itemIds, cancellationToken);

        var categoryIds = menuGroups
            .SelectMany(menu => menu.Items)
            .SelectMany(item => item.Categories.Concat(item.Variants.SelectMany(variant => variant.Categories)))
            .Select(category => category.Id)
            .Distinct()
            .ToList();

        foreach (var menu in menuGroups)
        {
            foreach (var item in menu.Items)
            {
                if (files.TryGetValue(item.Id, out var file))
                {
                    item.Image = new FileUrlModel
                    {
                        Role = file.Role,
                        Variants = file.Variants
                    };
                }
            }
        }

        await currentPriceClient.ApplyAsync(
            menuGroups.SelectMany(menu => menu.Items),
            cancellationToken);

        return MessageContract<ListMenuItemResponse>.Success(new ListMenuItemResponse { Items = menuGroups });
    }

}
