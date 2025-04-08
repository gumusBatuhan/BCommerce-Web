using BCommerce.Application.Services;
using BCommerce.Core.DTO;
using BCommerce.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace BCommerce.Web.VComponents
{
    public class UserInfoViewComponent : ViewComponent
    {
        private readonly UserService userService;
        private readonly ApplicationUserContext applicationUserContext;

        public UserInfoViewComponent(UserService userService, ApplicationUserContext applicationUserContext)
        {
            this.userService = userService;
            this.applicationUserContext = applicationUserContext;

        }
        public async Task<Microsoft.AspNetCore.Mvc.IViewComponentResult> InvokeAsync()
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




            return View("/Views/Shared/_PartialUserInfo.cshtml", userContext);

        }
    }
}
