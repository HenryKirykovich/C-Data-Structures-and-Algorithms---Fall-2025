# C# Data Structures and Algorithms – Fall 2025

This repository contains implementations of fundamental data structures and algorithms in C#. It's designed for educational purposes to explore concepts like linked lists, trees, multithreading, and more.

## Project Structure

### Projects
- **DataStructures** - Core library containing data structure implementations
- **Examples** - Console application demonstrating the data structures
- **DataStructures.Tests** - Unit tests for all implementations

### Data Structures Implemented

#### Linked Lists
- **SinglyLinkedList<T>** - A generic singly linked list implementation
  - Features: Add first/last, remove, contains, enumeration
  - Thread-safe: No
  - Time Complexity: O(1) for add first, O(n) for add last, search, and remove

#### Trees
- **BinarySearchTree<T>** - A generic binary search tree implementation
  - Features: Insert, remove, search, traversals (in-order, pre-order, post-order)
  - Thread-safe: No
  - Time Complexity: O(log n) average case, O(n) worst case

#### Multithreading
- **ThreadSafeQueue<T>** - A thread-safe queue implementation using locks
  - Features: Enqueue, dequeue, peek, wait operations
  - Thread-safe: Yes
  - Includes Producer-Consumer pattern example

## Getting Started

### Prerequisites
- .NET 8.0 SDK or later

### Building the Project
```bash
dotnet build
```

### Running Tests
```bash
dotnet test
```

### Running Examples
```bash
dotnet run --project Examples
```

## Example Usage

### Singly Linked List
```csharp
var list = new SinglyLinkedList<int>();
list.AddLast(1);
list.AddLast(2);
list.AddFirst(0);
Console.WriteLine($"Count: {list.Count}"); // Output: Count: 3
Console.WriteLine($"Contains 1: {list.Contains(1)}"); // Output: Contains 1: True
```

### Binary Search Tree
```csharp
var bst = new BinarySearchTree<int>();
bst.Insert(50);
bst.Insert(30);
bst.Insert(70);
var inOrder = bst.InOrderTraversal(); // [30, 50, 70]
```

### Thread-Safe Queue
```csharp
var queue = new ThreadSafeQueue<string>();
queue.Enqueue("Hello");
queue.Enqueue("World");

if (queue.TryDequeue(out var item))
{
    Console.WriteLine(item); // Output: Hello
}
```

## Testing

The project includes comprehensive unit tests covering:
- All public methods of each data structure
- Edge cases and error conditions
- Thread safety for concurrent operations
- Performance characteristics

Run tests with detailed output:
```bash
dotnet test --verbosity normal
```

## Learning Goals

This project demonstrates:
1. **Data Structure Implementation** - Building fundamental data structures from scratch
2. **Generic Programming** - Using C# generics for type-safe, reusable code
3. **Algorithm Analysis** - Understanding time and space complexity
4. **Thread Safety** - Implementing concurrent data structures
5. **Unit Testing** - Writing comprehensive tests for data structures
6. **Documentation** - Proper code documentation and XML comments

## License

This project is for educational purposes. Feel free to use and modify for learning.
