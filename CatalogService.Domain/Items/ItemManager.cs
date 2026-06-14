using CatalogService.Menus;

using Volo.Abp.DependencyInjection;

namespace CatalogService.Items;

public class ItemManager(IItemRepository itemRepository) : ITransientDependency
{
    public async Task<Item> CreateAsync(Item item,Guid categoryId,Guid menuId)
    {
        item.ItemCategories.Add(new ItemCategory
        {
            ItemId = item.Id,
            CategoryId = categoryId
        });

        item.MenuItems.Add(new MenuItem
        {
            ItemId = item.Id,
            MenuId = menuId
        });

        return await itemRepository.InsertAsync(item);
    }
}
