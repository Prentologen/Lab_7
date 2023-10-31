using System;
using System.Collections.Generic;

public class TaskScheduler<TTask, TPriority>
{
    private Queue<(TTask task, TPriority priority)> taskQueue = new Queue<(TTask, TPriority)>();
    private Func<TTask, TPriority> getPriority;
    private Func<TTask> initializeTask;
    private Action<TTask> resetTask;
    private Action<TTask> executeTask;

    public TaskScheduler(
        Func<TTask, TPriority> getPriority,
        Func<TTask> initializeTask,
        Action<TTask> resetTask,
        Action<TTask> executeTask)
    {
        this.getPriority = getPriority;
        this.initializeTask = initializeTask;
        this.resetTask = resetTask;
        this.executeTask = executeTask;
    }

    public void AddTask(TTask task)
    {
        TPriority priority = getPriority(task);
        taskQueue.Enqueue((task, priority));
    }

    public void ExecuteNext()
    {
        if (taskQueue.Count == 0)
        {
            Console.WriteLine("No tasks in the queue.");
            return;
        }

        (TTask task, TPriority priority) = taskQueue.Dequeue();


        TTask task1 = initializeTask(task);
        TTask initializedTask = task1;
        executeTask(initializedTask);


        resetTask?.Invoke(initializedTask);

        Console.WriteLine($"Task executed with priority: {priority}");
    }

    public int TaskCount => taskQueue.Count;
}


public class Program
{
    public static void Main()
    {
        TaskScheduler<string, int> scheduler = new TaskScheduler<string, int>(
            getPriority: task => int.Parse(task), 
            initializeTask: task => task, 
            resetTask: null,
            executeTask: ExecuteTask 
        );

        while (true)
        {
            Console.Write("Enter a task priority (or 'q' to quit): ");
            string input = Console.ReadLine();
            if (input.ToLower() == "q")
                break;

            scheduler.AddTask(input);
        }

        while (scheduler.TaskCount > 0)
        {
            scheduler.ExecuteNext();
        }
    }

    private static void ExecuteTask(string task)
    {
        Console.WriteLine($"Executing task: {task}");
    }
}
