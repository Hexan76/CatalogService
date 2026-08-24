using AutoMapper;

namespace CatalogService.Menus;

public class MenusProfile : Profile
{
    public MenusProfile()
    {
        CreateMap<Menu, MenuModel>(memberList: MemberList.None)
            ;
        //TODO : remove after handle template
        CreateMap<MenuModel, Menu>(memberList: MemberList.None);
        CreateMap<CreateMenuRequest, Menu>(memberList: MemberList.None);
        CreateMap<UpdateMenuRequest, Menu>(memberList: MemberList.None);

    }
}
