﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParrotdiseShop.Core.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "{0} has to be in the range of {1} to {2}")]

        [Required(ErrorMessage = "The Display Order is required")]

        public int? DisplayOrder { get; set; }
    }
}
