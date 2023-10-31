using System;
using System.Collections.Generic;

public class Repository<T>
{
    private List<T> data = new List<T>();

    public delegate bool Criteria<T>(T item);

    public void Add(T item)
    {
        data.Add(item);
    }

    public IEnumerable<T> Find(Criteria<T> criteria)
    {
        List<T> results = new List<T>();
        foreach (T item in data)
        {
            if (criteria(item))
            {
                results.Add(item);
            }
        }
        return results;
    }
}


public class Program
{
    public static void Main()
    {
        Repository<int> intRepository = new Repository<int>();
        intRepository.Add(1);
        intRepository.Add(2);
        intRepository.Add(3);
        intRepository.Add(4);
        intRepository.Add(5);
        Repository<int>.Criteria<int> isEven = x => x % 2 == 0;

        IEnumerable<int> evenNumbers = intRepository.Find(isEven);

        Console.WriteLine("Even Numbers:");
        foreach (int number in evenNumbers)
        {
            Console.WriteLine(number);
        }

        Repository<string> stringRepository = new Repository<string>();
        stringRepository.Add("pineapple");
        stringRepository.Add("cocos");
        stringRepository.Add("pear");
        stringRepository.Add("date");

        Repository<string>.Criteria<string> startsWithA = s => s.StartsWith("a", StringComparison.OrdinalIgnoreCase);

        IEnumerable<string> aFruits = stringRepository.Find(startsWithA);

        Console.WriteLine("\nFruits that start with 'A':");
        foreach (string fruit in aFruits)
        {
            Console.WriteLine(fruit);
        }
    }
}
