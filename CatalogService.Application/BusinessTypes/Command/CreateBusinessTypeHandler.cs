using CatalogService.Application;
using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.BusinessTypes;

public class CreateBusinessTypeHandler(IBusinessTypeRepository businessTypeRepository, IHttpClientService httpClientService, FileManager fileManager) : CatalogServiceAppService, ICreateBusinessTypeHandler
{
    public async Task<MessageContract<BusinessTypeModel>> Handle(CreateBusinessTypeRequest request, CancellationToken cancellationToken)
    {

        var createItem = ObjectMapper.Map<CreateBusinessTypeRequest, BusinessType>(request);

        var result = await businessTypeRepository.InsertAsync(createItem);

        var response = ObjectMapper.Map<BusinessType, BusinessTypeModel>(result);

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
                    StorageEntityType="BusinessType",
                }
            };

            var finalizeResponse = await httpClientService.SendAsync<FinalizeFilesResponse>(new FinalizeRequest() { Files = files });

            if (finalizeResponse != null && finalizeResponse.Files.Any())
            {
                var file = finalizeResponse.Files.FirstOrDefault();

                var fileResult = await fileManager.AddRelatedFileWithEntity(
                    result.Id,
                    typeof(BusinessType).Name,
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

        return MessageContract<BusinessTypeModel>.Success(response);
    }

}
