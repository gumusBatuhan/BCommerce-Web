namespace BCommerce.Web.Middlewares
{
    public class VisitorControlMiddleware
    {

        private readonly RequestDelegate next;
        public VisitorControlMiddleware(RequestDelegate requestDelegate)
        {
            this.next = requestDelegate;
        }

        public Task Invoke(HttpContext httpContext)
        {

            //if (httpContext.Request.Cookies["Authorization"] != null)
            //{

            //    httpContext.Request.Headers.Add("Authorization", httpContext.Request.Cookies["Authorization"]);
            //}

            if (!httpContext.User.Identity.IsAuthenticated)
            {

                if (!httpContext.Request.Cookies.ContainsKey("VisitorToken"))
                {
                    string visitorToken = Guid.NewGuid().ToString();
                    httpContext.Response.Cookies.Append("VisitorToken", visitorToken, new CookieOptions
                    {
                        Expires = DateTimeOffset.UtcNow.AddDays(20)

                    });
                    httpContext.Request.Headers.Add("VisitorToken", visitorToken);
                }
            }
            else
            {
                httpContext.Response.Cookies.Delete("VisitorToken");
            }

            return next(httpContext);
        }
    }
}
