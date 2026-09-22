using CatalogService.Items;
using CatalogService.Localization;

using Volo.Abp;

namespace CatalogService.Customizations;

public class CustomizationManager(
    ICustomizationGroupRepository customizationGroupRepository,
    ICustomizationRepository customizationRepository)
{
    public async Task<Customization> CreateAsync(
        Customization customization,
        long groupId,
        long[]? itemIds)
    {
        var group = await customizationGroupRepository.FindAsync(groupId);

        if (group == null)
        {
            throw new BusinessException(
                    CatalogServiceErrorCodes.CustomizationGroupNotFound,
                    CatalogServiceErrorCodes.CustomizationGroupNotFound)
                .WithResource(typeof(CatalogServiceResources))
                .WithData("GroupId", groupId);
        }

        customization.CustomizationGroupId = groupId;

        customization.Items = new List<ItemCustomization>();

        if (itemIds != null)
        {
            foreach (var itemId in itemIds.Distinct())
            {
                customization.Items.Add(new ItemCustomization
                {
                    ItemId = itemId,
                    CustomizationId = customization.Id
                });
            }
        }

        return await customizationRepository.InsertAsync(customization);
    }

    public async Task<Customization> UpdateAsync(Customization input)
    {
        var customization = await customizationRepository.GetAsync(input.Id);

        customization.Name = input.Name;
        customization.Description = input.Description;
        customization.Price = input.Price;
        customization.AvailableQuantity = input.AvailableQuantity;
        customization.MaxCountRequest = input.MaxCountRequest;
        customization.SortOrder = input.SortOrder;
        customization.IsDisabled = input.IsDisabled;

        if (input.CustomizationGroupId > 0 &&
            input.CustomizationGroupId != customization.CustomizationGroupId)
        {
            var group = await customizationGroupRepository.FindAsync(input.CustomizationGroupId);

            if (group == null)
            {
                throw new BusinessException(
                        CatalogServiceErrorCodes.CustomizationGroupNotFound,
                        CatalogServiceErrorCodes.CustomizationGroupNotFound)
                    .WithResource(typeof(CatalogServiceResources))
                    .WithData("GroupId", input.CustomizationGroupId);
            }

            customization.CustomizationGroupId = input.CustomizationGroupId;
        }

        return await customizationRepository.UpdateAsync(customization, true);
    }
}
