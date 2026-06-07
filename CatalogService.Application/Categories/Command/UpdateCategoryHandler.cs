using System.Text.RegularExpressions;

using CatalogService.Application;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.Categories;

public class UpdateCategoryHandler(CategoryManager categoryManager, IHttpClientService httpClientService, FileManager fileManager) : CatalogServiceAppService, IUpdateCategoryHandler
{
    public async Task<MessageContract<CategoryModel>> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
    {

        var result = await categoryManager.UpdateCategoryAsync(request.Id, request.Name, request.Description);

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
