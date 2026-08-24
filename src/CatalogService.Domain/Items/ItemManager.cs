using CatalogService.Menus;

using Volo.Abp;

namespace CatalogService.Items;

public class ItemManager(IItemRepository itemRepository)
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

        return await itemRepository.InsertAsync(item, true);
    }
    public async Task<Item> UpdateAsync(Item input)
    {
        var item = await itemRepository.GetAsync(input.Id);

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
            if (inputVariant.Id > 0)
            {
                var existingVariant = item.Variants
                    .FirstOrDefault(x => x.Id == inputVariant.Id);

                if (existingVariant != null)
                {
                    existingVariant.Name = inputVariant.Name;
                    existingVariant.Description = inputVariant.Description;
                    existingVariant.Price = inputVariant.Price;
                    existingVariant.DiscountPrecent = inputVariant.DiscountPrecent;
                    existingVariant.HasDiscount = inputVariant.HasDiscount;
                    existingVariant.SortOrder = inputVariant.SortOrder;
                    existingVariant.Ingredients = input.Ingredients;
                    existingVariant.IsDisabled = inputVariant.IsDisabled;
                }
                else
                {
                    throw new BusinessException("Variant not found.");
                }
            }
            else
            {
                // New Variant
                item.Variants.Add(new Item
                {
                    Name = inputVariant.Name,
                    Description = inputVariant.Description,
                    Price = inputVariant.Price,
                    DiscountPrecent = inputVariant.DiscountPrecent,
                    HasDiscount = inputVariant.HasDiscount,
                    SortOrder = inputVariant.SortOrder,
                    IsDisabled = inputVariant.IsDisabled,
                    Ingredients = inputVariant.Ingredients,
                    ParentId = item.Id
                });
            }
        }

        return await itemRepository.UpdateAsync(item, true);
    }
}
