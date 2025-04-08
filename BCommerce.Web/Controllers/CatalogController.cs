using BCommerce.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCommerce.Web.Controllers
{
    public class CatalogController : Controller
    {
        private readonly CatalogService _catalogService;

        public CatalogController(CatalogService catalogService)
        {
            _catalogService = catalogService;

        }

        public IActionResult All()
        {

            var result = _catalogService.GetAllCategory();

            return View(result.Data);
        }
    }
}
