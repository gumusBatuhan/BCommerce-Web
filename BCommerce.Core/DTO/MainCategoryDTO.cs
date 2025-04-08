using BCommerce.Core.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DTO
{
    public class MainCategoryDTO
    {
        public string Name { get; set; }

        public string Image { get; set; }

        public List<CategoryDTO> Categories { get; set; }
    }
}
