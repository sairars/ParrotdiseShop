using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace ParrotdiseShop.Core.Models
{
    public class Category
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }
    }
}
