* Building a Simple OWIN Pipeline  
    - NuGet the required host  
    Microsoft.Owin.Host.SystemWeb
    - Add public class Startup
    - Add Configuration(IAppBuilder app) method  
        ```
        public static void Configuration(IAppBuilder app)
        {
            app.Use(async (ctx, next) => {
                await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
            });
        }
        ```
    - Add middleware
        ``` UseDebugMiddleware
        public static void Configuration(IAppBuilder app)
        {
            app.Use(async (ctx, next) => {
                Debug.WriteLine("Incoming request: " + ctx.Request.Path);
                await next();
                Debug.WriteLine("Incoming request: " + ctx.Request.Path);
            });             
            app.Use(async (ctx, next) => {
                await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
            });
        }
        ```