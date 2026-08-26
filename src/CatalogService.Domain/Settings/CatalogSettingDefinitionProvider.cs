using Framework.BuildingBlock.Settings;

namespace CatalogService.Settings;

public class CatalogSettingDefinitionProvider : ISettingDefinitionProvider
{
    public const string ItemsPreparingTimeTemplates = "Catalog.Items.PreparingTimeTemplates";
    public void Define(ISettingDefinitionContext context)
    {
        context.Add(new SettingDefinition(ItemsPreparingTimeTemplates, SettingValueType.Json, "[600,1200,1800,2400,3000]"));
    }
}
