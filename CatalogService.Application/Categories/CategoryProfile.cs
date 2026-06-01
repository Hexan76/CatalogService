using AutoMapper;

namespace CatalogService.Categories;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryModel>(memberList: MemberList.None)
            ;
        //TODO : remove after handle template
        CreateMap<CategoryModel, Category>(memberList: MemberList.None);
        CreateMap<CreateCategoryRequest, Category>(memberList: MemberList.None);
        CreateMap<UpdateCategoryRequest, Category>(memberList: MemberList.None);
    }
}
