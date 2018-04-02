* What is OWIN?
    - Open Web Interface for .NET  
    OWIN defines a standard interface between .NET web servers and web applications.
    - It is just a specification
* Example
    ``` The application delegate or AppFunc
    using AppFunc = Func<IDictionary<string, object>, Task>;
    var f = new AppFunc(environment => {
        return Task.FromResult(null);
    });
    ```
* The Parts
    - Host  
    - Server
    - Middleware
    - Application
    ![OWIN Flowchart](https://lh3.googleusercontent.com/-11yHfPEFNMw/WrSz8P3U02I/AAAAAAAAADQ/gnHrPEHn0mcye3VQB17W0xFXfPf51IOXgCL0BGAs/w795-d-h411-n-rw/%25E6%25B5%2581%25E7%25A8%258B%25E6%25BC%2594%25E7%25A4%25BA.png)
* Environment Dictionary Keys  
    - owin.RequestPath(string)    
    - owin.RequestQueryString (string)
    - owin.RequestHeaders (IDictionary<string, string[]>)
    - owin.RequestBody (Stream)
    - owin.ResponseHeaders(IDictionary<string, string[]>)
    - owin.ResponseBody (Stream)
    - owin.ResponseStatusCode (Int32)
    - owin.Version (string)
    - [server.OnSendingHeaders (Action<Action<object>, object>)]  
    ...
* Project Katana    