using DataStructures.Multithreading;
using System.Collections.Concurrent;

namespace DataStructures.Tests;

public class ThreadSafeQueueTests
{
    [Fact]
    public void Enqueue_ShouldAddElementsCorrectly()
    {
        // Arrange
        var queue = new ThreadSafeQueue<int>();
        
        // Act
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        
        // Assert
        Assert.Equal(3, queue.Count);
        var array = queue.ToArray();
        Assert.Equal(new[] { 1, 2, 3 }, array);
    }

    [Fact]
    public void TryDequeue_WithElements_ShouldReturnTrueAndElement()
    {
        // Arrange
        var queue = new ThreadSafeQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        
        // Act
        var result1 = queue.TryDequeue(out var item1);
        var result2 = queue.TryDequeue(out var item2);
        
        // Assert
        Assert.True(result1);
        Assert.Equal(1, item1);
        Assert.True(result2);
        Assert.Equal(2, item2);
        Assert.Equal(0, queue.Count);
    }

    [Fact]
    public void TryDequeue_EmptyQueue_ShouldReturnFalse()
    {
        // Arrange
        var queue = new ThreadSafeQueue<int>();
        
        // Act
        var result = queue.TryDequeue(out var item);
        
        // Assert
        Assert.False(result);
        Assert.Equal(default(int), item);
    }

    [Fact]
    public void TryPeek_WithElements_ShouldReturnTrueAndElementWithoutRemoving()
    {
        // Arrange
        var queue = new ThreadSafeQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        
        // Act
        var result = queue.TryPeek(out var item);
        
        // Assert
        Assert.True(result);
        Assert.Equal(1, item);
        Assert.Equal(2, queue.Count); // Should not remove the element
    }

    [Fact]
    public void TryPeek_EmptyQueue_ShouldReturnFalse()
    {
        // Arrange
        var queue = new ThreadSafeQueue<int>();
        
        // Act
        var result = queue.TryPeek(out var item);
        
        // Assert
        Assert.False(result);
        Assert.Equal(default(int), item);
    }

    [Fact]
    public void Clear_ShouldRemoveAllElements()
    {
        // Arrange
        var queue = new ThreadSafeQueue<int>();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        
        // Act
        queue.Clear();
        
        // Assert
        Assert.Equal(0, queue.Count);
        Assert.Empty(queue.ToArray());
    }

    [Fact]
    public async Task ConcurrentOperations_ShouldMaintainThreadSafety()
    {
        // Arrange
        var queue = new ThreadSafeQueue<int>();
        const int itemsPerTask = 100;
        const int numberOfTasks = 4;
        var tasks = new List<Task>();
        var producedItems = new ConcurrentBag<int>();
        var consumedItems = new ConcurrentBag<int>();

        // Act - Start producer tasks
        for (int taskId = 0; taskId < numberOfTasks; taskId++)
        {
            int localTaskId = taskId;
            tasks.Add(Task.Run(() =>
            {
                for (int i = 0; i < itemsPerTask; i++)
                {
                    var item = localTaskId * itemsPerTask + i;
                    queue.Enqueue(item);
                    producedItems.Add(item);
                }
            }));
        }

        // Start consumer tasks
        for (int taskId = 0; taskId < numberOfTasks; taskId++)
        {
            tasks.Add(Task.Run(async () =>
            {
                var itemsConsumed = 0;
                while (itemsConsumed < itemsPerTask)
                {
                    if (queue.TryDequeue(out var item))
                    {
                        consumedItems.Add(item);
                        itemsConsumed++;
                    }
                    else
                    {
                        await Task.Delay(1); // Small delay to allow producers to add items
                    }
                }
            }));
        }

        await Task.WhenAll(tasks);

        // Assert
        Assert.Equal(numberOfTasks * itemsPerTask, producedItems.Count);
        Assert.Equal(numberOfTasks * itemsPerTask, consumedItems.Count);
        Assert.Equal(0, queue.Count); // All items should be consumed
        
        // Verify all produced items were consumed
        var producedSet = new HashSet<int>(producedItems);
        var consumedSet = new HashSet<int>(consumedItems);
        Assert.Equal(producedSet, consumedSet);
    }

    [Fact]
    public void WaitAndDequeue_WithTimeout_ShouldTimeoutCorrectly()
    {
        // Arrange
        var queue = new ThreadSafeQueue<int>();
        var startTime = DateTime.UtcNow;
        
        // Act
        var result = queue.WaitAndDequeue(100, out var item); // 100ms timeout
        var endTime = DateTime.UtcNow;
        
        // Assert
        Assert.False(result);
        Assert.Equal(default(int), item);
        var elapsed = endTime - startTime;
        Assert.True(elapsed.TotalMilliseconds >= 95, $"Expected at least 95ms, but was {elapsed.TotalMilliseconds}ms");
        Assert.True(elapsed.TotalMilliseconds < 200, $"Expected less than 200ms, but was {elapsed.TotalMilliseconds}ms");
    }

    [Fact]
    public async Task WaitAndDequeue_WithItemAdded_ShouldReturnImmediately()
    {
        // Arrange
        var queue = new ThreadSafeQueue<int>();
        
        // Act - Start waiting task
        var waitTask = Task.Run(() => queue.WaitAndDequeue(1000, out var item));
        
        // Add item after a short delay
        await Task.Delay(50);
        queue.Enqueue(42);
        
        var result = await waitTask;
        
        // Assert
        Assert.True(result);
    }
}