﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DTO
{
    public class FilteredProductDTO
    {
        public int ProductCount { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
