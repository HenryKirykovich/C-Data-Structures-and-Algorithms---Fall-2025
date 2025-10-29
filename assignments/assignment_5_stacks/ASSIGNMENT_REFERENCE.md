# Assignment 5: Browser Navigation System

---

## Assignment Overview

Build a **Browser Navigation System** that simulates how web browsers handle back/forward navigation using stacks! This assignment applies the Stack<T> concepts from Lab 5 to create a real-world application that mimics browser behavior.

---

## Learning Objectives

By completing this assignment, you will:

- **Apply dual-stack patterns** learned in Lab 5 to solve real-world problems
- **Implement browser-like navigation** using back and forward stacks
- **Handle edge cases** with proper guard clauses and user feedback
- **Practice defensive programming** with input validation
- **Document your learning process** through thoughtful reflection

---

## What You'll Build

You'll create an **Interactive Browser Navigation System** that allows users to:

- Visit new URLs with proper navigation history tracking
- Navigate backward through browsing history
- Navigate forward through previously visited pages
- Display navigation history with professional formatting
- Clear browsing history with confirmation
- Handle all edge cases gracefully (empty stacks, invalid input)

---

## Implementation Requirements

You will implement **6 key methods** in the `BrowserSession` class:

### Core Navigation Methods

- `VisitUrl(string url, string title)` - Handle visiting new URLs
- `GoBack()` - Navigate backward through history
- `GoForward()` - Navigate forward through history

### Display & Management Methods

- `DisplayBackHistory()` - Show back navigation history with proper formatting
- `DisplayForwardHistory()` - Show forward navigation history with proper formatting
- `ClearHistory()` - Clear all navigation history with count confirmation

_All other classes and UI components are provided - focus on these core methods!_

---

## Key Technical Concepts

### Dual-Stack Pattern

Learn how professional applications implement browser navigation:

```csharp
// Two stacks working together
Stack<WebPage> backStack = new Stack<WebPage>();     // Pages you can go back to
Stack<WebPage> forwardStack = new Stack<WebPage>();  // Pages you can go forward to
WebPage? currentPage = null;                         // Current page being viewed
```

### Navigation Logic

- **Visit URL**: currentPage → backStack, clear forwardStack, set new currentPage
- **Go Back**: currentPage → forwardStack, pop from backStack → currentPage
- **Go Forward**: currentPage → backStack, pop from forwardStack → currentPage

---

## Testing Requirements

Your application must handle these scenarios:

### Basic Navigation Tests

1. Visit multiple URLs in sequence
2. Navigate back through history
3. Navigate forward through previously visited pages
4. Try back/forward on empty stacks (should show appropriate messages)

### Edge Case Tests

1. Visit URL when no current page exists
2. Navigate back to the beginning of history
3. Navigate forward to the end of history
4. **Critical test**: Visit new URL after going back (should clear forward history)

---

## Grading

This assignment uses **comprehensive grading** across multiple areas:

| Component                  | Points | Requirements                                                                      |
| -------------------------- | ------ | --------------------------------------------------------------------------------- |
| **Core Navigation**        | **35** | VisitUrl, GoBack, GoForward methods work correctly with proper dual-stack pattern |
| **Display Methods**        | **25** | DisplayBackHistory, DisplayForwardHistory with professional formatting            |
| **History Management**     | **15** | ClearHistory method with count tracking and confirmation                          |
| **Stack Management**       | **15** | Proper dual-stack pattern implementation and LIFO behavior                        |
| **Implementation Quality** | **5**  | Clean method implementations, proper logic flow                                   |
| **Error Handling**         | **5**  | Guard clauses, edge case handling, meaningful error messages                      |
| **Documentation**          | **5**  | Complete ASSIGNMENT_NOTES.md with thoughtful reflection                           |

### Extra Credit (5 points)

- **History Limits** (2 points) - Implement max history size with eviction
- **JSON Export/Import** (3 points) - Save/load session data

**Total: 105 points (110 with extra credit)**

---

## Files Provided

- **`WebPage.cs`** - Complete web page representation (URL, title, timestamp)
- **`BrowserNavigator.cs`** - Complete interactive UI and menu system
- **`Program.cs`** - Complete application entry point
- **`BrowserSession.cs`** - Framework with TODO methods for you to implement
- **`ASSIGNMENT_NOTES_TEMPLATE.md`** - Template for your reflection documentation

---

## Common Pitfalls to Avoid

- **Forgetting to clear forward stack** when visiting new URL after going back
- **Not handling empty stacks** properly with guard clauses
- **Confusing the direction** of stack operations (which stack gets which page)
- **Poor user feedback** for unavailable operations
- **Inadequate documentation** - your reflection is worth 5 points!

---

## Submission Requirements

### What to Submit

1. **All source code files** (.cs files with your implementations)
2. **Project file** (Assignment5.csproj)
3. **ASSIGNMENT_NOTES.md** - Your implementation notes and reflection

### Submission Format

- Submit link to your GitHub repository
- Code should be in `assignments/assignment_5_stacks` directory
- Include clear commit messages showing your development process

---

## Due Date

**Due: October 29, 2024** by 11:59 PM

Submit the link to your GitHub repository containing your completed assignment. Late submissions will be penalized according to the course late policy.

---

## Getting Started Tips

1. **Review Lab 5 concepts** - This assignment builds directly on the dual-stack pattern
2. **Understand the provided structure** - Examine the WebPage and BrowserNavigator classes
3. **Implement incrementally** - Start with VisitUrl, then Back, then Forward
4. **Test frequently** - Run your application after each method implementation
5. **Focus on the dual-stack pattern** - Understanding this concept is key to success

---

## Real-World Applications

This assignment teaches concepts used in:

- **Web browsers** (Chrome, Firefox, Safari navigation)
- **Text editors** (undo/redo with branching)
- **Mobile apps** (navigation stacks, view controllers)
- **Game development** (state management, save systems)
- **Database systems** (transaction rollback, change tracking)

---

**Remember**: This assignment is about applying Lab 5 concepts to solve a real problem. Focus on understanding the dual-stack pattern and how it creates the illusion of "traveling through time" in your browsing history!

**Good luck building your browser navigation system! 🌐**
