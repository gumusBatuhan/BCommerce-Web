using BCommerce.Application.Services;
using BCommerce.Core.DTO;
using BCommerce.Web.Helpers;
using BCommerce.Web.Helpers.Models;
using BCommerce.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BCommerce.Web.Controllers
{
    public class ProductController : Controller
    {


        private readonly UserService userService;
        private readonly ApplicationUserContext applicationUserContext;
        private readonly QueryHelper queryHelper;
        private readonly ProductService productService;
        public ProductController(UserService userService, ApplicationUserContext applicationUserContext, QueryHelper queryHelper, ProductService productService)
        {
            this.userService = userService;
            this.applicationUserContext = applicationUserContext;
            this.queryHelper = queryHelper;
            this.productService = productService;
        }


        public IActionResult AddToFavorite(int productid, bool isadd)
        {

            if (!applicationUserContext.IsAutenticated)
            {

                return Json(Result.Error("Bu işlemi yapmak için login olunuz", 1));
            }

            //db de bu user için bu ürünün favori oldugunu set et
            Result result = userService.AddToFavorite(applicationUserContext.UserId, productid, isadd);

            // db de favori set etme işlemi başarılı ise
            if (result.IsSuccess)
            {

                return PartialView("_PartialFavoriteView", new FavoriteViewModel
                {
                    IsFavorite = isadd,
                    ProductId = productid

                });

            }

            return Problem(detail: "İşlem sırasında bi hata olustu", statusCode: 500);




        }


        public async Task<IActionResult> All()
        {
            FilterAreaHelperModel filterAreaHelperModel = queryHelper.CreateFilterModelFromQueryString();


            var filterProductResult = productService.GetFilteredProducts(applicationUserContext.UserId, new ProductFilterModelDTO
            {
                MinPrice = filterAreaHelperModel.MinPrice,
                MaxPrice = filterAreaHelperModel.MaxPrice,
                Brands = filterAreaHelperModel.SelectedBrandIds,
                Spesifications=filterAreaHelperModel.SelectedSpesificationIds
            });

            return View(filterProductResult.Data);
        }

        [HttpPost]
        public async Task<IActionResult> All(FilterAreaModel filterAreaModel)
        {
            string query = queryHelper.CreateQueyFromFilterData(filterAreaModel);


            return Redirect($"/Product/All{query}");

        }

    }
}
