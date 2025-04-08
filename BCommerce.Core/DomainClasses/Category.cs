using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DomainClasses
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Spesification> Spesifications { get; set; }

        public MainCategory MainCategory { get; set; }

        public ICollection<Product> Products { get; set; }
    }

}
