using CatalogService.Application;
using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class ListMenuItemHandler(IItemRepository itemRepository, IFileRepository fileRepository) : CatalogServiceAppService, IListMenuItemHandler
{
    public async Task<MessageContract<ListMenuItemResponse>> Handle(ListMenuItemRequest request, CancellationToken cancellationToken)
    {
        var query = await itemRepository.WithDetailsAsync();
        query = query.Where(x => x.VendorId == request.VendorId);

        var menuGroups = query.Where(x => x.VendorId == request.VendorId)
                .WhereIf(
                            request.MenuId.HasValue,
                            x => x.MenuItems.Any(mi => mi.MenuId == request.MenuId.Value)
                        )
                .SelectMany(x => x.MenuItems)
                .GroupBy(x => new
                {
                    x.MenuId,
                    x.Menu.Name
                })
                .Select(g => new MenuItemModel
                {
                    Id = g.Key.MenuId,
                    MenuTitle = g.Key.Name,
                    Items = g.Select(mi => new ItemModel
                    {
                        Id = mi.Item.Id,
                        Name = mi.Item.Name,
                        Description = mi.Item.Description,
                        SortOrder = mi.Item.SortOrder,
                        HasDiscount = mi.Item.HasDiscount,
                        DiscountPrecent = mi.Item.DiscountPrecent,
                        VendorId = mi.Item.VendorId,

                        Variants = mi.Item.Variants.Select(v => new VariantModel
                        {
                            Id = v.Id,
                            Name = v.Name,
                            Description = v.Description,
                            Price = v.Price,
                            HasDiscount = v.HasDiscount,
                            DiscountPrecent = v.DiscountPrecent,
                            SortOrder = v.SortOrder
                        }).ToList()
                    }).ToList()
                }).ToList();

        var itemIds = menuGroups
            .SelectMany(x => x.Items)
            .Select(x => x.Id)
            .Distinct()
            .ToList();

        var files = (await fileRepository.GetQueryableAsync()).Where(x => itemIds.Contains(x.EntityId));

        var fileLookup = files
            .GroupBy(x => x.EntityId)
            .ToDictionary(
                x => x.Key,
                x => x.OrderBy(f => f.Priority).FirstOrDefault()
            );

        var response = new ListMenuItemResponse
        {
            Items = menuGroups
        };

        foreach (var menu in menuGroups)
        {
            foreach (var item in menu.Items)
            {
                if (fileLookup.TryGetValue(item.Id, out var file))
                {
                    item.Image = new FileUrlModel
                    {
                        Role = file.Role,
                        Variants = file.Variants
                    };
                }
            }
        }


        return MessageContract<ListMenuItemResponse>.Success(response);
    }
}
