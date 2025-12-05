# Assignment 6: Game Matchmaking System 🎮

## Quick Reference for Development

**Due Date:** November 5, 2024 by 11:59 PM  
**Total Points:** 105 (110 with extra credit)

---

## 🎯 Learning Objectives

- **Apply queue patterns** learned in Lab 6 to solve real-world problems
- **Implement multi-queue systems** with different matching strategies
- **Handle skill-based matching** with filtering and validation
- **Design clean user interfaces** for console applications
- **Practice game industry patterns** used in actual matchmaking systems

---

## 📋 Core Requirements

### 1. Player Management System

- Create players with username and skill rating (1-10)
- Track wins, losses, and skill rating adjustments
- Display player statistics and profiles

### 2. Multi-Queue Matchmaking

- **Casual Mode:** Any two players can match (simple FIFO)
- **Ranked Mode:** Players within ±2 skill levels can match
- **QuickPlay Mode:** Prefer skill matching, but allow broader matching for speed

### 3. Match Processing

- Pair players appropriately based on game mode
- Simulate match outcomes with skill-based probability
- Update player statistics after matches

### 4. User Interface

- Interactive menu system
- Clear display of queue status and player information
- Professional error messages and confirmations

---

## ⭐ Stretch Features (Extra Credit - Choose ONE)

**Pick ONE of these three options for extra credit (5 points):**

### Option 1: Team Formation (2v2 Matches)

- Implement 2v2 team matches instead of 1v1
- Balance teams based on combined skill ratings
- Handle team formation logic and display

### Option 2: Avoid Recent Opponents System

- Track recent match history for each player
- Prevent matching players who played together recently
- Implement a "cooldown" period before rematches

### Option 3: Advanced Queue Analytics

- Track detailed queue time statistics
- Display average wait times per game mode
- Show peak usage times and queue efficiency metrics

---

## 🔧 Implementation Requirements

You will implement **6 key methods** in the `MatchmakingSystem` class:

### Core Matchmaking Methods:

- `AddToQueue(Player player, GameMode mode)` - Add players to appropriate queues
- `TryCreateMatch(GameMode mode)` - Create matches based on mode rules
- `ProcessMatch(Match match)` - Handle match outcomes and stat updates

### Display & Management Methods:

- `DisplayQueueStatus()` - Show all queue information with formatting
- `DisplayPlayerStats(Player player)` - Show detailed player information
- `GetQueueEstimate(GameMode mode)` - Calculate estimated wait times

_All other classes and UI components are provided - focus on these core methods!_

---

## 🧪 Testing Requirements

Your application must handle these scenarios:

### Basic Matchmaking Tests

- Create multiple players with different skill levels
- Add players to different queues
- Process matches and verify correct pairing
- Check that statistics update properly

### Game Mode Tests

- **Casual:** Any players should match regardless of skill
- **Ranked:** Only players within ±2 skill levels should match
- **QuickPlay:** Prefer skill matching but allow broader matching

### Edge Case Tests

- Try to create matches with empty queues
- Single player in queue (no match possible)
- All players same skill level in ranked
- Queue status display with various queue states

---

## 📊 Grading Breakdown

| Component            | Points | Requirements                                                  |
| -------------------- | ------ | ------------------------------------------------------------- |
| **Queue Management** | 30     | AddToQueue, proper queue selection, mode-based logic          |
| **Match Creation**   | 25     | TryCreateMatch with correct skill filtering for each mode     |
| **Match Processing** | 20     | ProcessMatch with win probability and stat updates            |
| **Display Methods**  | 15     | DisplayQueueStatus, DisplayPlayerStats with proper formatting |
| **Code Quality**     | 5      | Clean implementations, proper logic flow                      |
| **Error Handling**   | 5      | Guard clauses, edge cases, meaningful error messages          |
| **Documentation**    | 5      | Complete ASSIGNMENT_NOTES.md with thoughtful reflection       |
| **Extra Credit**     | 5      | Complete implementation of ONE stretch feature                |

**Total: 105 points (110 with extra credit)**

---

## 📚 Implementation Guide

### Phase 1: Basic Structure

1. Review the provided `Player`, `Match`, and `GameNavigator` classes
2. Understand the `MatchmakingSystem` class structure with multiple queues
3. Implement "Add to Queue" functionality

### Phase 2: Match Creation Logic

