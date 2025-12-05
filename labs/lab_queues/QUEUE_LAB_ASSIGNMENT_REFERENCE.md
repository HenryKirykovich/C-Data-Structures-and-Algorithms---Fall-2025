# Lab 6: Interactive Queue Application

## Assignment Overview

In this hands-on lab, you'll build a complete interactive Queue<T> application featuring an IT Support Desk Queue system by following along with your instructor. This lab focuses on understanding the First In, First Out (FIFO) principle and implementing real-world programming patterns used in help desk software, call centers, and many other queue-based systems.

## Learning Objectives

By completing this lab, you will:

- Understand and implement Queue<T> operations (Enqueue, Dequeue, Peek, Clear)
- Master the FIFO (First In, First Out) principle through hands-on coding
- Build an interactive menu-driven console application with real-time status
- Implement professional ticket management system with validation
- Practice proper error handling and input validation patterns
- Create user-friendly interfaces with clear feedback and visual indicators
- Compare and contrast Queue (FIFO) vs Stack (LIFO) behavior

## What You'll Build

You'll create an **Interactive IT Support Desk Queue System** that allows users to:

- Submit support tickets with quick-select or custom descriptions
- Process tickets in FIFO order (first submitted = first processed)
- View the next ticket without removing it (peek functionality)
- Display the complete queue with position indicators
- Submit urgent tickets with priority identification
- Search for specific tickets by ID or keywords
- Clear the entire queue with confirmation
- View comprehensive session statistics

## Instructions

1. **Follow Along in Class**: This is a guided lab where you'll code along with your instructor
2. **Start with the Student Skeleton**: Use `QueueStarter.cs` as your starting point
3. **Complete All 8 TODO Steps**: Work through each step as demonstrated in class
4. **Test Your Code**: Run and test your application after each major step
5. **Ask Questions**: If you get stuck or confused, ask for help immediately

## Files Provided

- **`QueueStarter.cs`** - Your starting point with 8 TODO steps and quick reference guide
- **`README.md`** - Complete lab guide with concepts, examples, and testing strategies
- **`SupportTicket.cs`** - Pre-built ticket class with properties and methods

## Submission Requirements

### What to Submit

Submit a link to your GitHub repository where your completed lab code is located in the `labs/lab-6-queues` directory.

### Completion Criteria

Your submitted code must demonstrate:

- ✅ All 8 TODO steps completed
- ✅ Application runs without crashing
- ✅ Basic queue operations work (Enqueue, Dequeue, Peek, Display, Clear)
- ✅ Proper error handling for empty queue operations
- ✅ Queue variables properly declared (Step 1)
- ✅ Ticket submission with validation (Step 2)
- ✅ Ticket processing with FIFO behavior (Step 3)
- ✅ Peek functionality implemented (Step 4)
- ✅ Queue display with position indicators (Step 5)
- ✅ Queue clearing with confirmation (Step 6)
- ✅ Urgent ticket handling (Step 7)
- ✅ Search functionality (Step 8)

### Testing Checklist

Before submitting, verify your application can:

1. **Submit tickets** using both quick-select options (1-5) and custom descriptions
2. **Process tickets** in FIFO order (first submitted = first processed)
3. **Handle empty queue** attempts gracefully (show error, don't crash)
4. **Peek at next ticket** without modifying the queue
5. **Display queue contents** with clear position numbers and visual indicators
6. **Clear all tickets** with user confirmation
7. **Search for tickets** by ID ("T001", "U001") or description keywords
8. **Show session statistics** (already implemented and working)
9. **Handle edge cases** (empty inputs, invalid menu choices, cancellations)

## Grading

This lab uses **completion-based grading**:

### Full Credit (20 points)

- Submitted working code that completes all 8 TODO steps
- Application demonstrates understanding of Queue<T> operations and FIFO behavior
- Code shows evidence of following the guided steps
- All basic and advanced features work correctly

### Partial Credit (14 points)

- Most TODO steps completed but some functionality incomplete
- Basic queue operations work but advanced features (urgent tickets, search) may be missing
- Clear effort demonstrated but minor issues present

### Minimal Credit (8 points)

- Some TODO steps completed
- Code shows basic understanding but significant functionality missing
- Application may have issues but shows participation

### No Credit (0 points)

- No submission or no evidence of participation
- Code doesn't compile or shows no understanding of queue concepts

## Due Date

**Due: November 5th** by 11:59 PM

Submit the link to your GitHub repository containing your completed lab code in the `labs/lab-6-queues` directory. Late submissions will be penalized according to the course late policy.

## Key Concepts Covered

### FIFO vs LIFO Comparison

- **Queue (FIFO)**: First ticket submitted = First ticket processed (like a line at the bank)
- **Stack (LIFO)**: Last item added = First item removed (like a stack of plates)

### Professional Application Patterns

- Input validation with multiple options
- User confirmation for destructive operations
- Real-time status display in menus
- Professional ID generation (T001, T002, U001)
- Search functionality with case-insensitive matching

### Safety Patterns

- Always guard `Dequeue()` and `Peek()` operations with count checks
- Validate user input before processing
- Provide clear error messages and feedback

## Real-World Applications

This lab teaches patterns used in:

- Help desk software (ServiceNow, Jira Service Desk)
- Call center queue management
- Operating system process scheduling
- Web application request handling
- Message queue systems

---

**Remember**: This lab is about learning through doing. Focus on understanding the FIFO principle and how queues differ from stacks as you build the application. Don't hesitate to ask questions during the guided session!
