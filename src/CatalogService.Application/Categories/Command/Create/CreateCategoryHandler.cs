using AutoMapper;

using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;
using Framework.HttpClient.Abstractions;

namespace CatalogService.Categories;

public class CreateCategoryHandler(
    CategoryManager categoryManager,
    IMapper mapper,
    IHttpClientService httpClientService,
    FileManager fileManager) : ICreateCategoryHandler
{
    public async Task<MessageContract<CategoryModel>> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {

        var createItem = mapper.Map<CreateCategoryRequest, Category>(request);

        var result = await categoryManager.CreateCategoryAsync(createItem.Name, createItem.Description, request.BusinessTypeId);

        var response = mapper.Map<Category, CategoryModel>(result);

        if (request.ImageId is not null)
        {
            var files = new List<FinalizeModel>
            {
                new FinalizeModel()
                {
                    Id = request.ImageId??Guid.Empty,
                    AppType = "QasedFood",
                    FileName = result.Name,
                    EntityKey = result.Slug,
                    GenerateThumbnail=true,
                    Role="default",
                    StorageEntityType="Category",
                }
            };

            var finalizeResponse = await httpClientService.SendAsync<FinalizeFilesResponse>(new FinalizeRequest() { Files = files });

            if (finalizeResponse != null && finalizeResponse.Files.Any())
            {
                var file = finalizeResponse.Files.FirstOrDefault();

                var fileResult = await fileManager.AddRelatedFileWithEntity(
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
