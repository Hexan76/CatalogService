using AutoMapper;

using Volo.Abp.AutoMapper;

namespace CatalogService.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryModel>(memberList: MemberList.None)
            .Ignore(c => c.IconUrl)
            .Ignore(c => c.BannerUrl)
            ;
        //TODO : remove after handle template
        CreateMap<CategoryModel, Category>(memberList: MemberList.None);
        CreateMap<CreateCategoryRequest, Category>(memberList: MemberList.None);
        CreateMap<UpdateCategoryRequest, Category>(memberList: MemberList.None);
    }
}
