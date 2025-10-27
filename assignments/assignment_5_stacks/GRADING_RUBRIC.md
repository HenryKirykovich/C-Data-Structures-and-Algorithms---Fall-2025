# Assignment 5 Grading Rubric

## Overview

**Total Points: 105 (110 with extra credit)**  
**Due Date: October 29, 2024**

This assignment evaluates students' ability to apply Stack<T> concepts from Lab 5 to build a browser navigation system using dual-stack patterns.

---

## Core Implementation (90 points)

### Core Navigation Methods (35 points)

| Criteria             | Excellent (32-35)                                                               | Good (26-31)                             | Needs Work (20-25)                             | Incomplete (0-19)               |
| -------------------- | ------------------------------------------------------------------------------- | ---------------------------------------- | ---------------------------------------------- | ------------------------------- |
| **VisitUrl Method**  | Perfect implementation: saves current to back, clears forward, sets new current | Mostly correct, 1-2 minor logical errors | Basic logic present but missing key steps      | Major logical errors or missing |
| **GoBack Method**    | Perfect: guards empty stack, moves current to forward, pops from back           | Mostly correct implementation            | Basic logic but missing guard clauses or steps | Major errors or not implemented |
| **GoForward Method** | Perfect: guards empty stack, moves current to back, pops from forward           | Mostly correct implementation            | Basic logic but missing guard clauses or steps | Major errors or not implemented |

### Stack Management (20 points)

| Criteria               | Excellent (18-20)                                               | Good (14-17)                                  | Needs Work (10-13)                        | Incomplete (0-9)                         |
| ---------------------- | --------------------------------------------------------------- | --------------------------------------------- | ----------------------------------------- | ---------------------------------------- |
| **Dual-Stack Pattern** | Perfect understanding and implementation of back/forward stacks | Good implementation with minor issues         | Basic understanding but some confusion    | Poor understanding of dual-stack concept |
| **State Consistency**  | Navigation state always consistent, proper stack management     | Mostly consistent with minor edge case issues | Some inconsistencies in complex scenarios | Frequent state inconsistencies           |
| **LIFO Behavior**      | Perfect LIFO behavior for both stacks                           | Good LIFO implementation                      | Basic LIFO understanding                  | Poor or incorrect LIFO implementation    |

### Display & History Management (20 points)

| Criteria                  | Excellent (18-20)                                                     | Good (14-17)                              | Needs Work (10-13)                                    | Incomplete (0-9)                |
| ------------------------- | --------------------------------------------------------------------- | ----------------------------------------- | ----------------------------------------------------- | ------------------------------- |
| **DisplayBackHistory**    | Perfect formatting with numbered list, proper empty state handling    | Good display with minor formatting issues | Basic display but poor formatting or missing elements | Major errors or not implemented |
| **DisplayForwardHistory** | Perfect formatting with numbered list, proper empty state handling    | Good display with minor formatting issues | Basic display but poor formatting or missing elements | Major errors or not implemented |
| **ClearHistory Method**   | Perfect: counts pages, clears both stacks, shows confirmation message | Mostly correct with minor issues          | Basic functionality but missing count or confirmation | Major errors or not implemented |

### User Interface (10 points)

| Criteria             | Excellent (9-10)                                                             | Good (7-8)                         | Needs Work (5-6)                                            | Incomplete (0-4)                       |
| -------------------- | ---------------------------------------------------------------------------- | ---------------------------------- | ----------------------------------------------------------- | -------------------------------------- |
| **Menu Integration** | All features integrate seamlessly with provided UI, excellent error handling | Good integration with minor issues | Basic integration but some features don't work well with UI | Poor integration or frequent UI issues |

### Edge Case Handling (10 points)

| Criteria                   | Excellent (9-10)                                                     | Good (7-8)                            | Needs Work (5-6)                             | Incomplete (0-4)                    |
| -------------------------- | -------------------------------------------------------------------- | ------------------------------------- | -------------------------------------------- | ----------------------------------- |
| **Empty Stack Guards**     | Perfect guard clauses for all stack operations with helpful messages | Good guards with minor message issues | Basic guards but poor error messages         | Missing or inadequate guard clauses |
| **Invalid Input Handling** | Excellent validation and error handling for all inputs               | Good validation with minor gaps       | Basic validation but some inputs not handled | Poor or missing input validation    |

---

## Code Quality and Documentation (15 points)

### Implementation Quality (5 points)

| Criteria                  | Excellent (5)                                           | Good (4)                               | Needs Work (2-3)                             | Incomplete (0-1)                  |
| ------------------------- | ------------------------------------------------------- | -------------------------------------- | -------------------------------------------- | --------------------------------- |
| **Method Implementation** | Clean, efficient implementations with proper logic flow | Good implementations with minor issues | Basic implementations but some awkward logic | Poor or confusing implementations |

### Error Handling (5 points)

| Criteria                       | Excellent (5)                                                             | Good (4)                            | Needs Work (2-3)                            | Incomplete (0-1)               |
| ------------------------------ | ------------------------------------------------------------------------- | ----------------------------------- | ------------------------------------------- | ------------------------------ |
| **Guard Clauses & Edge Cases** | Excellent guard clauses with meaningful error messages for all edge cases | Good error handling with minor gaps | Basic error handling but missing some cases | Poor or missing error handling |

### Documentation (5 points)

| Criteria                        | Excellent (5)                                                          | Good (4)                        | Needs Work (2-3)                                     | Incomplete (0-1)                   |
| ------------------------------- | ---------------------------------------------------------------------- | ------------------------------- | ---------------------------------------------------- | ---------------------------------- |
| **ASSIGNMENT_NOTES.md Quality** | Complete, thoughtful reflection covering all required sections clearly | Good reflection with minor gaps | Basic reflection but lacks depth or missing sections | Missing or very poor documentation |

---

## Stretch Features (5 points - Extra Credit)

### History Limits (2 points)

| Implementation | Points | Requirements                                                      |
| -------------- | ------ | ----------------------------------------------------------------- |
| **Complete**   | 2      | Max history size implemented with proper eviction of oldest items |
| **Partial**    | 1      | Basic implementation but with minor issues                        |
| **None**       | 0      | Not implemented                                                   |

### JSON Export/Import (3 points)

| Implementation | Points | Requirements                                                |
| -------------- | ------ | ----------------------------------------------------------- |
| **Complete**   | 3      | Full save/load functionality with proper JSON serialization |
| **Partial**    | 1-2    | Basic implementation but missing features or has issues     |
| **None**       | 0      | Not implemented                                             |

---

## Submission Requirements

### Required Files

- [ ] All .cs source files (BrowserSession.cs with implemented methods)
- [ ] Project file (Assignment5.csproj)
- [ ] ASSIGNMENT_NOTES.md with implementation notes and testing documentation
- [ ] Code compiles and runs without errors

### Testing Evidence

Students should demonstrate their application handles:

- [ ] Basic navigation (visit → back → forward)
- [ ] Edge cases (empty stacks, invalid input)
- [ ] Complex scenarios (visit after going back)

---

## Common Issues to Watch For

### Major Problems (Significant Point Deduction)

- **Not clearing forward stack** when visiting new URL after going back
- **Missing guard clauses** for empty stack operations
- **Incorrect stack operations** (wrong direction, not maintaining LIFO)
- **Poor state management** (current page not properly updated)

### Minor Issues (Small Point Deduction)

- **Inconsistent user feedback** messages
- **Minor formatting** issues in display
- **Basic input validation** missing edge cases

### Code Quality Issues

- **Poor variable naming** or code organization
- **Missing comments** for complex logic
- **Inconsistent coding style**

---