1. Implement Casual mode matching (any two players)
2. Implement Ranked mode matching (skill-based filtering)
3. Implement QuickPlay mode matching (hybrid approach)
4. Add guard clauses and error handling

### Phase 3: Match Processing & Statistics

1. Implement win probability calculation based on skill
2. Add match outcome processing and stat updates
3. Implement skill rating adjustments
4. Add match history tracking

### Phase 4: Display & Polish

1. Implement queue status display with formatting
2. Implement detailed player statistics display
3. Add wait time estimation
4. Test all scenarios and document in ASSIGNMENT_NOTES.md

---

## 🔍 Research and Problem Solving

**No external resources or links are provided intentionally!** This assignment is designed to encourage you to develop essential programming research skills.

**When you get stuck, GOOGLE IT!** This is a critical skill for any developer. Examples of effective searches:

- `"How to iterate through a queue in C#"`
- `"C# Queue<T> peek without removing"`
- `"C# switch statement with enum"`
- `"How to remove specific item from Queue C#"`
- `"C# LINQ where clause filtering"`

**Remember:** Stack Overflow, Microsoft Docs, and C# documentation are your friends. Learning to find solutions independently is just as important as implementing them!

---

## 💡 Tips for Success

### Understanding the Matchmaking Model

- **Casual Queue:** Fair FIFO processing, any skill levels
- **Ranked Queue:** Competitive matching within skill ranges
- **QuickPlay Queue:** Balance between speed and fairness

### Common Pitfalls to Avoid

- **Forgetting skill range validation** in Ranked mode
- **Not handling empty queues** properly
- **Incorrect probability calculations** for match outcomes
- **Poor user feedback** for queue status

### Testing Strategy

- Create players with varied skill levels (1, 3, 5, 7, 10)
- Test each queue mode independently
- Verify skill-based matching works correctly
- Test edge cases thoroughly

---

## 📅 Submission Requirements

### What to Submit

1. **All source code files** (.cs files)
2. **Project file** (Assignment6.csproj)
3. **ASSIGNMENT_NOTES.md** with your implementation notes and testing documentation

### Submission Format

- Submit link to your GitHub repository
- Code should be in `assignments/assignment_6_queues` directory
- Include clear commit messages showing your development process

### Due Date

**Due: November 5, 2024 by 11:59 PM**

---

## 🎓 Real-World Applications

This assignment teaches concepts used in:

- **Online gaming** (League of Legends, Overwatch matchmaking)
- **Ride-sharing apps** (Uber, Lyft driver-passenger pairing)
- **Food delivery** (DoorDash, Uber Eats order-driver matching)
- **Dating apps** (compatibility-based matching systems)
- **Job platforms** (skill-based candidate-employer matching)

---

## 🚀 Quick Start Checklist

### Before You Begin

- [ ] Download starter code from course files
- [ ] Place in `assignments/assignment_6_queues` directory
- [ ] Review `Player.cs`, `Match.cs`, and `GameNavigator.cs` classes
- [ ] Understand the 6 TODO methods in `MatchmakingSystem.cs`

### Development Order

- [ ] Implement `AddToQueue()` method first
- [ ] Add `TryCreateMatch()` for Casual mode
- [ ] Add Ranked mode skill filtering to `TryCreateMatch()`
- [ ] Add QuickPlay mode logic to `TryCreateMatch()`
- [ ] Implement `ProcessMatch()` method
- [ ] Add `DisplayQueueStatus()` method
- [ ] Add `DisplayPlayerStats()` method
- [ ] Implement `GetQueueEstimate()` method
- [ ] Test all scenarios thoroughly
- [ ] Complete `ASSIGNMENT_NOTES.md`

### Testing Checklist

- [ ] Create players with different skill levels
- [ ] Test Casual mode (any players match)
- [ ] Test Ranked mode (±2 skill levels only)
- [ ] Test QuickPlay mode (hybrid matching)
- [ ] Test empty queue handling
- [ ] Test single player scenarios
- [ ] Verify statistics update correctly
- [ ] Test queue status display
- [ ] Test player statistics display

---

**Remember:** This assignment is about applying Lab 6 concepts to solve a real problem. Focus on understanding multi-queue management and how different queues can have different processing rules while maintaining fairness!

**Good luck building your matchmaking system! 🎮**
