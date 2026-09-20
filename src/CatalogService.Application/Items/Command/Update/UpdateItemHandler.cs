using System.Text.RegularExpressions;

using AutoMapper;

using CatalogService.Files;
using CatalogService.ObjectStorageService;
using CatalogService.PricingService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.Items;

public class UpdateItemHandler(
    ItemManager itemManager,
    IMapper mapper,
    IHttpClientService httpClientService,
    FileManager fileManager) : IUpdateItemHandler
{
    public async Task<MessageContract<ItemModel>> Handle(UpdateItemRequest request, CancellationToken cancellationToken)
    {

        var requestItem = mapper.Map<UpdateItemRequest, Item>(request);
        var result = await itemManager.UpdateAsync(requestItem);

        if (request.Price > 0)
        {
            await SendPriceAsync(result);
        }

        foreach (var variant in result.Variants)
        {
            if (variant.Price > 0)
            {
                await SendPriceAsync(variant, result.VendorId);
            }
        }

        var response = mapper.Map<Item, ItemModel>(result);

        if (request.ImageId is not null)
        {
            var files = new List<FinalizeModel>
            {
                new FinalizeModel()
                {
                    Id=request.ImageId??Guid.Empty,
                    AppType = "QasedFood",
                    FileName = result.Name,
                    EntityKey = result.Name,
                    GenerateThumbnail=true,
                    Role="default",
                    StorageEntityType="Item",
                }
            };
            var finalizeResponse = await httpClientService.SendAsync<FinalizeFilesResponse>(new FinalizeRequest() { Files = files }, WrapType: ResponseType.Default, "ObjectStorageClient");

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

    private async Task SendPriceAsync(Item item, long fallbackVendorId = 0)
    {
        await httpClientService.SendAsync<ApiResult<UpsertPriceResponse>>(
            new UpsertPriceRequest
            {
                VendorId = item.VendorId != 0 ? item.VendorId : fallbackVendorId,
                EntityId = item.Id,
                Amount = item.Price,
                Currency = "AFN"
            },
            ResponseType.Custom,
            "PricingClient");
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
