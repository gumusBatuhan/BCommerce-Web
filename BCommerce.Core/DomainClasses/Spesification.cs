using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DomainClasses
{
    public class Spesification:BaseEntity
    {
        public string Name { get; set; }

        public ICollection<SpesificationItem> SpesificationItems { get; set; }


        public ICollection<Category> Categories { get; set; }
    }
}
