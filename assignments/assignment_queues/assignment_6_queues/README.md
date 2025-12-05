# Assignment 6: Game Matchmaking System

## 🎮 Overview

Build a **Game Matchmaking System** that simulates how online games pair players for matches using queues! This assignment applies the Queue<T> concepts from Lab 6 to create a real-world application that mimics game matchmaking behavior with skill-based matching and multiple game modes.

## 🎯 Learning Objectives

By completing this assignment, you will:

- **Apply queue patterns** learned in Lab 6 to solve real-world problems
- **Implement multi-queue systems** with different matching strategies
- **Handle skill-based matching** with filtering and validation
- **Design clean user interfaces** for console applications
- **Practice game industry patterns** used in actual matchmaking systems
- **Implement stretch features** like team formation and advanced statistics

## 📋 Requirements Overview

### Core Features (Required)

1. **Player Management System**

   - Create players with username and skill rating (1-10)
   - Track wins, losses, and skill rating adjustments
   - Display player statistics and profiles

2. **Multi-Queue Matchmaking**

   - Three game modes: Casual, Ranked, and QuickPlay
   - Different matching strategies per queue
   - Skill-based filtering for Ranked mode

3. **Match Processing**

   - Pair players appropriately based on game mode
   - Simulate match outcomes with skill-based probability
   - Update player statistics after matches

4. **User Interface**
   - Interactive menu system
   - Clear display of queue status and player information
   - Professional error messages and confirmations

### Stretch Features (Optional - Extra Credit - Choose ONE)

**Pick ONE of these three options for extra credit (5 points):**

1. **Team Formation (2v2 Matches)**

   - Implement 2v2 team matches instead of 1v1
   - Balance teams based on combined skill ratings
   - Handle team formation logic and display

2. **Avoid Recent Opponents System**

   - Track recent match history for each player
   - Prevent matching players who played together recently
   - Implement a "cooldown" period before rematches

3. **Advanced Queue Analytics**
   - Track detailed queue time statistics
   - Display average wait times per game mode
   - Show peak usage times and queue efficiency metrics

## 🔧 Technical Specifications

### Core Classes

The assignment includes these key components:

- **`MatchmakingSystem`** - Main class managing queues and matches (you implement core methods)
- **`Player`** - Represents a player with stats and preferences (provided)
- **`Match`** - Represents a completed match with players and outcome (provided)
- **`GameNavigator`** - Handles the interactive menu and user commands (provided)
- **`Program`** - Entry point and application orchestration (provided)

### Data Structures

```csharp
// Main matchmaking queues
Queue<Player> casualQueue = new Queue<Player>();
Queue<Player> rankedQueue = new Queue<Player>();
Queue<Player> quickPlayQueue = new Queue<Player>();
List<Player> allPlayers = new List<Player>();
List<Match> matchHistory = new List<Match>();
```

### Key Operations

1. **Add to Queue**: Add player to appropriate queue based on game mode
2. **Create Match**: Find compatible players and create matches
3. **Process Match**: Simulate match outcome and update player stats
4. **Display Queues**: Show current queue status with wait times
5. **Player Statistics**: Track and display comprehensive player metrics

## 🎯 Implementation Requirements

You will need to implement **6 key methods** in the `MatchmakingSystem` class:

### Core Matchmaking Methods

- `AddToQueue(Player player, GameMode mode)` - Add players to appropriate queues
- `TryCreateMatch(GameMode mode)` - Create matches based on mode rules
- `ProcessMatch(Match match)` - Handle match outcomes and stat updates

### Display & Management Methods

- `DisplayQueueStatus()` - Show all queue information with formatting
- `DisplayPlayerStats(Player player)` - Show detailed player information
- `GetQueueEstimate(GameMode mode)` - Calculate estimated wait times

_All other classes and UI components are provided - focus on these core methods!_

## 📝 Detailed Requirements

### 1. Queue Management Logic

- **Casual Mode**: Any two players can match (simple FIFO)
- **Ranked Mode**: Players within ±2 skill levels can match
- **QuickPlay Mode**: Prefer skill matching, but allow any match if queue is long
- **Always handle empty queues** with helpful error messages

### 2. Match Processing

- **Win probability** based on skill difference (higher skill = better chance)
- **Skill rating updates**: +1 for win, -1 for loss (minimum 1, maximum 10)
- **Statistics tracking**: Update wins, losses, and match history
- **Match validation**: Ensure valid players and outcomes

### 3. Input Validation

