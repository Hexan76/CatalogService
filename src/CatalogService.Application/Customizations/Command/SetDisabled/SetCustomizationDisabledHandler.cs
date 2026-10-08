using AutoMapper;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Customizations;

public class SetCustomizationDisabledHandler(CustomizationManager customizationManager,
                                             IMapper mapper) : ISetCustomizationDisabledHandler
{
    public async Task<MessageContract<CustomizationModel>> Handle(SetCustomizationDisabledRequest request, CancellationToken cancellationToken)
    {
        var customization = await customizationManager.SetDisabledAsync(request.Id, request.IsDisabled);
        var response = mapper.Map<Customization, CustomizationModel>(customization);
        return MessageContract<CustomizationModel>.Success(response);
    }
}
