using CatalogService.Application;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.BusinessTypes;

public class ListBusinessTypeHandler(IBusinessTypeRepository businessTypeRepository) : CatalogServiceAppService, IListBusinessTypeHandler
{
    public async Task<MessageContract<ListBusinessTypeResponse>> Handle(ListBusinessTypeRequest request, CancellationToken cancellationToken)
    {
        var query = await businessTypeRepository.GetListWithFilesAsync();

        var result = await AsyncExecuter.ToListAsync(query.Queryable);

        var response = new ListBusinessTypeResponse();

        var mappedList = new List<BusinessTypeModel>();
        foreach (var item in result)
        {
            var catMap = ObjectMapper.Map<BusinessType, BusinessTypeModel>(item.BusinessType);

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

        return MessageContract<ListBusinessTypeResponse>.Success(response);
    }
}
