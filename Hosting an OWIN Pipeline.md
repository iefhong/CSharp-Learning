* Self-Hosting an OWIN Pipeline  
    - Install self-hosting NuGet Package  
        - Microsoft.Owin.SelfHost  
    - Start the server  
        - WebApp.Start<T>()
* Testing an OWIN Pipeline  
    - Install test-server NuGet package  
        - Microsoft.Owin.Testing  
    - Create test method  
    - Start the server  
        - TestServer.Create<T>()
    - Do the test, and assert result  
    - Optional: Keep it DRY

* Hosting Static file
    install-package microsoft.owin.staticfiles
    app.UseStaticFiles();

* Testing
    create Unit Test Project  
    add Reference project
    install Package
    add Test class 
    ```
    
    ```                                       