## Constructors
* Special methods used to initialize objects
    ```
    public GradeBook()
    {
            // ... initialization code
    }
    GradeBook book = new GradeBook();
    ```
## Classes Versus Variables
* A class is a blueprint for creating objects
* A class can also be used to type a variable
    * A variable can refer to any object of the same type
    ``` New GradeBook Object
    class GradeBook
    {
        public GradeBook()
        {
            grades = new List<float>();
        }

        public void AddGrade(float grade)
        {
            grade.Add(grade);
        }

        List<float> grades;
    }
    ```    
## Reference Types
* Classes are reference types
* Variables hold a pointer value
    ``` Reference Types used the same GradeBook Object
    GradeBook book1 = new Grade Book();
    GradeBook book2 = book1;
    ```
## Statics
* Use static members of a class without creating an instance
    ```
    public static float MinimumGrade = 0;
    public static float MaximumGrade = 100;

    Console.WriteLine("Hello");
    Console.WriteLine(GradeBook.MaximumGrade);
    ```
## Classes    
* A class definition create a new type
    * Use the type for variables and arguments
* Use a class to create objects
    * Invoke methods and save state in objects
## Object Oriented Programming
* Objects are nouns
* Methods are verbs        
* Objects encapsulate functionality  
## Encapsulation
## Access Modifiers
* public 
    * Constructor
    * AddGrade
* private
    * grades    
    ```
        class GradeBook
        {
            public GradeBook()
            {
                grades = new List<float>();
            }

            public void AddGrade(float grade)
            {
                grade.Add(grade);
            }

            List<float> grades;
        }
    ```
## Summary
```
class GradeBook
{
    public GradeBook()
    {
        grades = new List(float)();
    }
    
    public GradeStatistics ComputeStatistics()
    {
        GradeStatistics stats = new GradeStatistics();

        float sum = 0;
        foreach(float grade in grades)
        {
            stats.HighestGrade = Math.Max(grade, stats.HighestGrade);
            stats.LowerestGrade = Math.Min(grade, stats.LowestGrade);
            sum += grade;
        }
        stats.AverageGrade = sum / grades.Count;
        return stats;
    }

    public void AddGrade(float grade)
    {
        grades.add(grade);
    }

    private List<float> grades;
}
```