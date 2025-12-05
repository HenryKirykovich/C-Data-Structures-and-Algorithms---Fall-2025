# Assignment 10: Flight Route Network Navigator ✈️

## Quick Reference for Development

**Due Date:** December 5th by 11:59 PM  
**Total Points:** 80

---

## 🎯 Learning Objectives

- **Master Graph data structures** for network representation and efficient route finding
- **Apply breadth-first search (BFS)** for shortest path algorithms in unweighted graphs
- **Implement Dijkstra's algorithm** for weighted shortest path optimization with priority queues
- **Understand adjacency list representation** for sparse graph efficiency and O(1) lookups
- **Build interactive flight planning applications** with professional menu systems and user experience
- **Practice algorithm complexity analysis** comparing BFS vs Dijkstra's performance characteristics

---

## 📋 Core Requirements

### 1. Flight Network Management System

- Load flight data from CSV into graph structure with adjacency list representation
- Represent airports as vertices and flights as directed weighted edges
- Handle multiple airlines serving same routes with different costs
- Display network statistics and connectivity information with metrics

### 2. Basic Search Operations Engine

- Find all direct flights between two airports using adjacency list lookup
- Get all destinations reachable from an airport via direct flights
- Find cheapest direct flight option using cost comparison
- Display route information with detailed flight and cost breakdown

### 3. Advanced Pathfinding Algorithms

- BFS traversal for finding any valid route between airports
- BFS for shortest route by number of stops (fewest hops)
- Dijkstra's algorithm for cheapest route by total cost (weighted shortest path)
- Path reconstruction from parent maps with proper route building

### 4. Network Analysis Tools

- Identify hub airports with most connections (highest degree vertices)
- Calculate comprehensive network-wide statistics and metrics
- Find isolated or poorly connected airports (low degree analysis)
- Generate detailed flight network reports with aggregate data

---

## 🔧 Implementation Requirements

You will implement **10 key methods** in the `FlightNetwork` class:

### Basic Search Methods:

- `FindDirectFlights(string origin, string destination)` - Adjacency list lookup for direct routes
- `GetDestinationsFrom(string origin)` - Return sorted list of reachable airports
- `FindCheapestDirectFlight(string origin, string destination)` - Minimum cost direct route

### BFS Pathfinding Methods:

- `FindRoute(string origin, string destination)` - BFS to find any valid route with queue traversal
- `FindShortestRoute(string origin, string destination)` - BFS for fewest stops (unweighted shortest path)

### Dijkstra's Algorithm Method:

- `FindCheapestRoute(string origin, string destination)` - Priority queue for lowest total cost path

### Network Analysis Methods:

- `FindHubAirports(int topN)` - Top N highest degree vertices (most connections)
- `CalculateNetworkStatistics()` - Comprehensive graph metrics and aggregate analysis
- `FindIsolatedAirports()` - Detect vertices with degree 0 (no connections)

### Optional Challenge Method:

- `FindRoutesByCriteria(string origin, string destination, int maxStops, decimal maxCost)` - DFS with constraints (extra challenge)

_Lab provides foundation (CSV loading, graph construction, AddAirport, AddFlight, DisplayAllAirports, GetAirport). You implement search, pathfinding, and analysis algorithms!_

---

## 📊 Grading Breakdown

| Component                   | Points | Requirements                                                                  |
| --------------------------- | ------ | ----------------------------------------------------------------------------- |
| **Basic Search Operations** | 15     | FindDirectFlights, GetDestinationsFrom, FindCheapestDirectFlight              |
| **BFS Pathfinding**         | 20     | FindRoute and FindShortestRoute with correct BFS queue-based implementation   |
| **Dijkstra's Algorithm**    | 20     | FindCheapestRoute with priority queue, distance tracking, path reconstruction |
| **Network Analysis**        | 15     | FindHubAirports, CalculateNetworkStatistics, FindIsolatedAirports             |
| **Implementation Quality**  | 5      | Clean algorithm implementations, proper data structure usage, efficiency      |
| **Documentation**           | 5      | Complete STUDY_NOTES.md with algorithm explanations and testing notes         |

