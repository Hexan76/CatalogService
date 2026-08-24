using AutoMapper;

using CatalogService.ObjectStorageService;

namespace CatalogService.Application;

public class CatalogServiceApplicationAutoMapperProfile : Profile
{
    public CatalogServiceApplicationAutoMapperProfile()
    {
        CreateMap<FinalizeModel, FinalizeRequest>(memberList: MemberList.None);
    }
}
