﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Models.ViewModels.Base
{
    public class ProductAndCategoryBase : EntityBase
    {
        public int CategoryId { get; set; }

        [Display(Name = "Cateogry")]
        public string CategoryName { get; set; }

        public int ProductId { get; set; }

        [MaxLength(3800)]
        public string Description { get; set; }

        [MaxLength(50)]
        [Display(Name = "Model")]
        public string ModelName { get; set; }

        [Display(Name = "Is Featured Product")]
        public bool IsFeatured { get; set; }

        [MaxLength(50)]
        [Display(Name = "Model Number")]
        public string ModelNumber { get; set; }

        [MaxLength(150)]
        public string ProductImage { get; set; }

        [MaxLength(150)]
        public string ProductImageLarge { get; set; }

        [MaxLength(150)]
        public string ProductImageThumb { get; set; }

        [DataType(DataType.Currency), Display(Name = "Price")]
        public decimal CurrentPrice { get; set; }

        [Display(Name = "In Stock")]
        public int UnitsInStock { get; set; }
    }
}
