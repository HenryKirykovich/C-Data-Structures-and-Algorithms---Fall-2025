using System;
using System.Collections.Generic;

/*
=== QUICK REFERENCE GUIDE ===

Queue<T> Essential Operations:
- new Queue<SupportTicket>()        // Create empty queue
- queue.Enqueue(item)               // Add item to back (FIFO)
- queue.Dequeue()                   // Remove and return front item
- queue.Peek()                      // Look at front item (don't remove)
- queue.Clear()                     // Remove all items
- queue.Count                       // Get number of items

Safety Rules:
- ALWAYS check queue.Count > 0 before Dequeue() or Peek()
- Empty queue Dequeue() throws InvalidOperationException
- Empty queue Peek() throws InvalidOperationException

Common Patterns:
- Guard clause: if (queue.Count > 0) { ... }
- FIFO order: First item enqueued is first item dequeued
- Enumeration: foreach gives front-to-back order

Helpful Icons:
- ✅ Success
- ❌ Error
- 👀 Look
- 📋 Display
- ℹ️ Information
- 📊 Stats
- 🎫 Ticket
- 🔄 Process
*/

namespace QueueLab
{
    /// <summary>
    /// Student skeleton version - follow along with instructor to build this out!
    /// Complete the TODO steps to build a complete IT Support Desk Queue system.
    /// </summary>
    class Program
    {
        // TODO Step 1: Set up your data structures and tracking variables
    // Queue to hold support tickets (FIFO)
    // Use Queue<T> to demonstrate FIFO behavior as required by the lab
    private static Queue<SupportTicket> ticketQueue = new Queue<SupportTicket>();

        // Counter to generate ticket IDs (T001, T002, ... and U001 for urgent)
        private static int ticketCounter = 1;

        // Total operations performed in this session (enqueue/dequeue/clear etc.)
        private static int totalOperations = 0;

        // Session start time for statistics
        private static DateTime sessionStart = DateTime.Now;

        // Pre-defined ticket options for easy selection during demos
        private static readonly string[] CommonIssues = {
            "Login issues - cannot access email",
            "Password reset request",
            "Software installation help",
            "Printer not working",
            "Internet connection problems",
            "Computer running slowly",
            "Email not sending/receiving",
            "VPN connection issues",
            "Application crashes on startup",
            "File recovery assistance",
            "Monitor display problems",
            "Keyboard/mouse not responding",
            "Video conference setup help",
            "File sharing permission issues",
            "Security software alert"
        };

