
using CatalogService.Application;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.Categories;

public class CreateCategoryHandler(ICategoryRepository categoryRepository, IHttpClientService httpClientService, FileManager fileManager) : CatalogServiceAppService, ICreateCategoryHandler
{
    public async Task<MessageContract<CategoryModel>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {

        var createItem = ObjectMapper.Map<CreateCategoryRequest, Category>(request);

        var result = await categoryRepository.InsertAsync(createItem);

        var response = ObjectMapper.Map<Category, CategoryModel>(result);

        if (request.File is not null)
        {
            var files = new List<FinalizeModel>
            {
                request.File
            };

            var finalizeResponse = await httpClientService.SendAsync<FinalizeFilesResponse>(new FinalizeRequest() { Files = files });

            if (finalizeResponse != null && finalizeResponse.Files.Any())
            {
                var file = finalizeResponse.Files.FirstOrDefault();
                var fileResult = await fileManager.AddRelatedFileWithEntity(file!.Id, result.Id, typeof(Category).Name, file.URL, file.FileName, request.File.Role);

                response.ImageUrl = fileResult.Url;
            }
        }

        return MessageContract<CategoryModel>.Success(response);
    }
}
