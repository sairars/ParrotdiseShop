using System.ComponentModel.DataAnnotations;

namespace ParrotdiseShop.Core.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [Display(Name = "Display Order")]
        [Range(1, 100)]
        public int DisplayOrder { get; set; }
    }
}
