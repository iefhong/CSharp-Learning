## Branching
```
if (age <= 2>)
    ServeMilk();
else if (age < 21)
    ServeSoda();    
else
{
    ServeDrink();
}    

if (age <= 2>)
{
    if(name == "Scott")
    {
        // ...
    }
}

string pass = age > 20 ? "pass" : "nopass";
```

## Switching
* Restricted to integers, characters, strings, and enum
    * Case labels are constants
    * Default label is optional
    ```
    switch(name){
        case "Scott":
            ServeSoda();
            break;
        case "Alex":
            ServeMilk();
            ServeDrink();
            break;
        default:
            ServeMilk();
            break;        
    }
    ```  
## Iterating
    ```
    for(int i = 0; i < age; i++)
    {
        Console.WriteLine(i);
    }

    do
    {
        age++;
        Console.WriteLine(age);
    }while( age < 100);

    while(age > 0)
    {
        age -= 1;
        Console.WriteLine(age);
    }

    int[] ages = {2, 21, 40, 72, 100};
    foreach (int value in ages)
    {
        Console.WriteLine(value);
    }
    ```      
