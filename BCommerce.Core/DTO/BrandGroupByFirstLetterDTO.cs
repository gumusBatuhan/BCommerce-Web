using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCommerce.Core.DTO
{
    public class BrandGroupByFirstLetterDTO
    {
        public List<BrandGroupByFirstLetterItemDTO> AllBrands { get; set; }
    }

    public class BrandGroupByFirstLetterItemDTO
    {
        public string FirstLetter { get; set; }

        public List<BrandDTO> Brands { get; set; }
    }
}
