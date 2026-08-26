using Framework.BuildingBlock.Application.Contracts;
using Framework.BuildingBlock.Settings;

namespace CatalogService.Settings.Query;

public class GetSettingsHandler(ISettings settings) : IFrameworkRequestHandler<GetSettingRequest, GetSettingResponse>
{
    public async Task<MessageContract<GetSettingResponse>> Handle(GetSettingRequest request, CancellationToken cancellationToken)
    {

        var response = new GetSettingResponse();
        response.PerparingItemTemplates = await settings.GetAsync<List<long>>(CatalogSettingDefinitionProvider.ItemsPreparingTimeTemplates, cancellationToken);
        return MessageContract<GetSettingResponse>.Success(response);
    }
}
