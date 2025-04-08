using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DomainClasses
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }


        public int BrandId { get; set; }

        public Brand Brand { get; set; }


        //1 PRODUCT IN N tane resmi olabilir.
        public ICollection<ProductImage> Images { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }

        public ICollection<ProductSpesification> ProductSpesifications { get; set; }
    }
}
