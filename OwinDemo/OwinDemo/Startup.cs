using Owin;
using OwinDemo.Middleware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Nancy.Owin;
using Nancy;

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

            app.UseDebugMiddleware(new DebugMiddlewareOptions
            {
                OnIncomingRequest = (ctx) =>
                {
                    var watch = new Stopwatch();
                    watch.Start();
                    ctx.Environment["DebugStopwatch"] = watch;
                },
                OnOutgoingRequest = (ctx) =>
                {
                    var watch = (Stopwatch)ctx.Environment["DebugStopwatch"];
                    watch.Stop();
                    Debug.WriteLine("Request took: " + watch.ElapsedMilliseconds + " ms");
                }

            });

            //app.Use<DebugMiddleware>();

            //app.UseNancy();

            //app.Map("/nancy", mappedApp => mappedApp.UseNancy());

            app.UseNancy(config =>
            {
                config.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            });

            app.Use(async (ctx, next) => {
                await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
            });
        }
    }
}