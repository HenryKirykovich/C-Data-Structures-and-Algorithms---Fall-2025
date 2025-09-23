using System;
using System.Threading;
using System.Threading.Tasks;
using DataStructures;
using DataStructures.LinkedLists;
using DataStructures.Trees;
using DataStructures.Multithreading;

namespace Examples;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== C# Data Structures and Algorithms - Fall 2025 ===");
        Console.WriteLine(DataStructuresInfo.GetInfo());
        Console.WriteLine();

        // Demonstrate Linked List
        await DemonstrateLinkedList();
        
        // Demonstrate Binary Search Tree
        await DemonstrateBinarySearchTree();
        
        // Demonstrate Multithreading
        await DemonstrateMultithreading();
        
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }

    static async Task DemonstrateLinkedList()
    {
        Console.WriteLine("=== Singly Linked List Demonstration ===");
        
        var list = new SinglyLinkedList<int>();
        
        // Add elements
        Console.WriteLine("Adding elements: 1, 2, 3 to the end");
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        
        Console.WriteLine("Adding element 0 to the beginning");
        list.AddFirst(0);
        
        Console.WriteLine($"List count: {list.Count}");
        Console.WriteLine($"List contents: [{string.Join(", ", list.ToArray())}]");
        
        // Search for elements
        Console.WriteLine($"Contains 2: {list.Contains(2)}");
        Console.WriteLine($"Contains 5: {list.Contains(5)}");
        
        // Remove element
        Console.WriteLine("Removing element 2");
        list.Remove(2);
        Console.WriteLine($"List contents after removal: [{string.Join(", ", list.ToArray())}]");
        Console.WriteLine($"List count: {list.Count}");
        
        Console.WriteLine();
        await Task.Delay(500); // Small delay for better readability
    }

    static async Task DemonstrateBinarySearchTree()
    {
        Console.WriteLine("=== Binary Search Tree Demonstration ===");
        
        var bst = new BinarySearchTree<int>();
        
        // Insert elements
        Console.WriteLine("Inserting elements: 50, 30, 70, 20, 40, 60, 80");
        var values = new[] { 50, 30, 70, 20, 40, 60, 80 };
        
        foreach (var value in values)
        {
            bst.Insert(value);
        }
        
        Console.WriteLine($"Tree count: {bst.Count}");
        
        // Demonstrate traversals
        Console.WriteLine($"In-order traversal: [{string.Join(", ", bst.InOrderTraversal())}]");
        Console.WriteLine($"Pre-order traversal: [{string.Join(", ", bst.PreOrderTraversal())}]");
        Console.WriteLine($"Post-order traversal: [{string.Join(", ", bst.PostOrderTraversal())}]");
        
        // Search for elements
        Console.WriteLine($"Contains 40: {bst.Contains(40)}");
        Console.WriteLine($"Contains 25: {bst.Contains(25)}");
        
        // Remove element
        Console.WriteLine("Removing element 30");
        bst.Remove(30);
        Console.WriteLine($"In-order traversal after removal: [{string.Join(", ", bst.InOrderTraversal())}]");
        Console.WriteLine($"Tree count: {bst.Count}");
        
        Console.WriteLine();
        await Task.Delay(500); // Small delay for better readability
    }

    static async Task DemonstrateMultithreading()
    {
        Console.WriteLine("=== Thread-Safe Queue and Producer-Consumer Demonstration ===");
        
        // Basic thread-safe queue operations
        var queue = new ThreadSafeQueue<string>();
        
        Console.WriteLine("Adding items to thread-safe queue...");
        queue.Enqueue("Item 1");
        queue.Enqueue("Item 2");
        queue.Enqueue("Item 3");
        
        Console.WriteLine($"Queue count: {queue.Count}");
        Console.WriteLine($"Queue contents: [{string.Join(", ", queue.ToArray())}]");
        
        // Dequeue items
        while (queue.TryDequeue(out var item))
        {
            Console.WriteLine($"Dequeued: {item}");
        }
        
        Console.WriteLine($"Queue count after dequeuing: {queue.Count}");
        Console.WriteLine();
        
        // Producer-Consumer pattern demonstration
        Console.WriteLine("Starting Producer-Consumer example...");
        Console.WriteLine("(This will run for a few seconds)");
        
        var example = new ProducerConsumerExample();
        
        using var cts = new CancellationTokenSource();
        cts.CancelAfter(TimeSpan.FromSeconds(5)); // Run for 5 seconds maximum
        
        try
        {
            await example.RunExample(
                producerCount: 2, 
                consumerCount: 2, 
                itemsPerProducer: 5, 
                cts.Token);
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine("Producer-Consumer example was cancelled due to timeout.");
        }
        
        Console.WriteLine("Producer-Consumer example completed.");
        Console.WriteLine();
    }
}
