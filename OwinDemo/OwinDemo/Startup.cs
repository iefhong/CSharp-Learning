using Owin;
using OwinDemo.Middleware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace OwinDemo
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            //app.Use(async (ctx, next) => {
            //    Debug.WriteLine("Incoming request: " + ctx.Request.Path);
            //    await next();
            //    Debug.WriteLine("Incoming request: " + ctx.Request.Path);
            //});

            app.Use<DebugMiddleware>(new DebugMiddlewareOptions());

            app.Use(async (ctx, next) => {
                await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
            });
        }
    }
}