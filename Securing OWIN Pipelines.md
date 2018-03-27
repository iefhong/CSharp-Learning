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
    ``` LoginModel
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }    
    ```
    ``` AuthController.cs
    public class AuthController : Controller
    {

        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (model.Username.Equals("iefhong", StringComparison.OrdinalIgnoreCase) 
                && model.Password == "password")
            {
                var identity = new ClaimsIdentity("ApplicationCookie");
                identity.AddClaims(new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, model.Username),
                    new Claim(ClaimTypes.Name, model.Username)
                });
                HttpContext.GetOwinContext().Authentication.SignIn(identity);
            }

            return View(model);
        }
    }    
    ```
    ``` Login.cshtml
    @inherits System.Web.Mvc.WebViewPage<OwinDemo.Models.LoginModel>
    @using System.Web.Mvc.Html

    <!DOCTYPE html>

    <html>
    <head>
        <meta name="viewport" content="width=device-width" />
        <title>Login</title>
    </head>
    <body>
        <div>
            @using (var form = Html.BeginForm())
            {
            <div>
                @Html.LabelFor(x =>x.Username)
                @Html.TextBoxFor(x=>x.Username)
            </div>
            <div>
                @Html.LabelFor(x=>x.Password)
                @Html.PasswordFor(x=>x.Password)
            </div>
            <div>
                <input type="submit" value="Log in" />
            </div>
            }
        </div>
    </body>
    </html>
    ```
* Log out user
    * IAuthenticationManager.SignOut()    