        static void Main(string[] args)
        {
            Console.WriteLine("🎫 IT Support Desk Queue Management");
            Console.WriteLine("===================================");
            Console.WriteLine("Building a ticket queue system with FIFO processing\n");

            bool running = true;
            while (running)
            {
                DisplayMenu();
                string choice = Console.ReadLine()?.ToLower() ?? "";

                switch (choice)
                {
                    case "1":
                    case "submit":
                        HandleSubmitTicket();
                        break;
                    case "2":
                    case "process":
                        HandleProcessTicket();
                        break;
                    case "3":
                    case "peek":
                    case "next":
                        HandlePeekNext();
                        break;
                    case "4":
                    case "display":
                    case "queue":
                        HandleDisplayQueue();
                        break;
                    case "5":
                    case "urgent":
                        HandleUrgentTicket();
                        break;
                    case "6":
                    case "search":
                        HandleSearchTicket();
                        break;
                    case "7":
                    case "stats":
                        HandleQueueStatistics();
                        break;
                    case "8":
                    case "clear":
                        HandleClearQueue();
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
            string nextTicket = ticketQueue.Count > 0 ? ticketQueue.Peek().TicketId : "None";

            Console.WriteLine("┌─ Support Desk Queue Operations ─────────────────┐");
            Console.WriteLine("│ 1. Submit      │ 2. Process    │ 3. Peek/Next  │");
            Console.WriteLine("│ 4. Display     │ 5. Urgent     │ 6. Search      │");
            Console.WriteLine("│ 7. Stats       │ 8. Clear      │ 9. Exit        │");
            Console.WriteLine("└─────────────────────────────────────────────────┘");
            Console.WriteLine($"Queue: {ticketQueue.Count} tickets | Next: {nextTicket} | Total ops: {totalOperations}");
            Console.Write("\nChoose operation (number or name): ");
        }

        // TODO Step 2: Handle submitting new tickets (Enqueue)
        static void HandleSubmitTicket()
        {
            Console.WriteLine("\n📝 Submit New Support Ticket");
            Console.WriteLine("Choose from common issues or enter custom:");

            // Math.Min() for safe array access - prevents index out of bounds errors
            // Display quick selection options
            for (int i = 0; i < Math.Min(5, CommonIssues.Length); i++)
            {
                Console.WriteLine($"  {i + 1}. {CommonIssues[i]}");
            }
            Console.WriteLine("  6. Enter custom issue");
            Console.WriteLine("  0. Cancel");
            
            Console.Write("\nSelect option (0-6): ");
            string? choice = Console.ReadLine();
            
            if (choice == "0")
            {
                Console.WriteLine("❌ Ticket submission cancelled.\n");
                return;
            }
            
            string description = "";
            // int.TryParse() for safe number conversion - better than catching exceptions
            if (int.TryParse(choice, out int index) && index >= 1 && index <= 5)
            {
                description = CommonIssues[index - 1];
            }
            else if (choice == "6")
            {
                Console.Write("Enter issue description: ");
                description = Console.ReadLine()?.Trim() ?? "";
            }
            
            // Input validation with multiple options - professional apps handle user choice
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("❌ Description cannot be empty. Ticket submission cancelled.\n");
                return;
            }
            // 1. Create ticket ID using ticketCounter (format: "T001", "T002", etc.)
            string ticketId = $"T{ticketCounter:D3}";

            // 2. Create new SupportTicket with ID, description, "Normal" priority, and "User"
            var ticket = new SupportTicket(ticketId, description, "Normal", "User");

            // 3. Enqueue the ticket to the back of the queue (standard FIFO)
            ticketQueue.Enqueue(ticket);

            // 4. Increment ticketCounter and totalOperations
            ticketCounter++;
            totalOperations++;

            // 5. Show success message with ticket ID, description, and queue position
            Console.WriteLine($"✅ Ticket submitted: {ticket.TicketId} - {ticket.Description}");
            Console.WriteLine($"   Position in queue: {ticketQueue.Count}\n");
        }

        // TODO Step 3: Handle processing tickets (Dequeue)
        static void HandleProcessTicket()
        {
            Console.WriteLine("\n🔄 Process Next Ticket");

            // 2. Guard: ensure queue has items
            if (ticketQueue.Count == 0)
            {
                Console.WriteLine("❌ No tickets in queue to process.\n");
                return;
            }

            // 4. Dequeue and process (remove from front)
            var ticket = ticketQueue.Dequeue();
            totalOperations++;

            Console.WriteLine("✅ Processing ticket:");
            Console.WriteLine(ticket.ToDetailedString());

            if (ticketQueue.Count > 0)
            {
                var next = ticketQueue.Peek();
                Console.WriteLine($"\nNext ticket: {next.TicketId} - {next.Description}\n");
            }
            else
            {
                Console.WriteLine("\n✨ All tickets processed. Queue is now empty.\n");
            }
        }

        // TODO Step 4: Handle peeking at next ticket
        static void HandlePeekNext()
        {
            Console.WriteLine("\n👀 View Next Ticket");

            if (ticketQueue.Count == 0)
            {
                Console.WriteLine("❌ Queue is empty. No tickets to view.\n");
                return;
            }
            var next = ticketQueue.Peek();
            Console.WriteLine("➡️ Next ticket to be processed:");
            Console.WriteLine(next.ToDetailedString());
            Console.WriteLine($"Position: 1 of {ticketQueue.Count}\n");
        }

        // TODO Step 5: Handle displaying the full queue
        static void HandleDisplayQueue()
        {
            Console.WriteLine("\n📋 Current Support Queue (FIFO Order):");

            if (ticketQueue.Count == 0)
            {
                Console.WriteLine("ℹ️ Queue is empty - no tickets waiting.\n");
                return;
            }

            Console.WriteLine($"Total tickets: {ticketQueue.Count}");
            int pos = 1;
            foreach (var ticket in ticketQueue)
            {
                string marker = pos == 1 ? " ← Next" : "";
                Console.WriteLine($"{pos:D2}. {ticket}{marker}");
                pos++;
            }
            Console.WriteLine();
        }

        // TODO Step 6: Handle clearing the queue
        static void HandleClearQueue()
        {
            Console.WriteLine("\n🧹 Clear All Tickets");

            if (ticketQueue.Count == 0)
            {
                Console.WriteLine("ℹ️ Queue is already empty. Nothing to clear.\n");
                return;
            }

            int countBefore = ticketQueue.Count;
            Console.Write($"This will remove {countBefore} tickets. Are you sure? (y/N): ");
            string? resp = Console.ReadLine()?.ToLower().Trim() ?? "";

            if (resp == "y" || resp == "yes")
            {
                ticketQueue.Clear();
                totalOperations++;
                Console.WriteLine($"✅ Cleared {countBefore} tickets from the queue.\n");
            }
            else
            {
                Console.WriteLine("❌ Clear operation cancelled.\n");
            }
        }

        // TODO Step 7: Handle urgent ticket submission (Priority)
        static void HandleUrgentTicket()
        {
            Console.WriteLine("\n🚨 Submit Urgent Ticket");
            Console.WriteLine("Note: Urgent tickets are flagged but will use standard enqueue for this demo.");
            Console.Write("Enter urgent issue description: ");
            string? desc = Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(desc))
            {
                Console.WriteLine("❌ Description cannot be empty. Urgent ticket cancelled.\n");
                return;
            }

            string ticketId = $"U{ticketCounter:D3}";
            var ticket = new SupportTicket(ticketId, desc, "Urgent", "User");
            // Urgent tickets are flagged but still enqueued (FIFO) for this lab
            ticketQueue.Enqueue(ticket);
            ticketCounter++;
            totalOperations++;

            Console.WriteLine($"✅ Urgent ticket submitted: {ticket.TicketId} - {ticket.Description}");
            Console.WriteLine("ℹ️ Note: real systems would prioritize urgent tickets to the front of the queue.\n");
        }

