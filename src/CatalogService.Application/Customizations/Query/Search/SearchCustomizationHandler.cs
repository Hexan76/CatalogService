using AutoMapper;

using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Customizations;

public class SearchCustomizationHandler(ICustomizationRepository customizationRepository,
                                        IFileRepository fileRepository,
                                        IMapper mapper) : ISearchCustomizationHandler
{
    public async Task<MessageContract<SearchCustomizationResponse>> Handle(SearchCustomizationRequest request, CancellationToken cancellationToken)
    {
        var query = customizationRepository.GetQueryable()
            .Include(x => x.CustomizationGroup)
            .Where(x => x.VendorId == request.VendorId);

        if (request.GroupId.HasValue)
        {
            query = query.Where(x => x.CustomizationGroupId == request.GroupId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(x => x.Name.Contains(request.SearchTerm));
        }

        if (request.IsDisabled.HasValue)
        {
            query = query.Where(x => x.IsDisabled == request.IsDisabled.Value);
        }

        query = query
            .OrderBy(x => x.SortOrder)
            .ThenBy(x => x.Name);

        var queryPaged = await customizationRepository.PaginationPagingAsync(
            null,
            query,
            request.Page,
            request.PageSize);

        var customizations = await queryPaged.Queryable.ToListAsync(cancellationToken);

        var response = new SearchCustomizationResponse
        {
            Items = mapper.Map<List<Customization>, List<CustomizationModel>>(customizations),
            TotalCount = queryPaged.RowCount
        };

        var files = await fileRepository.GetPrimaryFilesAsync(
            customizations.Select(x => x.Id),
            cancellationToken);

        foreach (var customization in response.Items)
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

        return MessageContract<SearchCustomizationResponse>.Success(
            response,
            "paginated Success",
            totalCount: (int)response.TotalCount,
            queryPaged.CurrentPage);
    }
}
