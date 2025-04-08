using BCommerce.Application.Services;
using BCommerce.Core.DTO;
using BCommerce.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BCommerce.Web.Controllers
{
    public class BasketController : Controller
    {
        private readonly UserService userService;
        private readonly ApplicationUserContext applicationUserContext;
        public BasketController(UserService userService, ApplicationUserContext applicationUserContext)
        {
            this.userService = userService;
            this.applicationUserContext = applicationUserContext;
        }


        [HttpPost]
        public IActionResult SetItemCount(int itemid, int newcount)
        {

            var result = userService.UpdateItemCount(applicationUserContext.UserId, applicationUserContext.VisitorToken, itemid, newcount);

            return Json(result);
        }

        [HttpGet]
        public IActionResult GetUserBasket()
        {

            var result = userService.GetUserBasket(applicationUserContext.UserId, applicationUserContext.VisitorToken);

            return PartialView("_ParitalBasket", result.Data);
        }

        [HttpPost]
        public IActionResult AddItem(int productId)
        {

            

            Result result = userService.AddItem(applicationUserContext.UserId, applicationUserContext.VisitorToken, productId, 1);

            return Json(result);
        }

        [HttpGet]
        public IActionResult GetBasketNotificationCount()
        {

            UserContext userContext = new UserContext();

            if (!applicationUserContext.IsAutenticated)
            {
                userContext = userService.GetUserContext(null, applicationUserContext.VisitorToken).Data;
            }
            else
            {
                userContext = userService.GetUserContext(applicationUserContext.UserId, null).Data;
            }

            Result<int> result = new Result<int>();
            result.IsSuccess = true;
            result.Data = userContext.BasketItemCount;
            return Json(result);
        }
    }
}
