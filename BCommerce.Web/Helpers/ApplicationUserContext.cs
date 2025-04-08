using BCommerce.Application.Services;
using BCommerce.Core.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace BCommerce.Web.Helpers
{
    public class ApplicationUserContext
    {

        private readonly IHttpContextAccessor _contextAccessor;
        public ApplicationUserContext(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public bool IsAutenticated
        {
            get
            {
                return _contextAccessor.HttpContext.User.Identity.IsAuthenticated;
            }
        }


        public string UserId
        {
            get
            {
                if (!_contextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    return null;
                }

                return _contextAccessor.HttpContext.User.Claims.FirstOrDefault(t => t.Type == "userid").Value;

            }
        }


        public Guid? VisitorToken
        {
            get
            {

                StringValues visitorTokenInHeader;
                _contextAccessor.HttpContext.Request.Headers.TryGetValue("VisitorToken", out visitorTokenInHeader);


                if (visitorTokenInHeader.Count == 0)
                {

                    string visitorToken;
                    _contextAccessor.HttpContext.Request.Cookies.TryGetValue("VisitorToken", out visitorToken);

                    return !string.IsNullOrEmpty(visitorToken) ? Guid.Parse(visitorToken) : null;
                }
                else
                {
                    return Guid.Parse(visitorTokenInHeader[0].ToString());
                }

            }
        }
    }
}
