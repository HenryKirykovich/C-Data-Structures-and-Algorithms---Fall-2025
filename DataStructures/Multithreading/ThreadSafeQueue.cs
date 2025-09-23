using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DataStructures.Multithreading;

/// <summary>
/// A thread-safe queue implementation using locks
/// </summary>
/// <typeparam name="T">The type of elements in the queue</typeparam>
public class ThreadSafeQueue<T>
{
    private readonly Queue<T> queue = new();
    private readonly object lockObject = new();

    /// <summary>
    /// Gets the number of elements in the queue
    /// </summary>
    public int Count
    {
        get
        {
            lock (lockObject)
            {
                return queue.Count;
            }
        }
    }

    /// <summary>
    /// Adds an element to the end of the queue
    /// </summary>
    /// <param name="item">The item to add</param>
    public void Enqueue(T item)
    {
        lock (lockObject)
        {
            queue.Enqueue(item);
            Monitor.PulseAll(lockObject); // Notify waiting threads
        }
    }

    /// <summary>
    /// Removes and returns the element at the beginning of the queue
    /// </summary>
    /// <param name="item">The dequeued item</param>
    /// <returns>True if an item was dequeued, false if the queue was empty</returns>
    public bool TryDequeue(out T? item)
    {
        lock (lockObject)
        {
            if (queue.Count > 0)
            {
                item = queue.Dequeue();
                return true;
            }
            
            item = default;
            return false;
        }
    }

    /// <summary>
    /// Waits for and removes an element from the beginning of the queue
    /// </summary>
    /// <param name="timeout">Maximum time to wait in milliseconds (-1 for infinite)</param>
    /// <param name="item">The dequeued item</param>
    /// <returns>True if an item was dequeued, false if timeout occurred</returns>
    public bool WaitAndDequeue(int timeout, out T? item)
    {
        lock (lockObject)
        {
            var endTime = timeout >= 0 ? DateTime.UtcNow.AddMilliseconds(timeout) : DateTime.MaxValue;
            
            while (queue.Count == 0 && DateTime.UtcNow < endTime)
            {
                var remainingTime = timeout >= 0 ? (int)(endTime - DateTime.UtcNow).TotalMilliseconds : -1;
                if (remainingTime <= 0 || !Monitor.Wait(lockObject, remainingTime))
                {
                    item = default;
                    return false;
                }
            }

            if (queue.Count > 0)
            {
                item = queue.Dequeue();
                return true;
            }

            item = default;
            return false;
        }
    }

    /// <summary>
    /// Returns the element at the beginning of the queue without removing it
    /// </summary>
    /// <param name="item">The item at the front of the queue</param>
    /// <returns>True if an item was peeked, false if the queue was empty</returns>
    public bool TryPeek(out T? item)
    {
        lock (lockObject)
        {
            if (queue.Count > 0)
            {
                item = queue.Peek();
                return true;
            }
            
            item = default;
            return false;
        }
    }

    /// <summary>
    /// Removes all elements from the queue
    /// </summary>
    public void Clear()
    {
        lock (lockObject)
        {
            queue.Clear();
        }
    }

    /// <summary>
    /// Gets all elements in the queue as an array (snapshot)
    /// </summary>
    /// <returns>An array containing all elements in the queue</returns>
    public T[] ToArray()
    {
        lock (lockObject)
        {
            return queue.ToArray();
        }
    }
}

/// <summary>
/// Producer-Consumer pattern example using ThreadSafeQueue
/// </summary>
public class ProducerConsumerExample
{
    private readonly ThreadSafeQueue<int> queue = new();
    private volatile bool isRunning = true;

    /// <summary>
    /// Demonstrates producer-consumer pattern
    /// </summary>
    /// <param name="producerCount">Number of producer threads</param>
    /// <param name="consumerCount">Number of consumer threads</param>
    /// <param name="itemsPerProducer">Number of items each producer should produce</param>
    /// <param name="cancellationToken">Cancellation token to stop the operation</param>
    /// <returns>Task representing the async operation</returns>
    public async Task RunExample(int producerCount = 2, int consumerCount = 2, int itemsPerProducer = 10, CancellationToken cancellationToken = default)
    {
        isRunning = true;
        var tasks = new List<Task>();

        // Start producer tasks
        for (int i = 0; i < producerCount; i++)
        {
            int producerId = i;
            tasks.Add(Task.Run(() => Producer(producerId, itemsPerProducer, cancellationToken), cancellationToken));
        }

        // Start consumer tasks
        for (int i = 0; i < consumerCount; i++)
        {
            int consumerId = i;
            tasks.Add(Task.Run(() => Consumer(consumerId, cancellationToken), cancellationToken));
        }

        // Wait for all producers to finish
        await Task.WhenAll(tasks.Take(producerCount));
        
        // Allow some time for consumers to process remaining items
        await Task.Delay(1000, cancellationToken);
        
        isRunning = false;

        // Wait for all consumers to finish
        await Task.WhenAll(tasks.Skip(producerCount));
    }

    private void Producer(int producerId, int itemCount, CancellationToken cancellationToken)
    {
        for (int i = 0; i < itemCount && !cancellationToken.IsCancellationRequested; i++)
        {
            var item = producerId * 1000 + i;
            queue.Enqueue(item);
            Console.WriteLine($"Producer {producerId} produced: {item}");
            
            // Simulate some work
            Thread.Sleep(100);
        }
        
        Console.WriteLine($"Producer {producerId} finished");
    }

    private void Consumer(int consumerId, CancellationToken cancellationToken)
    {
        while (isRunning && !cancellationToken.IsCancellationRequested)
        {
            if (queue.WaitAndDequeue(500, out var item))
            {
                Console.WriteLine($"Consumer {consumerId} consumed: {item}");
                
                // Simulate some work
                Thread.Sleep(150);
            }
        }
        
        // Process any remaining items
        while (queue.TryDequeue(out var item))
        {
            Console.WriteLine($"Consumer {consumerId} consumed final: {item}");
        }
        
        Console.WriteLine($"Consumer {consumerId} finished");
    }
}