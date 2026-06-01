using Framework.HttpClient.Abstractions;

namespace CatalogService.ObjectStorageService;

public class FinalizeRequest : IHttpRequest
{
    public HttpMethod Method => HttpMethod.Post;

    public string Route { get; set; } = "v1/api/ObjectStorageService/object-storage/finalize";
    public List<FinalizeModel> Files { get; set; } = [];

}
