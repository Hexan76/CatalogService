using AutoMapper;

using CatalogService.Customizations;

namespace CatalogService.Items;

public class ItemsProfile : Profile
{
    public ItemsProfile()
    {

        CreateMap<Item, ItemModel>(memberList: MemberList.None)
            .ForMember(x => x.Image, o => o.Ignore())
            .ForMember(x => x.Rate, o => o.Ignore())
            .ForMember(x => x.CustomizationGroups,
                o => o.MapFrom<ItemCustomizationGroupsResolver>());

        CreateMap<Item, SearchItemModel>(memberList: MemberList.None)
            ;

        //TODO : remove after handle template
        CreateMap<ItemModel, Item>(memberList: MemberList.None);
        CreateMap<CreateItemRequest, Item>(memberList: MemberList.None)
            .ForMember(x => x.Ingredients, o => o.MapFrom(src => string.Join('|', src.IngredientsArray)))
            .ForMember(x => x.Customizations, o => o.MapFrom(src => MapCustomizations(src.CusomizationIds)))
            ;
        CreateMap<UpdateItemRequest, Item>(memberList: MemberList.None)
            .ForMember(x => x.Ingredients, o => o.MapFrom(src => string.Join('|', src.IngredientsArray)))
            .ForMember(x => x.Customizations, o => o.MapFrom(src => MapCustomizations(src.CusomizationIds)))
            ;

        CreateMap<CustomizationModel, ItemCustomization>(memberList: MemberList.None);
        CreateMap<ItemCustomization, CustomizationModel>(memberList: MemberList.None)
            .IncludeMembers(x => x.Customization)
            ;
    }

    private static List<ItemCustomization> MapCustomizations(long[]? customizationIds)
        => (customizationIds ?? [])
            .Where(id => id > 0)
            .Distinct()
            .Select(id => new ItemCustomization { CustomizationId = id })
            .ToList();
}
public class ItemCustomizationGroupsResolver
    : IValueResolver<Item, ItemModel, ICollection<CustomizationGroupModel>>
{
    public ICollection<CustomizationGroupModel> Resolve(
        Item source,
        ItemModel destination,
        ICollection<CustomizationGroupModel> destMember,
        ResolutionContext context)
    {
        return source.Customizations
            .Select(x => x.Customization)
            .GroupBy(x => x.CustomizationGroup)
            .Select(group =>
            {
                var dto = context.Mapper.Map<CustomizationGroupModel>(group.Key);

                dto.Customizations = context.Mapper.Map<List<CustomizationModel>>(group);

                return dto;
            })
            .OrderBy(x => x.SortOrder)
            .ToList();
    }
}
