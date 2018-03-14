## .NET
* .NET is a software framework
## Common Language Runtime (CLR)
* The CLR manages your application 
    * Memory management  
    * Operating system and hardware independece  
    * Language independence
## FCL
* Framework class library  
    * A library of functionality to build applications  
## C#
* One of many languages for .NET
* Systax is similar to Java, C++, and JavaScript  
    ``` Example code 
    public static void Main()
    {
        if(DateTime.Now.DayOfWeek == DayofWeek.Monday)
        {
            Console.WriteLine("Another case of the Monday!");
        }
    }
    ```
## csc.exe
* The C# command line compiler  
    * Transforms C# code into Microsoft Intermediate Language
## Visual Studio    
* An integrated development environment   
    * Edit C# (and other) files
    * Runs the C# compiler
    * Debugging
    * Testing
## Solution Explorer    
* Will contain at least one project 
    * Contains one or more source code files
    * Each project produces an assembly
* Projects organized under a solution
    * Manage multiple applications or libraries

## Types      
* C# is Strongly typed
    * One way to define a type is to write a class
    * Every object you work with has a specific type
    * 1,000 of types are built into the .NET framework
    * you can define your own custom types
* Code you want to execute must live inside a type
    * You can place the code inside a mehton
    * We'll explore other things you can add to a type later...

## Summary
* C# is a strongly typed & case sensitive language for .NET
* Visual Studio is an IDE to work with C# applications of all types    
    ```
    static void Main(string[] args)
    {
        Console.WriteLine("Your name:");
        string name = Console.ReadLine();

        Console.WriteLine("How many hours of sleep did you get last night?");
        int hoursOfSleep = int.Parse(Console.ReadLine());

        Console.WriteLine("Hello, "+ name);
        if(hoursOfSleep > 8)
        {
            Console.WriteLine("You are well rested");
        }
        else
        {
            Console.WriteLine("You need more sleep");
        }
    }
    ```