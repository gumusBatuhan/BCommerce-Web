using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DomainClasses
{
    public class ProductSpesification:BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }    

        public int SpesificationId { get; set; }

        public Spesification Spesification { get; set; }


        public int SpesificationItemId { get; set; }

        public SpesificationItem SpesificationItem { get; set;}
    }
}
