using AutoMapper;

using CatalogService.CategoryCustomization;
using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Customizations;

public class GetItemCustomizationsByVendorHandler(
    ICustomizationGroupRepository customizationGroupRepository,
    IMapper mapper,
    IFileRepository fileRepository)
    : IGetItemCustomizationsByVendorHandler
{
    public async Task<MessageContract<GetItemCustomizationsByVendorResponse>> Handle(
        GetItemCustomizationsByVendorRequest request,
        CancellationToken cancellationToken)
    {
        var query = customizationGroupRepository.GetQueryable();

        query = query
                .Include(x => x.Customizations)
                    .ThenInclude(x => x.Items)
                        .ThenInclude(x => x.Item)
                .Include(x => x.Customizations)
                    .ThenInclude(x => x.Items);
        query = query
                    .Where(g => g.Customizations.Any(c =>
                            c.Items.Any(i => i.Item.VendorId == request.VendorId)))
                        .OrderBy(g => g.SortOrder);

        var groups = await query.ToListAsync(cancellationToken);

        foreach (var group in groups)
        {
            group.Customizations = group.Customizations
                .Where(c => c.Items.Any(i => i.Item.VendorId == request.VendorId))
                .OrderBy(c => c.SortOrder)
                .ToList();
        }

        var response = new GetItemCustomizationsByVendorResponse
        {
            Items = mapper.Map<
                List<CustomizationGroup>,
                List<CustomizationGroupModel>>(groups)
        };

        var customizationIds = response.Items
            .SelectMany(g => g.Customizations)
            .Select(c => c.Id);

        var files = await fileRepository.GetPrimaryFilesAsync(
            customizationIds,
            cancellationToken);

        foreach (var group in response.Items)
        {
            foreach (var customization in group.Customizations)
            {
                if (files.TryGetValue(customization.Id, out var file))
                {
                    customization.Image = new FileUrlModel
                    {
                        Role = file.Role,
                        Variants = file.Variants
                    };
                }
            }
        }

        return MessageContract<GetItemCustomizationsByVendorResponse>.Success(response);
    }
}
