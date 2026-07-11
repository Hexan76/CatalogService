using CatalogService.Items;

using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace CatalogService.Customizations;

public class CustomizationManager(
    ICustomizationGroupRepository customizationGroupRepository,
    ICustomizationRepository customizationRepository)
    : DomainService
{
    public async Task<Customization> CreateAsync(
        Customization customization,
        long groupId,
        long[]? itemIds)
    {
        var group = await customizationGroupRepository.FindAsync(groupId);

        if (group == null)
        {
            throw new BusinessException("CustomizationGroup.NotFound")
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
}
