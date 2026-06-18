using CatalogService.Application;
using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Items;

public class SearchItemHandler(IItemRepository itemRepository, IFileRepository fileRepository) : CatalogServiceAppService, ISearchItemHandler
{
    public async Task<MessageContract<SearchItemResponse>> Handle(SearchItemRequest request, CancellationToken cancellationToken)
    {

        var query = (await itemRepository.GetQueryableAsync())
            .Where(x => x.Name.Contains(request.SearchTerm ?? ""));

        if (request.BusinessTypeId.HasValue)
        {
            query = query.Where(x =>
                x.ItemCategories.Any(ic =>
                    ic.Category.CategoryBusinessTypes.Any(cbt =>
                        cbt.BusinessTypeId == request.BusinessTypeId.Value)));
        }
        var queryPaged = await itemRepository.PaginationPagingAsync(null, query, request.Page, request.PageSize);

        var itemIds = query.Select(x => x.Id);

        var files = (await fileRepository.GetQueryableAsync()).Where(x => itemIds.Contains(x.EntityId));

        var fileLookup = files
            .GroupBy(x => x.EntityId)
            .ToDictionary(
                x => x.Key,
                x => x.OrderBy(f => f.Priority).FirstOrDefault()
            );

        var response = new SearchItemResponse
        {
            Items = ObjectMapper.Map<List<Item>, List<ItemModel>>(query.ToList()),
            TotalCount = queryPaged.RowCount
        };

        foreach (var item in response.Items)
        {
            if (fileLookup.TryGetValue(item.Id, out var file))
            {
                item.Image = new FileUrlModel
                {
                    Role = file.Role,
                    Variants = file.Variants
                };
            }
        }


        return MessageContract<SearchItemResponse>.Success(response, "paginated Success", totalCount: (int)response.TotalCount, queryPaged.CurrentPage);
    }
}
