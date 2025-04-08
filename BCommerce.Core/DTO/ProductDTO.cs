using BCommerce.Core.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public bool IsFavorite { get; set; }

        public CategoryDTO Category { get; set; }


        public int BrandId { get; set; }

        public BrandDTO Brand { get; set; }

        public List<ProductImageDTO> Images { get; set; }
    }
}
