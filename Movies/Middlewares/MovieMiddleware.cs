using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Movies.Middlewares
{
    public class MovieMiddleware
    {
        private readonly RequestDelegate next;
        public  MovieMiddleware(RequestDelegate next)
        {
            this.next=next;
        }  
        public async  Task Invoke(HttpContext context)
        {
            Console.WriteLine("Actiona girildi");
            await next.Invoke(context);
            Console.WriteLine("Actiondan çıkıldı!");
        }
    }
    static public class MovieMiddlewareExtension
    {
        public static IApplicationBuilder UseMovie(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieMiddleware>();
        }
    }
}