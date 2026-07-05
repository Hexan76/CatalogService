using CatalogService.Application;
using CatalogService.CategoryCustomization;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class CreateCustomizationGroupHandler(ICustomizationGroupRepository customizationGroupRepository) : CatalogServiceAppService, ICreateCustomizationGroupHandler
{
    public async Task<MessageContract<CustomizationGroupModel>> Handle(
        CreateCustomizationGroupRequest request,
        CancellationToken cancellationToken)
    {
        var craeteItem = ObjectMapper.Map<CreateCustomizationGroupRequest, CustomizationGroup>(request);

        var resutl = await customizationGroupRepository.InsertAsync(craeteItem);
        var response = ObjectMapper.Map<CustomizationGroup, CustomizationGroupModel>(resutl);

        return MessageContract<CustomizationGroupModel>.Success(response);
    }
}