- Validate player names (no duplicates, not empty)
- Validate skill ratings (1-10 range)
- Handle queue operations gracefully
- Provide helpful error messages for invalid operations

## 🧪 Testing Requirements

Your application must handle these scenarios:

### Basic Matchmaking Tests

1. Create multiple players with different skill levels
2. Add players to different queues
3. Process matches and verify correct pairing
4. Check that statistics update properly

### Game Mode Tests

1. **Casual**: Any players should match regardless of skill
2. **Ranked**: Only players within ±2 skill levels should match
3. **QuickPlay**: Prefer skill matching but allow broader matching

### Edge Case Tests

1. Try to create matches with empty queues
2. Single player in queue (no match possible)
3. All players same skill level in ranked
4. Queue status display with various queue states

## 🎯 Grading Rubric

### Core Implementation (90 points)

| Component            | Points | Requirements                                                  |
| -------------------- | ------ | ------------------------------------------------------------- |
| **Queue Management** | 30     | AddToQueue, proper queue selection, mode-based logic          |
| **Match Creation**   | 25     | TryCreateMatch with correct skill filtering for each mode     |
| **Match Processing** | 20     | ProcessMatch with win probability and stat updates            |
| **Display Methods**  | 15     | DisplayQueueStatus, DisplayPlayerStats with proper formatting |

### Code Quality and Documentation (15 points)

| Aspect                     | Points | Requirements                                                 |
| -------------------------- | ------ | ------------------------------------------------------------ |
| **Implementation Quality** | 5      | Clean method implementations, proper logic flow              |
| **Error Handling**         | 5      | Guard clauses, edge case handling, meaningful error messages |
| **Documentation**          | 5      | Complete ASSIGNMENT_NOTES.md with thoughtful reflection      |

### Stretch Features (5 points - Extra Credit)

| Feature                          | Points | Requirements                                              |
| -------------------------------- | ------ | --------------------------------------------------------- |
| **Choose ONE of the following:** | 5      | Complete implementation of any one stretch feature option |
| - Team Formation (2v2)           |        | 2v2 matches with team balancing                           |
| - Avoid Recent Opponents         |        | Prevent recent rematches with cooldown system             |
| - Advanced Queue Analytics       |        | Detailed queue statistics and timing metrics              |

**Total: 105 points (110 with extra credit)**

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

## 💡 Tips for Success

### Understanding the Matchmaking Model

- **Casual Queue**: Fair FIFO processing, any skill levels
- **Ranked Queue**: Competitive matching within skill ranges
- **QuickPlay Queue**: Balance between speed and fairness

### Common Pitfalls to Avoid

1. **Forgetting skill range validation** in Ranked mode
2. **Not handling empty queues** properly
3. **Incorrect probability calculations** for match outcomes
4. **Poor user feedback** for queue status

### Testing Strategy

- Create players with varied skill levels (1, 3, 5, 7, 10)
- Test each queue mode independently
- Verify skill-based matching works correctly
- Test edge cases thoroughly

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

**Due: November 5, 2024** by 11:59 PM

## 🚀 Getting Started

1. **Review Lab 6 concepts** - This assignment builds directly on queue patterns
2. **Understand the provided structure** - Examine the Player and Match classes
3. **Implement queue management first** - This is the foundation
4. **Add matching logic incrementally** - Start with Casual, then Ranked
5. **Test frequently** - Verify each feature works before moving on

## 🔍 Research and Problem Solving

**No external resources or links are provided intentionally!** This assignment is designed to encourage you to develop essential programming research skills.

**When you get stuck, GOOGLE IT!** This is a critical skill for any developer. Examples of effective searches:

- `"How to iterate through a queue in C#"`
- `"C# Queue<T> peek without removing"`
- `"C# switch statement with enum"`
- `"How to remove specific item from Queue C#"`
- `"C# LINQ where clause filtering"`

**Remember**: Stack Overflow, Microsoft Docs, and C# documentation are your friends. Learning to find solutions independently is just as important as implementing them!

## 🎓 Real-World Applications

This assignment teaches concepts used in:

- **Online gaming** (League of Legends, Overwatch matchmaking)
- **Ride-sharing apps** (Uber, Lyft driver-passenger pairing)
- **Food delivery** (DoorDash, Uber Eats order-driver matching)
- **Dating apps** (compatibility-based matching systems)
- **Job platforms** (skill-based candidate-employer matching)

---

**Remember**: This assignment is about applying Lab 6 concepts to solve a real problem. Focus on understanding multi-queue management and how different queues can have different processing rules while maintaining fairness!

**Good luck building your matchmaking system! 🎮**
