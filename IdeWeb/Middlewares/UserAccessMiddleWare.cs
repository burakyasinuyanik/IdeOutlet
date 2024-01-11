using Microsoft.AspNetCore.Http;

namespace Ide.Web.Middlewares
{
    public class UserAccessMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var controllerName = context.Request.RouteValues["controller"]?.ToString();
            var actionName = context.Request.RouteValues["action"]?.ToString();

            await next(context); // Bir sonraki middleware'e devret.
          
        }
    }
}