        // TODO Step 8: Handle searching for tickets
        static void HandleSearchTicket()
        {
            Console.WriteLine("\n🔎 Search Tickets");

            if (ticketQueue.Count == 0)
            {
                Console.WriteLine("ℹ️ Queue is empty. No tickets to search.\n");
                return;
            }

            Console.Write("Enter ticket ID or description keyword: ");
            string? term = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(term))
            {
                Console.WriteLine("❌ Search term cannot be empty.\n");
                return;
            }

            string lower = term.ToLower();
            bool found = false;
            int pos = 1;
            Console.WriteLine("\nSearch results:");
            foreach (var ticket in ticketQueue)
            {
                if (ticket.TicketId.ToLower().Contains(lower) || ticket.Description.ToLower().Contains(lower))
                {
                    Console.WriteLine($"{pos:D2}. {ticket}");
                    found = true;
                }
                pos++;
            }

            if (!found)
            {
                Console.WriteLine($"No tickets found matching '{term}'.\n");
            }
            else
            {
                Console.WriteLine();
            }
        }

        static void HandleQueueStatistics()
        {
            Console.WriteLine("\n📊 Queue Statistics");
            
            TimeSpan sessionDuration = DateTime.Now - sessionStart;
            
            Console.WriteLine($"Current Queue Status:");
            Console.WriteLine($"- Tickets in queue: {ticketQueue.Count}");
            Console.WriteLine($"- Total operations: {totalOperations}");
            Console.WriteLine($"- Session duration: {sessionDuration:hh\\:mm\\:ss}");
            Console.WriteLine($"- Next ticket ID: T{ticketCounter:D3}");
            
            if (ticketQueue.Count > 0)
            {
                var oldestTicket = ticketQueue.Peek();
                Console.WriteLine($"- Longest waiting: {oldestTicket.TicketId} ({oldestTicket.GetFormattedWaitTime()})");
                
                // Count by priority
                int normal = 0, high = 0, urgent = 0;
                foreach (var ticket in ticketQueue)
                {
                    switch (ticket.Priority.ToLower())
                    {
                        case "normal": normal++; break;
                        case "high": high++; break;
                        case "urgent": urgent++; break;
                    }
                }
                Console.WriteLine($"- By priority: 🟢 Normal({normal}) 🟡 High({high}) 🔴 Urgent({urgent})");
            }
            else
            {
                Console.WriteLine("- Queue is empty");
            }
            Console.WriteLine();
        }

        static void ShowSessionSummary()
        {
            Console.WriteLine("\n📋 Final Session Summary");
            Console.WriteLine("========================");
            
            TimeSpan sessionDuration = DateTime.Now - sessionStart;
            
            Console.WriteLine($"Session Statistics:");
            Console.WriteLine($"- Duration: {sessionDuration:hh\\:mm\\:ss}");
            Console.WriteLine($"- Total operations: {totalOperations}");
            Console.WriteLine($"- Tickets remaining: {ticketQueue.Count}");
            
            if (ticketQueue.Count > 0)
            {
                Console.WriteLine($"- Unprocessed tickets:");
                int position = 1;
                foreach (var ticket in ticketQueue)
                {
                    Console.WriteLine($"  {position:D2}. {ticket}");
                    position++;
                }
                Console.WriteLine("\n⚠️ Remember to process remaining tickets!");
            }
            else
            {
                Console.WriteLine("✨ All tickets processed - excellent work!");
            }
            
            Console.WriteLine("\nThank you for using the Support Desk Queue System!");
            Console.WriteLine("You've learned FIFO queue operations and real-world ticket management! 🎫\n");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}