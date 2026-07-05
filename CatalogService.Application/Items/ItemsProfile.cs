using AutoMapper;

using CatalogService.Customizations;

namespace CatalogService.Items;

public class ItemsProfile : Profile
{
    public ItemsProfile()
    {
        CreateMap<Item, ItemModel>(memberList: MemberList.None)
            ;
        //TODO : remove after handle template
        CreateMap<ItemModel, Item>(memberList: MemberList.None);
        CreateMap<CreateItemRequest, Item>(memberList: MemberList.None);
        CreateMap<UpdateItemRequest, Item>(memberList: MemberList.None);

        CreateMap<VariantModel, ItemVariant>(memberList: MemberList.None);
        CreateMap<ItemVariant, VariantModel>(memberList: MemberList.None);

        CreateMap<CustomizationModel, ItemCustomization>(memberList: MemberList.None);
        CreateMap<ItemCustomization, CustomizationModel>(memberList: MemberList.None)
            .IncludeMembers(x => x.Customization)
            ;
    }
}
