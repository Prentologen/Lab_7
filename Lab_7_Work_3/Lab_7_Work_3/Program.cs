using System;
using System.Collections.Generic;

public class FunctionCache<TKey, TResult>
{
    private readonly Dictionary<TKey, CachedItem<TResult>> cache = new Dictionary<TKey, CachedItem<TResult>>();
    private TimeSpan cacheDuration;

    public delegate TResult Func<TKey, TResult>(TKey key);

    public FunctionCache(TimeSpan cacheDuration)
    {
        this.cacheDuration = cacheDuration;
    }

    public TResult GetOrAdd(TKey key, Func<TKey, TResult> func)
    {
        if (cache.TryGetValue(key, out CachedItem<TResult> cachedItem) && !cachedItem.IsExpired(cacheDuration))
        {
            return cachedItem.Value;
        }

        TResult result = func(key);
        cache[key] = new CachedItem<TResult>(result, DateTime.Now);
        return result;
    }

    private class CachedItem<T>
    {
        public T Value { get; }
        public DateTime CachedTime { get; }

        public CachedItem(T value, DateTime cachedTime)
        {
            Value = value;
            CachedTime = cachedTime;
        }

        public bool IsExpired(TimeSpan duration)
        {
            return DateTime.Now - CachedTime > duration;
        }
    }

    internal int GetOrAdd(string key1, System.Func<string, int> expensiveFunction)
    {
        throw new NotImplementedException();
    }
}


public class Program
{
    public static void Main()
    {
        FunctionCache<string, int> cache = new FunctionCache<string, int>(TimeSpan.FromMinutes(1));

        Func<string, int> expensiveFunction = key =>
        {
            Console.WriteLine($"Calculating result for key: {key}");
            return key.Length;
        };

        string key1 = "hello";
        string key2 = "world";

        int v = cache.GetOrAdd(key1, expensiveFunction);
        int result1 = v;
        int result2 = cache.GetOrAdd(key1, expensiveFunction); // Отримаємо результат з кешу

        int result3 = cache.GetOrAdd(key2, expensiveFunction);

        Console.WriteLine($"Result 1: {result1}");
        Console.WriteLine($"Result 2: {result2}");
        Console.WriteLine($"Result 3: {result3}");
    }
}
