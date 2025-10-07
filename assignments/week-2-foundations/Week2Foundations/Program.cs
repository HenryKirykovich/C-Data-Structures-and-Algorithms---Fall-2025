using System;
using System.Collections.Generic;

// Micro-tasks A-F for week-2 foundations
Console.WriteLine("Week2 Foundations - Micro Tasks\n");

//DemoArray();
//DemoList();
//DemoStack();
//DemoQueue();
//DemoDictionary();
//DemoHashSet();


// Run the benchmark
Benchmarks.Run();



// A. Array
static void DemoArray()
{
    int[] arr = new int[10];
    arr[0] = 7;
    arr[2] = 42;
    arr[5] = 13;

    int at2 = arr[2];
    System.Console.WriteLine($"A (Array): index2={at2}");

    int target = 13;

    for (int i = 0; i < arr.Length; i++)
    {
        if (arr[i] == target)
            Console.WriteLine($"A (Array): index2={at2}");
        else
            Console.WriteLine("not found. Sorry");
    }
}
	
// B. List<T>
static void DemoList()
{
    var list = new List<int>() { 1, 2, 3, 4, 5 };
    list.Insert(2, 99);
    System.Console.WriteLine("implementing Insert at index 2:");
    foreach (var item in list) Console.Write(item + " ");
    System.Console.WriteLine();
    list.Remove(99);
    System.Console.WriteLine("After removing 99:");
    foreach (var item in list) Console.Write(item + " ");
    Console.WriteLine($"\nCounting after deleting {list.Count}");
    
}

// C. Stack<T>
static void DemoStack()
{
	// Create a stack to hold "browser history" URLs
        Stack<string> history = new Stack<string>();

        // 2️Push add pages
        history.Push("https://site.com/home");
        history.Push("https://site.com/about");
        history.Push("https://site.com/contact");

        // 3 Peek watchs current page without removing it
        string currentPage = history.Peek();
        Console.WriteLine($"Current page: {currentPage}\n");

        // 4 Pop removes pages in LIFO order
        Console.WriteLine("Visited pages in Back order:");
        while (history.Count > 0)
        {
            
            Console.WriteLine(history.Pop());
        }
	
}

// D. Queue<T>
static void DemoQueue()
{
	// 1 Create a queue for "print jobs" 
            Queue<string> printJobs = new Queue<string>();

        // 2 Enqueue add three jobs
        printJobs.Enqueue("Job 1: Print Report");
        printJobs.Enqueue("Job 2: Print Invoice");
        printJobs.Enqueue("Job 3: Print Presentation");

        // 3 Peek shows what will be printed next (without removing it)
        Console.WriteLine($"Next job in queue: {printJobs.Peek()}\n");

        // 4 Dequeue processes all jobs in order
        Console.WriteLine("Processing order:");
        while (printJobs.Count > 0)
        {
            string job = printJobs.Dequeue(); // removes and returns the first element
            Console.WriteLine(job);
        }		
}

// E. Dictionary<TKey,TValue>
static void DemoDictionary()
{
	 // 1️⃣ Create a dictionary mapping SKU → quantity
        Dictionary<string, int> stock = new Dictionary<string, int>
        {
            { "A101", 50 },
            { "B205", 20 },
            { "C333", 75 }
        };

        // 2️⃣ Update one quantity
        stock["B205"] = 25;

        // 3️⃣ Print all items
        Console.WriteLine("Updated stock:");
        foreach (var kv in stock)
        {
            Console.WriteLine($"{kv.Key} = {kv.Value}");
        }

        // 4️⃣ Try to get a missing key
        Console.WriteLine();
        if (stock.TryGetValue("Z999", out int missingQty))
            Console.WriteLine($"Found missing: {missingQty}");
        else
            Console.WriteLine("SKU 'Z999' not found (TryGetValue returned false)");
	
	
	
}

// F. HashSet<T>
static void DemoHashSet()
{
	// 11 Create a hash set with duplicates
        HashSet<int> numbers = new HashSet<int> { 1, 2, 2, 3 };
        Console.WriteLine("Initial set elements:");
        foreach (var n in numbers)
        {
            Console.WriteLine(n);
        }
        // 2️ Try to add a duplicate element
    bool added = numbers.Add(2);
        Console.WriteLine($"Trying to add duplicate '2': returned {added}");

        // 3️ Perform a union with another set {3,4,5}
        numbers.UnionWith(new HashSet<int> { 3, 4, 5 });

        // 4️ Print all elements and final count
        Console.WriteLine("\nFinal set elements:");
        foreach (var n in numbers)
        {
            Console.WriteLine(n);
        }
        Console.WriteLine($"\nFinal Count: {numbers.Count}");
	
	
	
	
}




