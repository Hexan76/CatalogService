using AutoMapper;

using CatalogService.CategoryCustomization;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class CreateCustomizationGroupHandler(ICustomizationGroupRepository customizationGroupRepository, IMapper mapper) : ICreateCustomizationGroupHandler
{
    public async Task<MessageContract<CustomizationGroupModel>> Handle(
        CreateCustomizationGroupRequest request,
        CancellationToken cancellationToken)
    {
        var craeteItem = mapper.Map<CreateCustomizationGroupRequest, CustomizationGroup>(request);

        var resutl = await customizationGroupRepository.InsertAsync(craeteItem, true);
        var response = mapper.Map<CustomizationGroup, CustomizationGroupModel>(resutl);

        return MessageContract<CustomizationGroupModel>.Success(response);
    }
}
