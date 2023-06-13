using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParrotdiseShop.Core.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "The Display Order is required")]
        [Range(1, 100, ErrorMessage = "{0} has to be in the range of {1} to {2}")]
        public int? DisplayOrder { get; set; }
    }
}
