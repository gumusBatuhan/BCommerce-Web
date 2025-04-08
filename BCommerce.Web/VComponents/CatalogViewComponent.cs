using BCommerce.Application.Services;
using BCommerce.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BCommerce.Web.VComponents
{
    public class CatalogViewComponent : ViewComponent
    {
        private readonly CatalogService catalogService;
        public CatalogViewComponent(CatalogService catalogService)
        {
            this.catalogService = catalogService;
        }


        public async Task<Microsoft.AspNetCore.Mvc.IViewComponentResult> InvokeAsync()
        {
            //veri tabanında tüm main ve sub categoryleri cekmesi lazım
            Result<List<MainCategoryDTO>> result = catalogService.GetAllCategory();

            return View("/Views/Shared/_ParitalCatalog.cshtml", result.Data);

        }
    }
}
