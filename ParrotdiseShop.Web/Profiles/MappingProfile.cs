using AutoMapper;
using ParrotdiseShop.Core.Dtos;
using ParrotdiseShop.Core.Models;

namespace ParrotdiseShop.Web.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>().ForMember(c => c.Id, opt => opt.Ignore());

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.ImagePath, opt => opt.MapFrom<CustomImageMapper>());

            CreateMap<ShoppingCartItem, ShoppingCartItemDto>();

            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>().ForMember(o => o.Id, opt => opt.Ignore());
        }

        public class CustomImageMapper : IValueResolver<ProductDto, Product, string>
        {
            public string Resolve(ProductDto product, Product productFromDb, string imagePath, ResolutionContext context)
            {
                return product.ImagePath ?? productFromDb.ImagePath;
            }
        }
    }
}
