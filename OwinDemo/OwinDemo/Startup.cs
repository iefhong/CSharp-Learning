using Owin;
using OwinDemo.Middleware;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Nancy.Owin;
using Nancy;
using System.Web.Http;

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

            app.UseCookieAuthentication(new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new Microsoft.Owin.PathString("/Auth/Login")
            });


            app.UseFacebookAuthentication(new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions {
                AppId= "226126538131701",
                AppSecret = "",
                SignInAsAuthenticationType = "ApplicationCookie"
            });

            //app.Use<DebugMiddleware>();

            //app.UseNancy();

            app.Map("/nancy", mappedApp => mappedApp.UseNancy());

            app.Use(async (ctx, next) => {
                if (ctx.Authentication.User.Identity.IsAuthenticated)
                    Debug.WriteLine("User: " + ctx.Authentication.User.Identity.Name);
                else
                    Debug.WriteLine("User Not Authenticated");
                await next();
            });

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);

            //app.UseNancy(conf =>
            //{
            //    conf.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            //});

            //app.Use(async (ctx, next) => {
            //    await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
            //});
        }
    }
}