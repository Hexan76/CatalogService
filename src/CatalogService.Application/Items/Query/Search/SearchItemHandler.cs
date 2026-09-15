using AutoMapper;

using CatalogService.Files;
using CatalogService.ObjectStorageService;
using CatalogService.PricingService;

using Framework.BuildingBlock.Application.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Items;

public class SearchItemHandler(
    IItemRepository itemRepository,
    IFileRepository fileRepository,
    IMapper mapper,
    CurrentPriceClient currentPriceClient) : ISearchItemHandler
{
    public async Task<MessageContract<SearchItemResponse>> Handle(SearchItemRequest request, CancellationToken cancellationToken)
    {

        var query = itemRepository.GetQueryable();

        query = query
            .Include(x => x.MenuItems)
                .ThenInclude(mi => mi.Menu)
            .Include(x => x.Variants)
                .ThenInclude(x => x.MenuItems)
                .ThenInclude(x => x.Menu)
            .Include(x => x.Customizations)
            .ThenInclude(x => x.Customization)
            .ThenInclude(x => x.CustomizationGroup);

        query = query.Where(x => x.Name.Contains(request.SearchTerm ?? "") && x.ParentId == null);

        if (request.BusinessTypeId.HasValue)
        {
            query = query.Where(x =>
                x.ItemCategories.Any(ic =>
                    ic.Category.CategoryBusinessTypes.Any(cbt =>
                        cbt.BusinessTypeId == request.BusinessTypeId.Value)));
        }
        var queryPaged = await itemRepository.PaginationPagingAsync(null, query, request.Page, request.PageSize);

        var itemIds = query.Select(x => x.Id);

        var files = await fileRepository.GetPrimaryFilesAsync(itemIds);

        var response = new SearchItemResponse
        {
            Items = mapper.Map<List<Item>, List<SearchItemModel>>(query.ToList()),
            TotalCount = queryPaged.RowCount
        };

        foreach (var item in response.Items)
        {
            if (files.TryGetValue(item.Id, out var file))
            {
                item.Image = new FileUrlModel
                {
                    Role = file.Role,
                    Variants = file.Variants
                };
            }
        }

        await currentPriceClient.ApplyAsync(response.Items, cancellationToken);


        return MessageContract<SearchItemResponse>.Success(response, "paginated Success", totalCount: (int)response.TotalCount, queryPaged.CurrentPage);
    }
}
