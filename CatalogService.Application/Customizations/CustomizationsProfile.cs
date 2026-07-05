using AutoMapper;

using CatalogService.CategoryCustomization;

using Volo.Abp.AutoMapper;

namespace CatalogService.Customizations;

public class CustomizationsProfile : Profile
{
    public CustomizationsProfile()
    {
        CreateMap<Customization, CustomizationModel>(memberList: MemberList.None)
            ;
        CreateMap<CustomizationModel, Customization>(memberList: MemberList.None);
        CreateMap<CreateCustomizationRequest, Customization>(memberList: MemberList.None)
            .Ignore(x=>x.Items)
            ;

        CreateMap<CreateCustomizationGroupRequest, CustomizationGroup>(memberList: MemberList.None);
        CreateMap<CustomizationGroup, CustomizationGroupModel>(memberList: MemberList.None);

    }
}
