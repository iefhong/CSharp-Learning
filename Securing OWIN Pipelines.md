# Securing OWIN Pipelines
* Add CookieAuthenticationMiddleware
    - Install  
    install-package microsoft.owin.security.cookies
    - Configuration  
    ``` Startup.cs
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
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

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);

            app.UseNancy(conf =>
            {
                conf.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            });

        }
    }    
    ```
* Authenticate user  
    - IAuthenticationManager.SignIn()
* Use the user and the user's claims
* Log out user
    * IAuthenticationManager.SignOut()    