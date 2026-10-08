using AutoMapper;

using CatalogService.CategoryCustomization;

using Volo.Abp.AutoMapper;

namespace CatalogService.Customizations;

public class CustomizationsProfile : Profile
{
    public CustomizationsProfile()
    {
        CreateMap<Customization, CustomizationModel>(memberList: MemberList.None)
            .ForMember(x => x.GroupId, o => o.MapFrom(src => src.CustomizationGroupId))
            .ForMember(x => x.GroupTitle, o => o.MapFrom(src => src.CustomizationGroup == null ? null : src.CustomizationGroup.Name))
            ;
        CreateMap<CustomizationModel, Customization>(memberList: MemberList.None);
        CreateMap<CreateCustomizationRequest, Customization>(memberList: MemberList.None)
            .Ignore(x=>x.Items)
            ;

        CreateMap<CreateCustomizationGroupRequest, CustomizationGroup>(memberList: MemberList.None);
        CreateMap<CustomizationGroup, CustomizationGroupModel>(memberList: MemberList.None);

    }
}
