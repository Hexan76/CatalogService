using CatalogService.Application;
using CatalogService.Files;
using CatalogService.Items.Command;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;
using Framework.RabbitMQ;

namespace CatalogService.Items;

public class CreateItemHandler(
    ItemManager itemManager,
    IHttpClientService httpClientService,
    FileManager fileManager,
    IMessagePublisher messagePublisher) : CatalogServiceAppService, ICreateItemHandler
{
    public async Task<MessageContract<ItemModel>> Handle(CreateItemRequest request, CancellationToken cancellationToken)
    {

        var createItem = ObjectMapper.Map<CreateItemRequest, Item>(request);

        var result = await itemManager.CreateAsync(createItem, request.CategoryId, request.MenuId);

        var response = ObjectMapper.Map<Item, ItemModel>(result);

        if (request.ImageId is not null)
        {
            var files = new List<FinalizeModel>
            {
                new FinalizeModel()
                {
                    Id = request.ImageId??Guid.Empty,
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

        await messagePublisher.PublishAsync(new Message<ItemCreatedEvent>()
        {
            Type = nameof(ItemCreatedEvent),
            Payload = new ItemCreatedEvent()
            {
                ItemId = response.Id,
                ItemName = response.Name
            },
        }, null, cancellationToken);

        return MessageContract<ItemModel>.Success(response);
    }

}
