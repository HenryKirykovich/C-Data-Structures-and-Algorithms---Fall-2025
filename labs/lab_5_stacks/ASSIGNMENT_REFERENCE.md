# Lab 5: Interactive Stack Application

## Assignment Overview

In this hands-on lab, you'll build a complete interactive Stack<T> application with undo/redo functionality by following along with your instructor. This lab focuses on understanding the Last In, First Out (LIFO) principle and implementing real-world programming patterns used in text editors, image editors, and many other applications.

## Learning Objectives

By completing this lab, you will:

- Understand and implement Stack<T> operations (Push, Pop, Peek, Clear)
- Master the LIFO (Last In, First Out) principle through hands-on coding
- Build an interactive menu-driven console application
- Implement advanced dual-stack pattern for undo/redo functionality
- Practice proper error handling and input validation
- Create professional user interfaces with clear feedback

## What You'll Build

You'll create an **Interactive Action History System** that allows users to:

- Add actions to a history stack
- Remove actions with proper LIFO ordering
- View the current stack contents
- Undo previous removals (restore actions)
- Redo undone operations
- View session statistics
- Handle all edge cases gracefully

## Instructions

1. **Follow Along in Class**: This is a guided lab where you'll code along with your instructor
2. **Start with the Student Skeleton**: Use `StackStarter.cs` as your starting point
3. **Complete All 12 Steps**: Work through each TODO step as demonstrated
4. **Test Your Code**: Run and test your application after each major step
5. **Ask Questions**: If you get stuck or confused, ask for help immediately

## Files Provided

- **`StackStarter.cs`** - Your starting point with 12 TODO steps and quick reference guide
- **`README.md`** - Complete lab guide with concepts, examples, and testing strategies

## Submission Requirements

### What to Submit

Submit a link to your GitHub repository where your completed lab code is located in the `labs/lab_5_stacks` directory.

### Completion Criteria

Your submitted code must demonstrate:

- ✅ All 12 TODO steps completed
- ✅ Application runs without crashing
- ✅ Basic stack operations work (Push, Pop, Peek, Display, Clear)
- ✅ Proper error handling for empty stack operations
- ✅ Undo/Redo functionality implemented (advanced features)

### Testing Checklist

Before submitting, verify your application can:

1. Push multiple actions to the stack
2. Pop actions in LIFO order
3. Handle attempts to pop from an empty stack (show error, don't crash)
4. Display stack contents clearly
5. Clear all actions
6. Undo a pop operation (restore the action)
7. Redo an undone operation (remove it again)
8. Show session statistics

## Grading

This lab uses **completion-based grading**:

### Full Credit (20 points)

- Submitted working code that completes all basic requirements
- Application demonstrates understanding of Stack<T> operations
- Code shows evidence of following the guided steps

### Partial Credit (14 points)

- Basic stack operations work but advanced features (undo/redo) incomplete
- Some functionality missing but clear effort demonstrated

### Minimal Credit (8 points)

- Incomplete submission
- Code shows some understanding but many features missing

### No Credit (0 points)

- No submission or no evidence of participation

## Due Date

**Due: October 29, 2024** by 11:59 PM

Submit the link to your GitHub repository containing your completed lab code in the `labs/lab_5_stacks` directory. Late submissions will be penalized according to the course late policy.

---

**Remember**: This lab is about learning through doing. Focus on understanding the concepts as you build the application, and don't hesitate to ask questions during the guided session!
