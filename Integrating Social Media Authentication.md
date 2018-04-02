* Integrating Social Media Authentication  
    - Configure authentication provider  
    - Get NuGet package for selected provider  
    - Add middleware to pipeline  
    - Configure middleware with ID and secret  
    - Create login link  
    - Create challenge  
        - IOwinContext.Authentication.Challenge()  
    - Return HTTP 401 Unauthorized  

* Configure host
    ``` C:\Windows\System32\drivers\etc\hosts
    127.0.0.1       www.owin.demo
    ```

* Create IIS Web Site
    www.owin.demo  
* Configure csproj 
    local iis 

* Facebook OAuth  
    [facebook](https://developers.facebook.com/)   
    Create facebook Login product
    install package microsoft.owin.security.facebook      
    ``` Startup.cs
    app.UseFacebookAuthentication(new Microsoft.Owin.Security.Facebook.FacebookAuthenticationOptions {
        AppId= "226126538131701",
        AppSecret = "",
        SignInAsAuthenticationType = "ApplicationCookie"
    });    
    ```   
    ``` AuthController.cs
    public ActionResult LoginFacebook()
    {
        HttpContext.GetOwinContext().Authentication.Challenge(new Microsoft.Owin.Security.AuthenticationProperties() {
            RedirectUri = "/secret"
        }, "Facebook");

        return new HttpUnauthorizedResult();
    }    
    ```
    ``` Views/Auth/Login.cshtml
    <div>@Html.ActionLink("Log in with Facebook", "LoginFacebook")</div>
    ```

* Twitter Oauth  
    [Twitter](https://apps.twitter.com/)  
    Create twitter Login product
    install package microsoft.owin.security.twitter  
    