using CatalogService.Menus;

using Volo.Abp.DependencyInjection;

namespace CatalogService.Items;

public class ItemManager(IItemRepository itemRepository) : ITransientDependency
{
    public async Task<Item> CreateAsync(Item item, long categoryId, long menuId)
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
    public async Task<Item> UpdateAsync(Item input)
    {
        var item = await itemRepository.GetAsync(
            input.Id,
            includeDetails: true
        );

        item.Name = input.Name;
        item.Description = input.Description;
        item.SortOrder = input.SortOrder;
        item.HasDiscount = input.HasDiscount;
        item.DiscountPrecent = input.DiscountPrecent;
        item.Ingredients = input.Ingredients;


        var existingVariants = item.Variants.ToList();

        foreach (var existing in existingVariants)
        {
            var stillExists = input.Variants.Any(x => x.Id == existing.Id);

            if (!stillExists)
            {
                item.Variants.Remove(existing);
            }
        }

        foreach (var inputVariant in input.Variants)
        {
            var existing = item.Variants.FirstOrDefault(x => x.Id == inputVariant.Id);

            if (existing == null)
            {
                item.Variants.Add(new ItemVariant
                {
                    Name = inputVariant.Name,
                    Description = inputVariant.Description,
                    Price = inputVariant.Price,
                    DiscountPrecent = inputVariant.DiscountPrecent,
                    HasDiscount = inputVariant.HasDiscount,
                    SortOrder = inputVariant.SortOrder,
                    IsDisabled = inputVariant.IsDisabled,

                    ItemId = item.Id
                });
            }
            else
            {
                existing.Name = inputVariant.Name;
                existing.Description = inputVariant.Description;
                existing.Price = inputVariant.Price;
                existing.DiscountPrecent = inputVariant.DiscountPrecent;
                existing.HasDiscount = inputVariant.HasDiscount;
                existing.SortOrder = inputVariant.SortOrder;
                existing.IsDisabled = inputVariant.IsDisabled;
            }
        }

        return await itemRepository.UpdateAsync(item);
    }
}
