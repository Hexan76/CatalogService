using AutoMapper;

using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.BusinessTypes;

public class UpdateBusinessTypeHandler(
    IBusinessTypeRepository businessTypeRepository,
    IHttpClientService httpClientService,
    IMapper mapper,
    FileManager fileManager) : IUpdateBusinessTypeHandler
{
    public async Task<MessageContract<BusinessTypeModel>> Handle(UpdateBusinessTypeRequest request, CancellationToken cancellationToken)
    {
        var founded = await businessTypeRepository.GetAsync(request.Id);

        mapper.Map(request, founded);

        var result = await businessTypeRepository.UpdateAsync(founded);

        var response = mapper.Map<BusinessType, BusinessTypeModel>(result);

        if (request.ImageId is not null)
        {
            var files = new List<FinalizeModel>
            {
                new FinalizeModel()
                {
                    Id=request.ImageId.Value,
                    AppType = "QasedFood",
                    FileName = result.Name,
                    EntityKey = result.Name,
                    GenerateThumbnail=true,
                    Role="default",
                    StorageEntityType="BusinessType",
                }
            };
            var finalizeResponse = await httpClientService.SendAsync<FinalizeFilesResponse>(new FinalizeRequest() { Files = files }, ResponseType.Default, "ObjectStorageClient");

            if (finalizeResponse != null && finalizeResponse.Files.Any())
            {
                var file = finalizeResponse.Files.FirstOrDefault();

                var fileResult = await fileManager.AddOrUpdateRelatedFile(
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
