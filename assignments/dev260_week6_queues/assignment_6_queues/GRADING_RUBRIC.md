# Assignment 6 Grading Rubric

## Overview

**Total Points: 105 (110 with extra credit)**  
**Due Date: November 5, 2024**

This assignment evaluates students' ability to apply Queue<T> concepts from Lab 6 to build a game matchmaking system using multi-queue patterns with different matching strategies.

---

## Core Implementation (90 points)

### Queue Management (30 points)

| Criteria              | Excellent (27-30)                                                                   | Good (21-26)                             | Needs Work (15-20)                             | Incomplete (0-14)                 |
| --------------------- | ----------------------------------------------------------------------------------- | ---------------------------------------- | ---------------------------------------------- | --------------------------------- |
| **AddToQueue Method** | Perfect implementation: adds to correct queue, calls JoinQueue(), proper validation | Mostly correct, 1-2 minor logical errors | Basic logic present but missing key steps      | Major logical errors or missing   |
| **Queue Selection**   | Perfect mode-based queue selection with proper switch/if logic                      | Good selection with minor issues         | Basic logic but some modes handled incorrectly | Poor or incorrect queue selection |
| **Player Tracking**   | Excellent queue time tracking and player state management                           | Good tracking with minor issues          | Basic tracking but missing some state updates  | Poor or missing state tracking    |

### Match Creation Logic (25 points)

| Criteria                  | Excellent (23-25)                                                     | Good (18-22)                               | Needs Work (13-17)                                | Incomplete (0-12)                    |
| ------------------------- | --------------------------------------------------------------------- | ------------------------------------------ | ------------------------------------------------- | ------------------------------------ |
| **TryCreateMatch Method** | Perfect implementation with correct logic for all three game modes    | Good implementation with minor mode issues | Basic logic but missing key mode differences      | Major errors or not implemented      |
| **Skill-Based Matching**  | Perfect ±2 skill level filtering for Ranked mode                      | Good skill filtering with minor issues     | Basic skill checking but incorrect range or logic | Poor or missing skill-based matching |
| **Queue Processing**      | Excellent FIFO and filtering logic, proper player removal from queues | Good processing with minor issues          | Basic processing but some edge cases missed       | Poor queue processing logic          |
| **Edge Case Handling**    | Perfect handling of empty queues, single players, no valid matches    | Good edge case handling                    | Basic handling but missing some cases             | Poor or missing edge case handling   |

### Match Processing (20 points)

| Criteria                   | Excellent (18-20)                                                       | Good (14-17)                          | Needs Work (10-13)                         | Incomplete (0-9)                   |
| -------------------------- | ----------------------------------------------------------------------- | ------------------------------------- | ------------------------------------------ | ---------------------------------- |
| **ProcessMatch Method**    | Perfect: simulates outcome, records history, updates stats, displays    | Good implementation with minor issues | Basic functionality but missing some steps | Major errors or not implemented    |
| **Statistics Updates**     | Excellent player stat tracking (wins, losses, skill rating adjustments) | Good stat updates with minor issues   | Basic updates but some stats not tracked   | Poor or missing statistics updates |
| **Match History Tracking** | Perfect match recording with comprehensive details                      | Good history tracking                 | Basic history but missing details          | Poor or missing match history      |

### Display Methods (15 points)

| Criteria               | Excellent (14-15)                                                   | Good (11-13)                              | Needs Work (8-10)                                     | Incomplete (0-7)                |
| ---------------------- | ------------------------------------------------------------------- | ----------------------------------------- | ----------------------------------------------------- | ------------------------------- |
| **DisplayQueueStatus** | Perfect formatting with numbered lists, queue times, empty handling | Good display with minor formatting issues | Basic display but poor formatting or missing elements | Major errors or not implemented |
| **DisplayPlayerStats** | Perfect detailed player info with queue status and match history    | Good player display with minor issues     | Basic player info but missing details                 | Major errors or not implemented |
| **GetQueueEstimate**   | Perfect wait time estimation with mode-specific logic               | Good estimation with minor logic issues   | Basic estimation but oversimplified                   | Poor or missing estimation      |

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

**Students must choose ONE of the three options below. Full credit (5 points) awarded for complete implementation of any single option.**

### Option 1: Team Formation (2v2 Matches) - 5 points

| Implementation | Points | Requirements                                       |
| -------------- | ------ | -------------------------------------------------- |
| **Complete**   | 5      | 2v2 matches implemented with proper team balancing |
| **Incomplete** | 0      | Not working or major functionality missing         |

### Option 2: Avoid Recent Opponents System - 5 points

| Implementation | Points | Requirements                                           |
| -------------- | ------ | ------------------------------------------------------ |
| **Complete**   | 5      | Prevents recent rematches with cooldown system working |
| **Incomplete** | 0      | Not working or major functionality missing             |

### Option 3: Advanced Queue Analytics - 5 points

| Implementation | Points | Requirements                                           |
| -------------- | ------ | ------------------------------------------------------ |
| **Complete**   | 5      | Queue time tracking and analytics features implemented |
| **Incomplete** | 0      | Not working or major functionality missing             |

---

## Game Mode Requirements Verification

### Casual Mode (FIFO Matching)

- [ ] Any two players can match regardless of skill level
- [ ] Simple first-in-first-out processing
- [ ] No skill restrictions or filtering

### Ranked Mode (Skill-Based Matching)

- [ ] Only players within ±2 skill levels can match
- [ ] Proper skill level validation and filtering
- [ ] Maintains competitive balance

### QuickPlay Mode (Hybrid Matching)

- [ ] Prefers skill-based matching when possible
- [ ] Falls back to broader matching for queue efficiency
- [ ] Balances speed vs. quality appropriately

---

## Submission Requirements

### Required Files

- [ ] All .cs source files (MatchmakingSystem.cs with implemented methods)
- [ ] Project file (Assignment6.csproj)
- [ ] ASSIGNMENT_NOTES.md with implementation notes and testing documentation
- [ ] Code compiles and runs without errors

### Testing Evidence

Students should demonstrate their application handles:

- [ ] Basic queue operations (add players, create matches)
- [ ] Skill-based matching in Ranked mode
- [ ] Different game mode behaviors
- [ ] Edge cases (empty queues, single players, no valid matches)
- [ ] Statistics tracking and display

---

## Common Issues to Watch For

### Major Problems (Significant Point Deduction)

- **Incorrect skill matching** in Ranked mode (not using ±2 level range)
- **Missing queue management** (not removing players after matching)
- **Poor game mode logic** (treating all modes the same way)
- **Not calling JoinQueue/LeaveQueue** methods for time tracking

### Minor Issues (Small Point Deduction)

- **Inconsistent user feedback** messages
- **Minor formatting** issues in display methods
- **Basic input validation** missing edge cases

### Code Quality Issues

- **Poor variable naming** or code organization
- **Missing comments** for complex matching logic
- **Inconsistent coding style**
- **Not following the TODO method signatures**

---

## Real-World Connection Evaluation

Students should demonstrate understanding of:

- **Queue concepts** in game industry applications
- **Matchmaking algorithms** and fairness vs. speed tradeoffs
- **Multi-queue systems** and when to use different strategies
- **Player experience** considerations in queue design

---
