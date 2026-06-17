using Framework.BuildingBlock.Application.Contracts;

using Volo.Abp.Application.Dtos;

namespace CatalogService.BusinessTypes;

public class UpdateBusinessTypeRequest : EntityDto<Guid>, IFrameworkRequest<BusinessTypeModel>
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? ImageId { get; set; }
}
