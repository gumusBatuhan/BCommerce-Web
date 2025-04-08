using BCommerce.Application.Services;
using BCommerce.Insfrastructure.Data;
using BCommerce.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BCommerce.Web.VComponents
{
    public class TrendProductViewComponent : ViewComponent
    {
        private readonly ProductService productService;
        private readonly ApplicationUserContext applicationUserContext;
        public TrendProductViewComponent(ProductService productService, ApplicationUserContext applicationUserContext)
        {
            this.productService = productService;
            this.applicationUserContext = applicationUserContext;
        }

        public async Task<Microsoft.AspNetCore.Mvc.IViewComponentResult> InvokeAsync()
        {
            var trendProdutResult = productService.GetTrendProducts(applicationUserContext.UserId);

            return View("/Views/Shared/_PartialTrendProducts.cshtml", trendProdutResult.Data);

        }
    }
}
