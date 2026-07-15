using System.Text.RegularExpressions;

using CatalogService.Application;
using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.Items;

public class UpdateItemHandler(ItemManager itemManager, IHttpClientService httpClientService, FileManager fileManager) : CatalogServiceAppService, IUpdateItemHandler
{
    public async Task<MessageContract<ItemModel>> Handle(UpdateItemRequest request, CancellationToken cancellationToken)
    {

        var requestItem = ObjectMapper.Map<UpdateItemRequest, Item>(request);
        var result = await itemManager.UpdateAsync(requestItem);
        
        var response = ObjectMapper.Map<Item, ItemModel>(result);

        if (request.ImageId is not null)
        {
            var files = new List<FinalizeModel>
            {
                new FinalizeModel()
                {
                    Id=request.ImageId??Guid.Empty,
                    AppType = ObjectStorages.StorageAppType.QasedFood,
                    FileName = result.Name,
                    EntityKey = result.Name,
                    GenerateThumbnail=true,
                    Role="default",
                    StorageEntityType="Item",
                }
            };
            var finalizeResponse = await httpClientService.SendAsync<FinalizeFilesResponse>(new FinalizeRequest() { Files = files });

            if (finalizeResponse != null && finalizeResponse.Files.Any())
            {
                var file = finalizeResponse.Files.FirstOrDefault();

                var fileResult = await fileManager.AddOrUpdateRelatedFile(
                    result.Id,
                    typeof(Item).Name,
                    file.URL,
                    file.FileName,
                    file.Size,
                    file.Extension,
                    "default",
                    file.Variants);

                response.Image = new FileUrlModel()
                {
                    Role = "default",
                    Variants = file.Variants
                };
            }
        }

        return MessageContract<ItemModel>.Success(response);
    }
    static string GenerateSlug(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        text = text.Trim();

        text = Regex.Replace(text, @"\s+", "-");

        text = Regex.Replace(text, @"[^a-zA-Z0-9\u0600-\u06FF\-]", "");

        text = Regex.Replace(text, @"-+", "-");

        return text.ToLowerInvariant();
    }
}
