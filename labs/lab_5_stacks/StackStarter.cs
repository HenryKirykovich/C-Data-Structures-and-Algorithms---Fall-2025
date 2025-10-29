using System;
using System.Collections.Generic;

/*
=== QUICK REFERENCE GUIDE ===

Stack<T> Essential Operations:
- new Stack<string>()           // Create empty stack
- stack.Push(item)              // Add item to top (LIFO)
- stack.Pop()                   // Remove and return top item
- stack.Peek()                  // Look at top item (don't remove)
- stack.Clear()                 // Remove all items
- stack.Count                   // Get number of items

Safety Rules:
- ALWAYS check stack.Count > 0 before Pop() or Peek()
- Empty stack Pop() throws InvalidOperationException
- Empty stack Peek() throws InvalidOperationException

Common Patterns:
- Guard clause: if (stack.Count > 0) { ... }
- LIFO order: Last item pushed is first item popped
- Enumeration: foreach gives top-to-bottom order

Helpful icons!:
- ✅ Success
- ❌ Error
- 👀 Look
- 📋 Display out
- ℹ️ Information
- 📊 Stats
- 📝 Write
*/

namespace StackLab
{
    /// <summary>
    /// Completed interactive Stack application with undo/redo and statistics.
    /// Follows the guided skeleton's 12 steps.
    /// </summary>
    class Program
    {

        // Step 1 - Declare two stacks for action history and undo functionality
        private static readonly Stack<string> actionHistory = new Stack<string>();
        private static readonly Stack<string> undoHistory = new Stack<string>();

        // Step 2 - Add a counter for total operations
        private static int totalOperations = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("=== Interactive Stack Demo ===");
            Console.WriteLine("Building an action history system with undo/redo\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                switch (choice)
                {
                    case "1":
                    case "push":
                        HandlePush();
                        break;
                    case "2":
                    case "pop":
                        HandlePop();
                        break;
                    case "3":
                    case "peek":
                    case "top":
                        HandlePeek();
                        break;
                    case "4":
                    case "display":
                        HandleDisplay();
                        break;
                    case "5":
                    case "clear":
                        HandleClear();
                        break;
                    case "6":
                    case "undo":
                        HandleUndo();
                        break;
                    case "7":
                    case "redo":
                        HandleRedo();
                        break;
                    case "8":
                    case "stats":
                        ShowStatistics();
                        break;
                    case "9":
                    case "exit":
                        running = false;
                        ShowSessionSummary();
                        break;
                    default:
                        Console.WriteLine("❌ Invalid choice. Please try again.\n");
                        break;
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("┌─ Stack Operations Menu ─────────────────────────┐");
            Console.WriteLine("│ 1. Push      │ 2. Pop       │ 3. Peek/Top    │");
            Console.WriteLine("│ 4. Display   │ 5. Clear     │ 6. Undo        │");
            Console.WriteLine("│ 7. Redo      │ 8. Stats     │ 9. Exit        │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");
            // Step 3 - add stack size and total operations to our display
            Console.WriteLine($"ℹ️  Stack size: {actionHistory.Count}  |  Undo stack: {undoHistory.Count}  |  Ops: {totalOperations}");
            Console.Write("\nChoose operation (number or name): ");
        }

        // Step 4 - Implement HandlePush method
        static void HandlePush()
        {
            Console.Write("📝 Enter action to push: ");
            string? input = Console.ReadLine();
            string value = (input ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(value))
            {
                Console.WriteLine("❌ Action cannot be empty.\n");
                return;
            }

            actionHistory.Push(value);
            // New action invalidates redo history
            undoHistory.Clear();
            totalOperations++;
            Console.WriteLine($"✅ Pushed: '{value}'\n");
        }

        // Step 5 - Implement HandlePop method
        static void HandlePop()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("❌ Nothing to pop. The stack is empty.\n");
                return;
            }

