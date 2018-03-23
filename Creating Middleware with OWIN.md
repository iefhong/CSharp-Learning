* Creating Middleware with OWIN  
    - xxxMiddleware Class
        ``` DebugMiddleware
        using AppFunc = System.Func<
            System.Collections.Generic.IDictionary<string, object>,
            System.Threading.Tasks.Task
        >;

        public class DebugMiddleware
        {
            AppFunc _next;
            public DebugMiddleware(AppFunc next)
            {
                _next = next;
            }

            public async Task Invoke(IDictionary<string, object> environment)
            {
                var ctx = new OwinContext(environment);
                Debug.WriteLine("Incoming request: " + ctx.Request.Path);
                await _next(environment);
                Debug.WriteLine("Incoming request: " + ctx.Request.Path);
            }
        }
        ```  
        ``` Startup
        public class Startup
        {
            public static void Configuration(IAppBuilder app)
            {
                //app.Use(async (ctx, next) => {
                //    Debug.WriteLine("Incoming request: " + ctx.Request.Path);
                //    await next();
                //    Debug.WriteLine("Incoming request: " + ctx.Request.Path);
                //});
                app.Use<DebugMiddleware>();
                app.Use(async (ctx, next) => {
                    await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
                });
            }
        }
        ```
    - xxxMiddlewareOptions Class  
    - xxxMiddlewareExtensions  