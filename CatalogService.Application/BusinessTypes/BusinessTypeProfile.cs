using AutoMapper;

namespace CatalogService.BusinessTypes;

public class BusinessTypeProfile : Profile
{
    public BusinessTypeProfile()
    {
        CreateMap<BusinessType, BusinessTypeModel>(memberList: MemberList.None)
            ;
        //TODO : remove after handle template
        CreateMap<BusinessTypeModel, BusinessType>(memberList: MemberList.None);
        CreateMap<CreateBusinessTypeRequest, BusinessType>(memberList: MemberList.None);
        CreateMap<UpdateBusinessTypeRequest, BusinessType>(memberList: MemberList.None);

    }
}
