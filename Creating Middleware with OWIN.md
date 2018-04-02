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
        ``` Startup.cs
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
        ``` DebugMiddlewareOptions.cs
        public class DebugMiddlewareOptions
        {
            public Action<IOwinContext> OnIncomingRequest { get; set; }
            public Action<IOwinContext> OnOutgoingRequest { get; set; }
        }
        ```
        ``` DebugMiddleware.cs
        public class DebugMiddleware
        {
            AppFunc _next;
            DebugMiddlewareOptions _options;
            public DebugMiddleware(AppFunc next, DebugMiddlewareOptions options )
            {
                _next = next;
                _options = options;

                if (_options.OnIncomingRequest != null)
                {
                    _options.OnIncomingRequest = (ctx) => { Debug.WriteLine("Incoming request: " + ctx.Request.Path); };
                }

                if (_options.OnOutgoingRequest != null)
                {
                    _options.OnOutgoingRequest = (ctx) => { Debug.WriteLine("Outgoing request: " + ctx.Request.Path); };
                }
            }

            public async Task Invoke(IDictionary<string, object> environment)
            {
                var ctx = new OwinContext(environment);
                _options.OnIncomingRequest(ctx);
                await _next(environment);
                _options.OnOutgoingRequest(ctx);

            }
        }        
        ```
        ``` Startup.cs
        public class Startup
        {
            public static void Configuration(IAppBuilder app)
            {
                app.Use<DebugMiddleware>(new DebugMiddlewareOptions());

                app.Use(async (ctx, next) => {
                    await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
                });
            }
        }        
        ```
        ``` Startup.cs 
        public class Startup
        {
            public static void Configuration(IAppBuilder app)
            {
                app.Use<DebugMiddleware>(new DebugMiddlewareOptions
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

                app.Use(async (ctx, next) => {
                    await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
                });
            }
        }        
        ```
    - xxxMiddlewareExtensions  
    ``` DebugMiddlewareExtensions.cs
    public static class DebugMiddlewareExtensions
    {
        public static void UseDebugMiddleware(this IAppBuilder app, DebugMiddlewareOptions options = null)
        {
            if (options == null)
            {
                options = new DebugMiddlewareOptions();
            }

            app.Use<DebugMiddleware>(options);
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

            //app.Use<DebugMiddleware>();

            app.Use(async (ctx, next) => {
                await ctx.Response.WriteAsync("<html><head><body>Hello world</body></head><html/>");
            });
        }
    }    
    ```
