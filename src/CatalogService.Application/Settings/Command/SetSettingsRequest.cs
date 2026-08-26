using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Settings.Command;

public class SetSettingsRequest : IFrameworkRequest<SettingModel>
{
    public ICollection<long> PreparingItemTemplates { get; set; } = [];
}
