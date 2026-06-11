using AutoMapper;

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

    }
}
