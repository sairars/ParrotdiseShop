using ParrotdiseShop.Core.Dtos;

namespace ParrotdiseShop.Core.ViewModels
{
    public class ProductViewModel
    {
        public IEnumerable<ProductDto> Products { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}
