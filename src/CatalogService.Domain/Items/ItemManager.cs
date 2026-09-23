using CatalogService.Customizations;
using CatalogService.Localization;
using CatalogService.Menus;

using Volo.Abp;

namespace CatalogService.Items;

public class ItemManager(
    IItemRepository itemRepository,
    ICustomizationRepository customizationRepository)
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

        item.Variants ??= [];
        item.Customizations ??= [];

        await EnsureCustomizationsExistAsync(CollectCustomizationIds(item));
        AppendAndMergeCustomizations(item);

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
        item.PreparingTime = input.PreparingTime;

        if (input.Price > 0)
        {
            item.Price = input.Price;
        }

        await EnsureCustomizationsExistAsync(CollectCustomizationIds(input));
        AppendCustomizations(item, GetCustomizationIds(input));

        var parentCustomizationIds = GetCustomizationIds(item);
        var inputVariants = input.Variants ?? [];
        var existingVariants = item.Variants.ToList();

        foreach (var existing in existingVariants)
        {
            var stillExists = inputVariants.Any(x => x.Id == existing.Id);

            if (!stillExists)
            {
                item.Variants.Remove(existing);
            }
        }

        foreach (var inputVariant in inputVariants)
        {
            var mergedCustomizationIds = parentCustomizationIds
                .Concat(GetCustomizationIds(inputVariant));

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
                    existingVariant.Ingredients = inputVariant.Ingredients;
                    existingVariant.IsDisabled = inputVariant.IsDisabled;
                    existingVariant.PreparingTime = inputVariant.PreparingTime;
                    existingVariant.VendorId = item.VendorId;

                    AppendCustomizations(existingVariant, mergedCustomizationIds);
                }
                else
                {
                    throw new BusinessException(
                            CatalogServiceErrorCodes.VariantNotFound,
                            CatalogServiceErrorCodes.VariantNotFound)
                        .WithResource(typeof(CatalogServiceResources));
                }
            }
            else
            {
                var variant = new Item
                {
                    Name = inputVariant.Name,
                    Description = inputVariant.Description,
                    Price = inputVariant.Price,
                    DiscountPrecent = inputVariant.DiscountPrecent,
                    HasDiscount = inputVariant.HasDiscount,
                    SortOrder = inputVariant.SortOrder,
                    IsDisabled = inputVariant.IsDisabled,
                    Ingredients = inputVariant.Ingredients,
                    PreparingTime = inputVariant.PreparingTime,
                    VendorId = item.VendorId,
                    ParentId = item.Id
                };

                AppendCustomizations(variant, mergedCustomizationIds);
                item.Variants.Add(variant);
            }
        }

        return await itemRepository.UpdateAsync(item, true);
    }

    private void AppendAndMergeCustomizations(Item item)
    {
        MergeCustomizations(item, GetCustomizationIds(item));

        var parentCustomizationIds = GetCustomizationIds(item);

        foreach (var variant in item.Variants)
        {
            variant.VendorId = item.VendorId;
            variant.ParentId = item.Id;

            AppendCustomizations(
                variant,
                parentCustomizationIds.Concat(GetCustomizationIds(variant)));
        }
    }

    private static void MergeCustomizations(Item item, IEnumerable<long> customizationIds)
    {
        item.Customizations ??= new List<ItemCustomization>();

        var requestedIds = DistinctCustomizationIds(customizationIds);

        foreach (var existing in item.Customizations
            .Where(x => !requestedIds.Contains(x.CustomizationId))
            .ToList())
        {
            item.Customizations.Remove(existing);
        }

        AppendCustomizations(item, requestedIds);
    }

    private static void AppendCustomizations(Item item, IEnumerable<long> customizationIds)
    {
        item.Customizations ??= new List<ItemCustomization>();

        var existingIds = item.Customizations
            .Select(x => x.CustomizationId)
            .ToHashSet();

        foreach (var customizationId in DistinctCustomizationIds(customizationIds))
        {
            if (existingIds.Add(customizationId))
            {
                item.Customizations.Add(new ItemCustomization
                {
                    ItemId = item.Id,
                    CustomizationId = customizationId
                });
            }
        }
    }

    private async Task EnsureCustomizationsExistAsync(IReadOnlyCollection<long> customizationIds)
    {
        if (customizationIds.Count == 0)
        {
            return;
        }

        var existingCount = await customizationRepository.CountAsync(x =>
            customizationIds.Contains(x.Id));

        if (existingCount != customizationIds.Count)
        {
            throw new BusinessException(
                    CatalogServiceErrorCodes.CustomizationNotFound,
                    CatalogServiceErrorCodes.CustomizationNotFound)
                .WithResource(typeof(CatalogServiceResources));
        }
    }

    private static IReadOnlyCollection<long> CollectCustomizationIds(Item item)
    {
        return DistinctCustomizationIds(
            GetCustomizationIds(item)
                .Concat((item.Variants ?? []).SelectMany(GetCustomizationIds)));
    }

    private static IReadOnlyCollection<long> GetCustomizationIds(Item item)
        => DistinctCustomizationIds(item.Customizations?.Select(x => x.CustomizationId));

    private static IReadOnlyCollection<long> DistinctCustomizationIds(IEnumerable<long>? customizationIds)
        => (customizationIds ?? [])
            .Where(id => id > 0)
            .Distinct()
            .ToArray();
}
