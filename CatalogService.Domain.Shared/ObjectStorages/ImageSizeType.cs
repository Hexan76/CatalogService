using System.Text.Json.Serialization;

namespace CatalogService.ObjectStorages;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ImageSize
{
    Small,
    Medium,
    Large
}
