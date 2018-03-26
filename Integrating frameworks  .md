* Intergrating Frameworks  
    - [NancyFx](www.nancyfx.org)
    - [Web Api](www.asp.net)

# NancyFx
* Install
    install-package nancy.owin
* Adding NancyModule    
    ``` NancyDemoModule.cs
    public class NancyDemoModule : NancyModule
    {
        public NancyDemoModule()
        {
            Get["/nancy"] = x =>
            {
                var env = Context.GetOwinEnvironment();
                return "Hello from Nancy! You requested: " + env["owin.RequestPath"];
            };
        }
    }    
    ```
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

            app.UseNancy();

            app.Use(async (ctx, next) => {
                await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
            });
        }
    }    
    ```
* Configuring NancyFx for passthrough  
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
    ```
