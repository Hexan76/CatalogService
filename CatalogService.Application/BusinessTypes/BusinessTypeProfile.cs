using AutoMapper;

namespace CatalogService.BusinessTypes;

public class BusinessTypeProfile : Profile
{
    public BusinessTypeProfile()
    {
        CreateMap<BusinessType, BusinessTypeModel>(memberList: MemberList.None)
            //.ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryBusinessTypes.Select(x => x.Category)))
            ;
        //TODO : remove after handle template
        CreateMap<BusinessTypeModel, BusinessType>(memberList: MemberList.None);
        CreateMap<CreateBusinessTypeRequest, BusinessType>(memberList: MemberList.None);
        CreateMap<UpdateBusinessTypeRequest, BusinessType>(memberList: MemberList.None);

    }
}
