using CatalogService.Application;
using CatalogService.Categories;
using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public class GetBusinessTypeHandler(IBusinessTypeRepository businessTypeRepository, IFileRepository fileRepository) : CatalogServiceAppService, IGetBusinessTypeHandler
{
    public async Task<MessageContract<BusinessTypeModel>> Handle(GetBusinessTypeRequest request, CancellationToken cancellationToken)
    {
        var founded = await businessTypeRepository.FindAsync(request.Id, true);

        var response = ObjectMapper.Map<BusinessType, BusinessTypeModel>(founded);
        response.Categories = ObjectMapper.Map<List<Category>, List<CategoryModel>>(founded.CategoryBusinessTypes.Select(x => x.Category).ToList());

        var filesByCategory = (await fileRepository.GetQueryableAsync()).Where(x => response.Categories.Select(c => c.Id).Contains(x.EntityId));

        foreach (var item in response.Categories)
        {
            var files = filesByCategory
                .OrderBy(x => x.Priority);

            foreach (var file in files)
            {
                item.Image = new FileUrlModel
                {
                    Role = file.Role,
                    Variants = file.Variants,
                };

            }


        }

        return MessageContract<BusinessTypeModel>.Success(response);

    }
}
