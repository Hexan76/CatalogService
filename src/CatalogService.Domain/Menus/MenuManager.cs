using System.Text.RegularExpressions;

using CatalogService.Localization;

using Volo.Abp;

namespace CatalogService.Menus;

public class MenuManager(IMenuRepository menuRepository)
{
    public async Task<List<Menu>> BulkUpdateAsync(
        IReadOnlyList<MenuBulkUpdate> updates,
        CancellationToken cancellationToken = default)
    {
        if (updates.Count == 0)
        {
            return [];
        }

        if (updates.Any(x => x.SortOrder <= 0))
        {
            throw new BusinessException(
                    CatalogServiceErrorCodes.PriorityMustBeGreaterThanZero,
                    CatalogServiceErrorCodes.PriorityMustBeGreaterThanZero)
                .WithResource(typeof(CatalogServiceResources));
        }

        if (updates.Select(x => x.Id).Distinct().Count() != updates.Count
            || updates.Select(x => x.SortOrder).Distinct().Count() != updates.Count)
        {
            throw new BusinessException(
                    CatalogServiceErrorCodes.DuplicatePrioritiesNotAllowed,
                    CatalogServiceErrorCodes.DuplicatePrioritiesNotAllowed)
                .WithResource(typeof(CatalogServiceResources));
        }

        var ids = updates.Select(x => x.Id).ToList();
        var menus = await menuRepository.GetListAsync(
            x => ids.Contains(x.Id),
            cancellationToken);

        if (menus.Count != ids.Count)
        {
            throw new BusinessException(
                    CatalogServiceErrorCodes.MenuNotFound,
                    CatalogServiceErrorCodes.MenuNotFound)
                .WithResource(typeof(CatalogServiceResources));
        }

        var updateById = updates.ToDictionary(x => x.Id);

        foreach (var menu in menus)
        {
            var update = updateById[menu.Id];
            menu.Name = update.Name.Trim();
            menu.Slug = GenerateSlug(menu.Name);
            menu.SortOrder = update.SortOrder;
            menu.IsDisabled = update.IsDisabled;

            if (update.VendorId > 0)
            {
                menu.VendorId = update.VendorId;
            }
        }

        await menuRepository.UpdateManyAsync(menus, true, cancellationToken);

        return menus.OrderBy(x => x.SortOrder).ThenBy(x => x.Id).ToList();
    }

    private static string GenerateSlug(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return string.Empty;
        }

        text = text.Trim();
        text = Regex.Replace(text, @"\s+", "-");
        text = Regex.Replace(text, @"[^a-zA-Z0-9\u0600-\u06FF\-]", "");
        text = Regex.Replace(text, @"-+", "-");

        return text.ToLowerInvariant();
    }
}