**Total: 80 points**

**Note:** The multi-criteria search method `FindRoutesByCriteria` is optional and provided as an additional challenge for students interested in exploring DFS with backtracking and constraint satisfaction.

---

## 🔍 Research and Problem Solving

**No external resources or links are provided intentionally!** This assignment is designed to encourage you to develop essential programming research skills.

**When you get stuck, GOOGLE IT!** This is a critical skill for any developer. Examples of effective searches:

- `"C# BFS algorithm graph implementation queue"`
- `"Dijkstra's algorithm C# PriorityQueue example"`
- `"How to reconstruct path from parent map BFS C#"`
- `"C# Dictionary adjacency list graph representation"`
- `"Graph pathfinding algorithms comparison BFS vs Dijkstra"`
- `"C# PriorityQueue min heap usage enqueue dequeue"`
- `"How to detect if graph is connected C# BFS"`
- `"Adjacency list vs adjacency matrix sparse graph"`
- `"C# HashSet visited tracking graph traversal"`
- `"Dijkstra algorithm relaxation step explanation"`

**Remember:** Stack Overflow, Microsoft Docs, GeeksforGeeks, and C# documentation are your friends. Learning to find algorithm implementations and adapt them to your specific use case is just as important as understanding the theory!

---

## 💡 Tips for Success

### Understanding Graph Algorithms and Data Structures

- **BFS guarantees shortest path** in unweighted graphs (fewest number of stops/hops)
- **Dijkstra's finds optimal path** in weighted graphs (lowest total cost via edge weights)
- **Adjacency list is efficient** for sparse graphs with few connections relative to total vertices
- **Priority queue is essential** for Dijkstra's performance to extract minimum cost vertex efficiently
- **Parent map enables path reconstruction** by backtracking from destination to origin

### Common Pitfalls to Avoid

