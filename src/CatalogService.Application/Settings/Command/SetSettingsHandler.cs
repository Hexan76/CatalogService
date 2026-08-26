using Framework.BuildingBlock.Application.Contracts;
using Framework.BuildingBlock.Settings;

namespace CatalogService.Settings.Command;

public class SetSettingsHandler(ISettings settings) : IFrameworkRequestHandler<SetSettingsRequest, SettingModel>
{
    public async Task<MessageContract<SettingModel>> Handle(SetSettingsRequest request, CancellationToken cancellationToken)
    {
        await settings.SetAsync<List<long>>(CatalogSettingDefinitionProvider.ItemsPreparingTimeTemplates, [.. request.PreparingItemTemplates], cancellationToken);

        SettingModel response = new()
        {
            PreparingItemTemplates = await settings.GetAsync<List<long>>(CatalogSettingDefinitionProvider.ItemsPreparingTimeTemplates, cancellationToken)
        };

        return MessageContract<SettingModel>.Success(response);
    }
}
