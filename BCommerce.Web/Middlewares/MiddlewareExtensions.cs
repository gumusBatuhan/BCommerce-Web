namespace BCommerce.Web.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder AddWBeererControlMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BeererControlMiddleware>();
        }
        public static IApplicationBuilder AddVisitorControl(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<VisitorControlMiddleware>();
        }
    }
}