            string removed = actionHistory.Pop();
            // Save removed item for potential undo
            undoHistory.Push(removed);
            totalOperations++;
            Console.WriteLine($"✅ Popped: '{removed}'");
            if (actionHistory.Count > 0)
            {
                Console.WriteLine($"👀 New top: '{actionHistory.Peek()}'\n");
            }
            else
            {
                Console.WriteLine("ℹ️  Stack is now empty.\n");
            }
        }

        // Step 6 - Implement HandlePeek method
        static void HandlePeek()
        {
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("👀 Stack is empty. Nothing on top.\n");
                return;
            }

            string top = actionHistory.Peek();
            Console.WriteLine($"👀 Top: '{top}' (stack size: {actionHistory.Count})\n");
        }

        // Step 7 - Implement HandleDisplay method
        static void HandleDisplay()
        {
            Console.WriteLine("\n📋 Action History (Top → Bottom):");
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("(empty)\n");
                return;
            }

            int index = 0;
            foreach (var item in actionHistory)
            {
                string marker = index == 0 ? "<- top" : string.Empty;
                Console.WriteLine($" {index + 1}. {item} {marker}");
                index++;
            }
            Console.WriteLine($"Total: {actionHistory.Count}\n");
        }

        // Step 8 - Implement HandleClear method
        static void HandleClear()
        {
            if (actionHistory.Count == 0 && undoHistory.Count == 0)
            {
                Console.WriteLine("ℹ️  Nothing to clear.\n");
                return;
            }

            int wasActions = actionHistory.Count;
            int wasUndo = undoHistory.Count;

            actionHistory.Clear();
            undoHistory.Clear();
            totalOperations++;
            Console.WriteLine($"✅ Cleared. Removed {wasActions} from action stack and {wasUndo} from undo stack.\n");
        }

        // Step 9 - Implement HandleUndo method (Advanced)
        static void HandleUndo()
        {
            if (undoHistory.Count == 0)
            {
                Console.WriteLine("ℹ️  Nothing to undo.\n");
                return;
            }

            string restored = undoHistory.Pop();
            actionHistory.Push(restored);
            totalOperations++;
            Console.WriteLine($"✅ Undo: restored '{restored}' to the stack.\n");
        }

        // Step 10 - Implement HandleRedo method (Advanced)
        static void HandleRedo()
        {
            // Redo here means: re-apply the last removal (re-remove the item we just restored)
            if (actionHistory.Count == 0)
            {
                Console.WriteLine("ℹ️  Nothing to redo.\n");
                return;
            }

            string reRemoved = actionHistory.Pop();
            undoHistory.Push(reRemoved);
            totalOperations++;
            Console.WriteLine($"✅ Redo: removed '{reRemoved}' again.\n");
        }

        // Step 11 - Implement ShowStatistics method
        static void ShowStatistics()
        {
            Console.WriteLine("\n📊 Session Statistics");
            Console.WriteLine($"- Action stack size: {actionHistory.Count}");
            Console.WriteLine($"- Undo stack size:   {undoHistory.Count}");
            Console.WriteLine($"- Total operations:  {totalOperations}");
            Console.WriteLine($"- Stack empty:       {(actionHistory.Count == 0 ? "yes" : "no")}");
            if (actionHistory.Count > 0)
            {
                Console.WriteLine($"- Current top:       '{actionHistory.Peek()}'");
            }
            Console.WriteLine();
        }

        // Step 12 - Implement ShowSessionSummary method
        static void ShowSessionSummary()
        {
            Console.WriteLine("\n=== Session Summary ===");
            Console.WriteLine($"Total operations: {totalOperations}");
            Console.WriteLine($"Final stack size: {actionHistory.Count}");
            if (actionHistory.Count > 0)
            {
                Console.WriteLine("Remaining actions (Top → Bottom):");
                int i = 0;
                foreach (var item in actionHistory)
                {
                    string marker = i == 0 ? "<- top" : string.Empty;
                    Console.WriteLine($" {i + 1}. {item} {marker}");
                    i++;
                }
            }
            Console.WriteLine("\nGreat work today! 🚀 Keep practicing stacks and LIFO thinking.\n");
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
