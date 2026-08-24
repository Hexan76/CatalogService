using AutoMapper;

using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Categories;

public class ListCategoryHandler(ICategoryRepository categoryRepository, IMapper mapper) : IListCategoryHandler
{
    public async Task<MessageContract<ListCategoryResponse>> Handle(ListCategoryRequest request, CancellationToken cancellationToken)
    {
        var query = await categoryRepository.GetListWithFilesAsync(request.ParentId);

        query.Queryable = query.Queryable
        .Where(x => x.Category.CategoryBusinessTypes.Any(x => x.BusinessTypeId == request.BusinessTypeId));

        if (request.ParentId is not null)
        {
            query.Queryable = query.Queryable.Where(x => x.Category.ParentId == request.ParentId);
        }

        var result = await query.Queryable.ToListAsync();

        var response = new ListCategoryResponse();

        var mappedList = new List<CategoryModel>();
        foreach (var item in result)
        {
            var catMap = mapper.Map<Category, CategoryModel>(item.Category);

            var files = item.Files
                .OrderBy(x => x.Priority);

            foreach (var file in files)
            {
                catMap.Image = new FileUrlModel
                {
                    Role = file.Role,
                    Variants = file.Variants,
                };

            }

            mappedList.Add(catMap);
        }
        response.Items = mappedList;

        return MessageContract<ListCategoryResponse>.Success(response);
    }
}
