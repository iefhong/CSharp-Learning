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
# Web Api
* Install
    install-package Microsoft.AspNet.WebApi.Owin    
* integrating Web Api   
    ``` HelloWorldController.cs
    [RoutePrefix("api")]
    public class HelloWorldController : ApiController
    {
        [Route("hello")]
        [HttpGet]
        public IHttpActionResult HelloWorld()
        {
            return Content(System.Net.HttpStatusCode.OK, "Hello from web api");
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

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);

            app.UseNancy(conf =>
            {
                conf.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            });

            app.Use(async (ctx, next) => {
                await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
            });
        }
    }    
    ```
# ASP.NET MVC
* Install
    install-package microsoft.aspnet.mvc    
* Configuration MVC
    ``` HomeController.cs
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
    }    
    ```    
    ``` Index.cshtml
    @inherits System.Web.Mvc.WebViewPage
    @{
        Layout = null;
    }

    <!DOCTYPE html>

    <html>
    <head>
        <meta name="viewport" content="width=device-width" />
        <title>Index</title>
    </head>
    <body>
        <div> 
            Hello from MVC
        </div>
    </body>
    </html>
    ```
    ``` Global.asax
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteTable.Routes.MapRoute(name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Home", action = "Index" });
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

            var config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            app.UseWebApi(config);

            app.UseNancy(conf =>
            {
                conf.PassThroughWhenStatusCodesAre(HttpStatusCode.NotFound);
            });

            //app.Use(async (ctx, next) => {
            //    await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
            //});
        }
    }    
    ```