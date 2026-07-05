using CatalogService.Application;
using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class ListMenuItemHandler(IItemRepository itemRepository, IFileRepository fileRepository) : CatalogServiceAppService, IListMenuItemHandler
{
    public async Task<MessageContract<ListMenuItemResponse>> Handle(ListMenuItemRequest request, CancellationToken cancellationToken)
    {
        var query = (await itemRepository.WithDetailsAsync())
            .Where(x => x.VendorId == request.VendorId);

        if (request.MenuId.HasValue)
        {
            query = query.Where(x => x.MenuItems.Any(mi => mi.MenuId == request.MenuId.Value));
        }

        var items = await AsyncExecuter.ToListAsync(query, cancellationToken);

        var menuGroups = items
            .SelectMany(item => item.MenuItems
                .Where(mi => !request.MenuId.HasValue || mi.MenuId == request.MenuId.Value)
                .Select(mi => new { mi.MenuId, mi.Menu, Item = item }))
            .GroupBy(x => new { x.MenuId, x.Menu.Name })
            .Select(g => new MenuItemModel
            {
                Id = g.Key.MenuId,
                MenuTitle = g.Key.Name,
                Items = ObjectMapper.Map<List<Item>, List<ItemModel>>(g.Select(x => x.Item).ToList())
            })
            .ToList();

        var itemIds = menuGroups
            .SelectMany(x => x.Items)
            .Select(x => x.Id)
            .Distinct();

        var files = await fileRepository.GetPrimaryFilesAsync(itemIds, cancellationToken);

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

        return MessageContract<ListMenuItemResponse>.Success(new ListMenuItemResponse { Items = menuGroups });
    }
}
