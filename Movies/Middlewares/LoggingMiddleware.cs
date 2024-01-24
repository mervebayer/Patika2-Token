using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Movies.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate next;
    public LoggingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }
     public async Task Invoke(HttpContext context)
    {
        string action = context.GetEndpoint()?.DisplayName;

        Console.WriteLine($"Action '{action}' is invoked at {DateTime.Now}");

        await next.Invoke(context);
    }
}
    static public class MoviesMiddlewareExtension
    {
        public static IApplicationBuilder GlobalLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
   
