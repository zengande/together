using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WebMVC.Infrastructure.Exceptions;

namespace WebMVC.Infrastructure.Attributes
{
    public class ApiRequestExceptionHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ApiRequestExceptionHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (ApiRequestExcption ex)
            {
                var statusCode = context.Response.StatusCode;
                await HandleExceptionAsync(context, statusCode, ex.Message);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var data = new { code = statusCode.ToString(), is_success = false, msg = msg };
            var result = JsonConvert.SerializeObject(new { data = data });
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }

    public static class ApiRequestExceptionHandlingExtensions
    {
        public static IApplicationBuilder UseApiRequestExceptionHandling(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiRequestExceptionHandlingMiddleware>();
        }
    }
}
