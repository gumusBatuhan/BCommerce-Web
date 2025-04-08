using System.Globalization;

namespace BCommerce.Web.Middlewares
{
    /// <summary>
    /// Jwt ının .net idenetity tarafından okunurken headerdan cekildiğini bildiğimiz için.
    /// Cookie e eklediğimiz Authorization parametresini alıp headera ekledik.
    /// </summary>
    public class BeererControlMiddleware
    {
        private readonly RequestDelegate next;
        public BeererControlMiddleware(RequestDelegate requestDelegate)
        {
            this.next = requestDelegate;
        }

        public Task Invoke(HttpContext httpContext)
        {

            if (httpContext.Request.Cookies["Authorization"] != null)
            {

                httpContext.Request.Headers.Add("Authorization", httpContext.Request.Cookies["Authorization"]);
            }


            return next(httpContext);
        }
    }
}
