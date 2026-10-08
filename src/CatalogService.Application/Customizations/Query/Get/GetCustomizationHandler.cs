using AutoMapper;

using CatalogService.Files;
using CatalogService.ObjectStorageService;

using Framework.BuildingBlock.Application.Contracts;

using Microsoft.EntityFrameworkCore;

namespace CatalogService.Customizations;

public class GetCustomizationHandler(ICustomizationRepository customizationRepository,
                                     IFileRepository fileRepository,
                                     IMapper mapper) : IGetCustomizationHandler
{
    public async Task<MessageContract<CustomizationModel>> Handle(GetCustomizationRequest request, CancellationToken cancellationToken)
    {
        var customization = await customizationRepository.GetQueryable()
            .Include(x => x.CustomizationGroup)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        var response = mapper.Map<Customization, CustomizationModel>(customization);

        if (response is not null)
        {
            var files = await fileRepository.GetPrimaryFilesAsync([response.Id], cancellationToken);

            if (files.TryGetValue(response.Id, out var file))
            {
                response.Image = new FileUrlModel
                {
                    Role = file.Role,
                    Variants = file.Variants
                };
            }
        }

        return MessageContract<CustomizationModel>.Success(response);
    }
}
