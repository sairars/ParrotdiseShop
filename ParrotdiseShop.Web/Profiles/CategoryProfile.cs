using AutoMapper;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Web.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>().ForMember(c => c.Id, opt => opt.Ignore());
        }
    }
}
