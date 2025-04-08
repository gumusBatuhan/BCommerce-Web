using BCommerce.Application.Helpers;
using BCommerce.Application.Services;
using BCommerce.Core.DomainClasses;
using BCommerce.Core.DTO;
using BCommerce.Web.Helpers;
using BCommerce.Web.Models;
using BCommerce.Web.Options;
using BCommerceWeb.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BCommerce.Web.Controllers
{
    public class AuthController : Controller
    {

        private string _cookieKey = "Authorization";

        private readonly ApplicationUserContext applicationUserContext;
        private readonly UserService userService;
        private readonly IdentityService identityService;
        private readonly IOptions<JWTOptions> jwtoptions;
        public AuthController(IOptions<JWTOptions> jwtoptions, IdentityService identityService, ApplicationUserContext applicationUserContext, UserService userService)
        {

            this.jwtoptions = jwtoptions;
            this.identityService = identityService;
            this.applicationUserContext = applicationUserContext;
            this.userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Logout()
        {
            //cookieyi siler
            Response.Cookies.Delete(_cookieKey);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            //JWT TOKEN oluşturuldu
            var result = await identityService.LoginWithJwtAsync(model.Email, model.Password, jwtoptions.Value.SigningKey, jwtoptions.Value.Issuer, jwtoptions.Value.Audience);


            if (!result.IsSuccess)
            {
                ViewBag.Error = result.Errors[0];
                return View(model);
            }


            #region [OluşsturulanJWTTokenClientaCookielereEkletilir]


            var tokenObject = new
            {
                token = new JwtSecurityTokenHandler().WriteToken(result.Data),
                expires = result.Data.ValidTo
            };

            //jwt token client tarafından saklanmalı .

            //Bearer neden verdik?
            //.net idenetitiy her requeste JWT token kontrolünü  Authorization anahtarı içindeki
            ////Bearer dan sonraki kısmı kullanarak yapar.
            HttpContext.Response.Cookies.Append(_cookieKey, "Bearer " + tokenObject.token, new CookieOptions
            {
                Expires = tokenObject.expires

            });

            #endregion


            string userid = result.Data.Claims.FirstOrDefault(t => t.Type == "userid").Value;
            userService.MergeVistorBasketToUser(userid, applicationUserContext.VisitorToken);

            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser applicationUser = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                NameSurname = model.NameSurname
            };


            Result result = await identityService.Register(applicationUser, model.Password, "WebUser");


            if (!result.IsSuccess)
            {
                ViewBag.Error = result.Errors[0];
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
