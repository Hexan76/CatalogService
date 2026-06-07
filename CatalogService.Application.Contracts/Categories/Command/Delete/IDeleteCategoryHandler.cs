using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Categories;

public interface IDeleteCategoryHandler : IFrameworkRequestHandler<DeleteCategoryRequest, BaseResponse>
{
}
