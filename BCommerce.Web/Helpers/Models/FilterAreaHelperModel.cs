namespace BCommerce.Web.Helpers.Models
{
    public class FilterAreaHelperModel
    {
        public decimal? MinPrice { get; set; }

        public decimal? MaxPrice { get; set; }

        public List<int> SelectedBrandIds { get; set; } = new List<int>();

        public List<int> SelectedSpesificationIds { get; set; } = new List<int>();
    }
}
