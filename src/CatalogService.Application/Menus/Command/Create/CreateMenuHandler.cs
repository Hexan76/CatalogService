using System.Text.RegularExpressions;

using AutoMapper;

using Framework.BuildingBlock.Application.Contracts;

namespace CatalogService.Menus;

public class CreateMenuHandler(IMenuRepository menuRepository, IMapper mapper) : ICreateMenuHandler
{
    public async Task<MessageContract<MenuModel>> Handle(CreateMenuRequest request, CancellationToken cancellationToken)
    {

        var createItem = mapper.Map<CreateMenuRequest, Menu>(request);
        createItem.Slug = GenerateSlug(createItem.Name);
        var result = await menuRepository.InsertAsync(createItem);

        var response = mapper.Map<Menu, MenuModel>(result);


        return MessageContract<MenuModel>.Success(response);
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
