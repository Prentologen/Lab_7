using System;

public class Calculator<T>
{
    public T Add(T a, T b)
    {
        dynamic da = a;
        dynamic db = b;
        return da + db;
    }

    public T Subtract(T a, T b)
    {
        dynamic da = a;
        dynamic db = b;
        return da - db;
    }

    public T Multiply(T a, T b)
    {
        dynamic da = a;
        dynamic db = b;
        return da * db;
    }

    public T Divide(T a, T b)
    {
        dynamic da = a;
        dynamic db = b;
        if (db == 0)
        {
            throw new DivideByZeroException("Division by zero is not allowed.");
        }
        return da / db;
    }
}

public class Program
{
    public static void Main()
    {
        Calculator<int> intCalculator = new Calculator<int>();
        Console.WriteLine("Integer Calculator:");
        Console.WriteLine("Add: " + intCalculator.Add(5, 3));
        Console.WriteLine("Subtract: " + intCalculator.Subtract(5, 3));
        Console.WriteLine("Multiply: " + intCalculator.Multiply(5, 3));
        Console.WriteLine("Divide: " + intCalculator.Divide(10, 2));

        Calculator<double> doubleCalculator = new Calculator<double>();
        Console.WriteLine("\nDouble Calculator:");
        Console.WriteLine("Add: " + doubleCalculator.Add(5.5, 3.2));
        Console.WriteLine("Subtract: " + doubleCalculator.Subtract(5.5, 3.2));
        Console.WriteLine("Multiply: " + doubleCalculator.Multiply(5.5, 3.2));
        Console.WriteLine("Divide: " + doubleCalculator.Divide(10.0, 2.0));
    }
}
