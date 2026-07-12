using CatalogService.Application;
using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.Customizations.Command;

public class CreateCustomizationHandler(IHttpClientService httpClientService, CustomizationManager customizationManager, FileManager fileManager) : CatalogServiceAppService, ICreateCustomizationHandler
{
    public async Task<MessageContract<CustomizationModel>> Handle(
        CreateCustomizationRequest request,
        CancellationToken cancellationToken)
    {

        var createItem = ObjectMapper.Map<CreateCustomizationRequest, Customization>(request);

        var result = await customizationManager.CreateAsync(createItem, request.GroupId, request.Items);

        var response = ObjectMapper.Map<Customization, CustomizationModel>(result);

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
                    StorageEntityType="Customization",
                }
            };

            var finalizeResponse = await httpClientService.SendAsync<FinalizeFilesResponse>(new FinalizeRequest() { Files = files });

            if (finalizeResponse != null && finalizeResponse.Files.Any())
            {
                var file = finalizeResponse.Files.FirstOrDefault();

                var fileResult = await fileManager.AddRelatedFileWithEntity(
                    result.Id,
                    typeof(Customization).Name,
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
        return MessageContract<CustomizationModel>.Success(response);
    }
}
