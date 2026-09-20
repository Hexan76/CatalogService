using AutoMapper;

using CatalogService.Files;
using CatalogService.ObjectStorageService;
using CatalogService.PricingService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;
using Framework.RabbitMQ;

namespace CatalogService.Items;

public class CreateItemHandler(
    ItemManager itemManager,
    IHttpClientService httpClientService,
    FileManager fileManager,
    IMapper mapper,
    IMessagePublisher messagePublisher) : ICreateItemHandler
{
    public async Task<MessageContract<ItemModel>> Handle(CreateItemRequest request, CancellationToken cancellationToken)
    {

        var createItem = mapper.Map<CreateItemRequest, Item>(request);

        var result = await itemManager.CreateAsync(createItem, request.CategoryId, request.MenuId);

        await SendPriceAsync(result);

        foreach (var variant in result.Variants)
        {
            await SendPriceAsync(variant, result.VendorId);
        }

        var response = mapper.Map<Item, ItemModel>(result);

        if (request.ImageId is not null)
        {
            var files = new List<FinalizeModel>
            {
                new FinalizeModel()
                {
                    Id = request.ImageId??Guid.Empty,
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

                var fileResult = await fileManager.AddRelatedFileWithEntity(
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

        //await messagePublisher.PublishAsync(new Message<ItemCreatedEvent>()
        //{
        //    Type = nameof(ItemCreatedEvent),
        //    Payload = new ItemCreatedEvent(response),
        //}, null, cancellationToken);

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

}
