using BCommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCommerce.Web.Controllers
{
    public class BrandController : Controller
    {
        private readonly BrandService brandService;
        public BrandController(BrandService brandService)
        {
            this.brandService = brandService;
        }


        public IActionResult List()
        {

            var result = brandService.GetBrandsWithFirstLetterGrouped();


            return View(result.Data);
        }
    }
}
