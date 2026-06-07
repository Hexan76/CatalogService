
using System.Text.RegularExpressions;

using CatalogService.Application;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.Categories;

public class CreateCategoryHandler(CategoryManager categoryManager, IHttpClientService httpClientService, FileManager fileManager) : CatalogServiceAppService, ICreateCategoryHandler
{
    public async Task<MessageContract<CategoryModel>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {

        var createItem = ObjectMapper.Map<CreateCategoryRequest, Category>(request);

        var result = await categoryManager.CreateCategoryAsync(createItem.Name, createItem.Description);

        var response = ObjectMapper.Map<Category, CategoryModel>(result);

        if (request.ImageId is not null)
        {
            var files = new List<FinalizeModel>
            {
                new FinalizeModel()
                {
                    Id = request.ImageId??Guid.Empty,
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

                var fileResult = await fileManager.AddRelatedFileWithEntity(
                    file!.Id,
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
