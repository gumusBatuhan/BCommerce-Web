using BCommerce.Core.DTO;

namespace BCommerce.Web.Models
{
    public class FilterAreaModel
    {
        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public List<FilterBrand> Brands { get; set; } = new List<FilterBrand>();
        public List<int> SelectedBrands { get; set; }=new List<int>();

        public List<SpesificationDTO> Spesifications { get; set; }
        public List<int> SelectedSpesifications { get; set; }
    }

    

}