- **Forgetting to track visited nodes** in BFS (causes infinite loops and stack overflow)
- **Not reconstructing path correctly** from parent map (results in reversed or incomplete routes)
- **Confusing shortest hops vs cheapest cost** - these require different algorithms!
- **Case sensitivity issues** with airport codes (use ToUpperInvariant() consistently)
- **Not handling disconnected graphs** when no route exists between airports
- **Infinite loop in Dijkstra's** if visited check is missing or incorrect
- **Wrong priority queue order** (need min-heap, not max-heap for Dijkstra's)

### Testing Strategy

- Start with simple 2-3 airport networks you create manually to verify basic operations
- Draw graphs on paper to visualize expected paths before coding
- Verify each algorithm finds the expected routes with known test cases
- Test edge cases: no route exists, origin equals destination, invalid airport codes
- Use debugger to step through BFS and Dijkstra's queue operations
- Compare BFS shortest route vs Dijkstra's cheapest route to understand differences
- Test with provided flights.csv (52 flights, 16 airports) for realistic scenarios

---

## 📅 Submission Requirements

### What to Submit

1. **All source code files** (.cs files including your completed FlightNetwork.cs)
2. **Project file** (Assignment10.csproj with proper .NET configuration)
3. **Data files** (flights.csv and any additional test data files you create)
4. **STUDY_NOTES.md** with your implementation notes, algorithm explanations, challenges faced, and testing documentation

### Submission Format

- Submit link to your GitHub repository
- Code should be in `assignments/assignment_10_graphs` directory
- Include clear commit messages showing your systematic development process
- Ensure code builds and runs without errors using `dotnet build` and `dotnet run`

---

## 🚀 Development Workflow

### Phase 1: Understanding Lab Foundation (Completed in Lab)

1. Review `Airport` and `Flight` data models provided in lab
2. Understand adjacency list structure: `Dictionary<string, List<Flight>>`
3. Examine CSV loading logic and graph population in `LoadFlightsFromCSV`
4. Test provided display methods: `DisplayAllAirports`, `GetAirport`

### Phase 2: Basic Search Operations (Start Here)

1. Implement `FindDirectFlights()` using adjacency list direct lookup
2. Add `GetDestinationsFrom()` with LINQ and proper sorting
3. Implement `FindCheapestDirectFlight()` with cost comparison and OrderBy
4. Test all three methods with various airport pairs from flights.csv

### Phase 3: BFS Pathfinding (Core Algorithm)

1. Implement `FindRoute()` with basic BFS queue-based traversal
2. Add parent tracking Dictionary for path reconstruction logic
3. Implement `FindShortestRoute()` (BFS naturally guarantees shortest path)
4. Test with various origin/destination pairs, verify fewest stops

### Phase 4: Dijkstra's Implementation (Advanced Algorithm)

1. Set up `PriorityQueue<string, decimal>` for cost-based vertex extraction
2. Implement distance tracking Dictionary and relaxation logic
3. Add path reconstruction from parent map (similar to BFS)
4. Test and compare with BFS results - should find cheaper but possibly longer routes

### Phase 5: Network Analysis (Graph Metrics)

1. Implement `FindHubAirports()` with degree calculation and LINQ sorting
2. Add `CalculateNetworkStatistics()` with comprehensive metrics
3. Implement `FindIsolatedAirports()` detection for zero-degree vertices
4. Test with full network to verify hub identification (ORD, ATL typically hubs)

### Phase 6: Testing & Documentation (Final Polish)

1. Test all methods with comprehensive scenarios and edge cases
2. Handle invalid inputs: null parameters, non-existent airports, disconnected graphs
3. Verify error handling for file I/O exceptions and parsing errors
4. Complete STUDY_NOTES.md with algorithm explanations, time complexity analysis, and reflections

---

## 🧪 Testing Scenarios

### Basic Functionality Tests

1. Load `flights.csv` successfully - verify 52 flights and 16 airports loaded
2. Find direct flights SEA → SFO - should find Alaska Airlines flight
3. Get all destinations from ORD - should show 8+ reachable airports (hub)
4. Display cheapest direct flight SEA → LAX - compare multiple airline options

### BFS Pathfinding Tests

1. **Find any route** SEA → MIA - BFS should find valid multi-hop path
2. **Shortest route** SEA → MIA - verify fewest stops (likely through ATL hub)
3. **No route exists** - test disconnected airports, should return null gracefully
4. **Origin equals destination** - should handle edge case (return single-airport route)

### Dijkstra's Algorithm Tests

1. **Cheapest route** SEA → MIA - verify lowest total cost (may be more stops)
2. **Compare with BFS** - same origin/destination should show cost vs stops tradeoff
3. **Single hop** - cheapest route should match cheapest direct flight when available
4. **Hub routing** - verify Dijkstra's routes through cost-effective hubs vs direct expensive flights

### Network Analysis Tests

1. **Hub airports** - verify ORD and ATL appear in top hubs (most connections)
2. **Network statistics** - check total airports (16), total flights (52), averages
3. **Isolated airports** - test with modified CSV having disconnected airports
4. **Edge cases** - empty network, single airport, single flight

---

## 📐 Algorithm Complexity Reference

### Time Complexity Summary

| Operation                | Time Complexity  | Explanation                                     |
| ------------------------ | ---------------- | ----------------------------------------------- |
| **FindDirectFlights**    | O(k)             | k = flights from origin (adjacency list size)   |
| **GetDestinationsFrom**  | O(k log k)       | k flights + sorting destinations                |
| **FindCheapestDirect**   | O(k)             | Linear scan through k flights from origin       |
| **FindRoute (BFS)**      | O(V + E)         | Visit all vertices V and edges E in worst case  |
| **FindShortestRoute**    | O(V + E)         | Same as BFS (guaranteed shortest in unweighted) |
| **FindCheapestRoute**    | O((V + E) log V) | Dijkstra's with priority queue operations       |
| **FindHubAirports**      | O(V log V)       | Iterate vertices + sort by degree               |
| **CalculateStatistics**  | O(V + E)         | Traverse all vertices and edges once            |
| **FindIsolatedAirports** | O(V + E)         | Check all vertices for incoming/outgoing edges  |

**Key Insights:**

- V = number of airports (vertices), E = number of flights (edges)
- For flights.csv: V = 16, E = 52 (sparse graph)
- Adjacency list makes most operations very efficient
- BFS and Dijkstra's scale well even with thousands of airports

---

## 🌐 Real-World Applications

This assignment teaches concepts directly used in:

- **Flight Booking Systems** - Google Flights, Kayak, Expedia use Dijkstra's for route optimization
- **Navigation Apps** - Google Maps, Waze use A\* (enhanced Dijkstra's) for driving directions
- **Social Networks** - LinkedIn connection paths, Facebook friend suggestions use BFS
- **Network Routing** - Internet BGP routing protocols use shortest path algorithms
- **Supply Chain** - Amazon logistics, FedEx delivery route planning use graph algorithms
- **Game Development** - NPC pathfinding, navigation mesh traversal in AAA games
- **Ride Sharing** - Uber, Lyft driver-passenger matching and route optimization

---

## 🎓 Key Concepts to Master

### Graph Representation

```csharp
// Adjacency List - Efficient for sparse graphs
Dictionary<string, List<Flight>> routes = new Dictionary<string, List<Flight>>();

// Example structure:
// "SEA" → [Flight(SEA→SFO, $150), Flight(SEA→LAX, $180), Flight(SEA→ORD, $320)]
// "SFO" → [Flight(SFO→LAX, $120), Flight(SFO→ORD, $390)]

// Why adjacency list?
// - Space: O(V + E) instead of O(V²) for matrix
// - For 16 airports: List uses ~52 entries, Matrix would use 256 entries
// - Perfect for sparse graphs (real flight networks)
```

### BFS Path Reconstruction

```csharp
// Parent map tracks how we reached each airport
Dictionary<string, string> parents = new Dictionary<string, string>();

// During BFS:
parents[neighbor] = current;  // We reached 'neighbor' from 'current'

// Reconstruction:
List<string> path = new List<string>();
string node = destination;
while (node != origin) {
    path.Add(node);
    node = parents[node];  // Follow parent chain backwards
}
path.Add(origin);
path.Reverse();  // Reverse to get origin → destination order
```

### Dijkstra's Distance Relaxation

```csharp
// "Relaxation" = updating shortest known distance
decimal newCost = distances[current] + flight.Cost;

if (newCost < distances[neighbor]) {
    distances[neighbor] = newCost;  // Found shorter path!
    parents[neighbor] = current;    // Update how we got there
    priorityQueue.Enqueue(neighbor, newCost);  // Re-queue with new priority
}
```

---

## 📝 STUDY_NOTES.md Template

Your documentation should include:

### Implementation Notes

- Which methods were most challenging and why
- How you debugged BFS and Dijkstra's algorithms
- Interesting discoveries about graph traversal patterns

### Algorithm Explanations

- BFS: How queue ensures shortest path in unweighted graphs
- Dijkstra's: How priority queue + relaxation finds minimum cost path
- Path reconstruction: How parent maps enable route building

### Reflections

- What you learned about graph algorithms
- How this applies to real-world routing problems
- Challenges overcome during implementation

---

**Remember:** This assignment builds on Lab 10 graph concepts to solve real-world route optimization problems. Focus on understanding how BFS and Dijkstra's algorithms enable efficient pathfinding in network structures, and how adjacency lists make graph operations fast and memory-efficient!

**Good luck building your flight route navigator! ✈️🗺️**
