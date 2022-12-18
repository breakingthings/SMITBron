using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SMITBron.Web.Middlewares
{
    public class RequestInfoHeadersMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.Headers.Add("X-Request-Path", context.Request.Path.Value);
            await next(context);
        }
    }
}
