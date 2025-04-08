using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DomainClasses
{
    public class Basket : BaseEntity
    {
        public string? UserId { get; set; }

        public Guid? VisitorToken { get; set; }

        public ICollection<BasketItem> BasketItems { get; set; }
    }
}
