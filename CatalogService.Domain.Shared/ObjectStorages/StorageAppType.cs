using System.ComponentModel;
using System.Text.Json.Serialization;

namespace CatalogService.ObjectStorages;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum StorageAppType
{
    [Description("4Sough")]
    CharSoughShop = 0,
    QasedFood = 1,
    QasedParcel = 2,

}
