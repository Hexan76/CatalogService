using CatalogService.Application;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.Categories;

public class UpdateCategoryHandler(ICategoryRepository categoryRepository, IHttpClientService httpClientService, FileManager fileManager) : CatalogServiceAppService, IUpdateCategoryHandler
{
    public async Task<MessageContract<CategoryModel>> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {

        var founded = await categoryRepository.GetAsync(request.Id);

        ObjectMapper.Map(request, founded);

        var result = await categoryRepository.UpdateAsync(founded);

        var response = ObjectMapper.Map<Category, CategoryModel>(result);

        if (request.ImageId is not null)
        {
            var files = new List<FinalizeModel>
            {
                new FinalizeModel()
                {
                    AppType = ObjectStorages.StorageAppType.QasedFood,
                    FileName = result.Name,
                    EntityKey = result.Slug,
                    GenerateThumbnail=true,
                    Role="default",
                    StorageEntityType=ObjectStorages.StorageEntityType.Category,
                }
            };
            var finalizeResponse = await httpClientService.SendAsync<FinalizeFilesResponse>(new FinalizeRequest() { Files = files });

            if (finalizeResponse != null && finalizeResponse.Files.Any())
            {
                var file = finalizeResponse.Files.FirstOrDefault();

                var fileResult = await fileManager.AddOrUpdateRelatedFile(
                    result.Id,
                    typeof(Category).Name,
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

        return MessageContract<CategoryModel>.Success(response);
    }
}
