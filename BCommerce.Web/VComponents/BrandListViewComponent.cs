using BCommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCommerce.Web.VComponents
{
    
    public class BrandListViewComponent : ViewComponent
    {
        private readonly BrandService brandService;
        public BrandListViewComponent(BrandService brandService)
        {
            this.brandService = brandService;
        }


        public async Task<Microsoft.AspNetCore.Mvc.IViewComponentResult> InvokeAsync()
        {

            var data = brandService.GetBrands(10);

            return View("/Views/Shared/_PartialMainBrandList.cshtml", data.Data);
        }

    }
}
