# Assignment 10: Flight Route Network Navigator

## 📚 Overview

Build a **Flight Route Network Navigator** that demonstrates the power of Graph data structures for efficient route finding and network analysis! This assignment applies Graph concepts to create a real-world application that manages flight connections, finds optimal routes, and provides interactive flight planning with sophisticated pathfinding algorithms.

## 🎯 Learning Objectives

By completing this assignment, you will:

- **Master Graph data structures** for network representation and navigation
- **Apply breadth-first search (BFS)** for shortest path finding
- **Implement Dijkstra's algorithm** for weighted shortest path optimization
- **Understand adjacency list representation** for efficient graph storage
- **Build interactive flight planning applications** with professional menu systems
- **Practice algorithm complexity analysis** comparing different pathfinding approaches

## 📋 Requirements Overview

### Core Features (Required)

1. **Flight Network Management System**

   - Load flight data from `flights.csv` into graph structure
   - Represent airports as vertices and flights as weighted edges
   - Handle bidirectional routes and multiple airlines
   - Display network statistics and connectivity information

2. **Basic Search Operations**

   - Find all direct flights between two airports
   - Get all destinations reachable from an airport
   - Find cheapest direct flight between locations
   - Display route information with details

3. **Pathfinding Algorithms**

   - Find any valid route using BFS traversal
   - Find shortest route by number of stops (BFS)
   - Find cheapest route by total cost (Dijkstra's algorithm)
   - Compare different routing strategies

4. **Network Analysis Tools**
   - Identify hub airports with most connections
   - Calculate network-wide statistics
   - Find isolated or poorly connected airports
   - Generate comprehensive flight network reports

## 🔧 Technical Specifications

### Core Classes

The assignment includes these key components:

- **`Airport`** - Airport data model with code, name, city, country (provided in lab)
- **`Flight`** - Flight/edge data model with route details and costs (provided in lab)
- **`FlightNetwork`** - Main graph class with adjacency list (you implement 10+ core methods)
- **`FlightNetworkNavigator`** - Interactive menu system and user interface (provided)
- **`Program`** - Entry point and application orchestration (provided in lab)

### Data Structures

```csharp
// Core graph storage using adjacency list representation
Dictionary<string, Airport> airports = new Dictionary<string, Airport>();
Dictionary<string, List<Flight>> routes = new Dictionary<string, List<Flight>>();

// Search and pathfinding support structures
Queue<string> bfsQueue = new Queue<string>();
PriorityQueue<string, decimal> dijkstraQueue = new PriorityQueue<string, decimal>();
Dictionary<string, string> parentMap = new Dictionary<string, string>();
HashSet<string> visited = new HashSet<string>();
```

### Key Operations

1. **Load Flight Data**: Parse CSV and populate graph structure
2. **Direct Flight Search**: Adjacency list lookup for immediate connections
3. **BFS Pathfinding**: Queue-based traversal for shortest hop-count routes
4. **Dijkstra's Algorithm**: Priority queue-based weighted shortest path
5. **Network Analysis**: Degree calculation and connectivity metrics

## 🎯 Implementation Requirements

You will need to implement **10+ key methods** in the `FlightNetwork` class:

### Basic Search Operations (Completed Partially in Lab)

- `LoadFlightsFromCSV(string filename)` - Parse CSV and build graph (COMPLETED IN LAB)
- `AddAirport(Airport airport)` - Add vertex to graph (COMPLETED IN LAB)
- `AddFlight(Flight flight)` - Add edge to adjacency list (COMPLETED IN LAB)
- `DisplayAllAirports()` - Show all vertices with formatting (COMPLETED IN LAB)

### Search & Query Methods (YOUR IMPLEMENTATION)

- `FindDirectFlights(string origin, string destination)` - Return direct flight list
- `GetDestinationsFrom(string origin)` - Return all reachable airports via direct flights
- `FindCheapestDirectFlight(string origin, string destination)` - Minimum cost direct flight

### Pathfinding Algorithms (YOUR IMPLEMENTATION)

- `FindRoute(string origin, string destination)` - BFS to find any valid route
- `FindShortestRoute(string origin, string destination)` - BFS for fewest stops
- `FindCheapestRoute(string origin, string destination)` - Dijkstra's for lowest cost

### Advanced Search (YOUR IMPLEMENTATION - OPTIONAL CHALLENGE)

- `FindRoutesByCriteria(string origin, string destination, int maxStops, decimal maxCost)` - Constrained search (optional)

### Network Analysis Methods (YOUR IMPLEMENTATION)

- `FindHubAirports(int topN)` - Highest degree vertices
- `CalculateNetworkStatistics()` - Comprehensive graph metrics
- `FindIsolatedAirports()` - Vertices with degree 0

_Lab provides foundation (CSV loading, graph setup, display). You implement search, pathfinding, and analysis!_

## 📝 Detailed Requirements

### 1. Graph Construction Logic (Completed in Lab)

- **CSV Parsing**: Read flight data using `File.ReadAllLines()` and split by comma
- **Airport Creation**: Build Airport objects from unique codes
- **Flight Storage**: Populate adjacency list with Flight objects
- **Bidirectional Handling**: Add reverse routes for round-trip flights

### 2. BFS Pathfinding Implementation

- **Algorithm**: Breadth-first search with queue
- **Path Reconstruction**: Use parent tracking to rebuild route
- **Termination**: Stop when destination found or queue empty
- **Output**: Return List&lt;string&gt; of airport codes in order

### 3. Dijkstra's Algorithm Implementation

- **Priority Queue**: Use PriorityQueue&lt;string, decimal&gt; for min-cost extraction
- **Distance Tracking**: Maintain shortest known distances to each airport
- **Path Reconstruction**: Track parents for final route building
- **Relaxation**: Update distances when shorter path found

### 4. Network Analysis

- **Degree Calculation**: Count outgoing flights per airport
- **Hub Identification**: Sort by degree and return top N
- **Statistics Aggregation**: Calculate averages and extremes
- **Isolation Detection**: Find airports with no connections

## 🧪 Testing Requirements

Your application must handle these scenarios:

### Basic Functionality Tests

1. Load `flights.csv` successfully and display airport count
2. Find direct flights between specific airports
3. Get all destinations from a hub airport
4. Display cheapest direct flight option

### Pathfinding Tests

1. **BFS Route Finding**: Find any route between distant airports
2. **Shortest Route**: Verify fewest-stop path is found
3. **Cheapest Route**: Verify Dijkstra's finds minimum total cost
4. **No Route Exists**: Handle disconnected airports gracefully

### Network Analysis Tests

1. Identify correct hub airports (highest connections)
2. Calculate accurate network statistics
3. Find isolated airports with no routes
4. Handle edge cases (single airport, no flights)

### Edge Case Tests

1. Search for routes from airport to itself
2. Invalid airport codes (case sensitivity)
3. Empty CSV or missing file
4. Airports with only incoming or only outgoing flights

## 🎯 Grading Rubric

### Core Implementation (55 points)

| Component                   | Points | Requirements                                                    |
| --------------------------- | ------ | --------------------------------------------------------------- |
| **Basic Search Operations** | 15     | FindDirectFlights, GetDestinations, FindCheapestDirectFlight    |
| **BFS Pathfinding**         | 20     | FindRoute and FindShortestRoute with correct BFS implementation |
| **Dijkstra's Algorithm**    | 20     | FindCheapestRoute with priority queue and path reconstruction   |

### Network Analysis & Quality (20 points)

| Component                  | Points | Requirements                                                 |
| -------------------------- | ------ | ------------------------------------------------------------ |
| **Network Analysis**       | 15     | FindHubAirports, CalculateStatistics, FindIsolatedAirports   |
| **Implementation Quality** | 5      | Clean algorithm implementations, proper data structure usage |

### Documentation (5 points)

| Aspect            | Points | Requirements                                        |
| ----------------- | ------ | --------------------------------------------------- |
| **Documentation** | 5      | Complete STUDY_NOTES.md with algorithm explanations |

**Total: 80 points**

**Note**: The multi-criteria search method `FindRoutesByCriteria` is optional and provided as an additional challenge for students who want to explore DFS with backtracking.

## 📚 Implementation Guide

### Phase 1: Understanding Lab Foundation (Completed during Lab)

1. Review `Airport` and `Flight` data models from lab
2. Understand adjacency list structure in `FlightNetwork`
3. Examine CSV loading and graph population logic
4. Test provided display and basic operations

### Phase 2: Basic Search Operations

1. Implement `FindDirectFlights()` using adjacency list lookup
2. Add `GetDestinationsFrom()` with proper sorting
3. Implement `FindCheapestDirectFlight()` with cost comparison
4. Test all three methods with various airport pairs

### Phase 3: BFS Pathfinding

1. Implement `FindRoute()` with basic BFS traversal
2. Add parent tracking for path reconstruction
3. Implement `FindShortestRoute()` (BFS guarantees shortest)
4. Test with various origin/destination pairs

### Phase 4: Dijkstra's Implementation

1. Set up PriorityQueue&lt;string, decimal&gt; for cost-based traversal
2. Implement distance tracking and relaxation logic
3. Add path reconstruction from parent map
4. Test and compare with BFS results

### Phase 5: Network Analysis

1. Add `FindHubAirports()` with degree calculation
2. Implement `CalculateNetworkStatistics()`
3. Add `FindIsolatedAirports()` detection

### Phase 6: Testing & Documentation

1. Test all methods with comprehensive scenarios
2. Handle edge cases and invalid inputs
3. Verify error handling for invalid airport codes and missing routes
4. Complete STUDY_NOTES.md with algorithm explanations and reflections

## 💡 Tips for Success

### Understanding Graph Algorithms

- **BFS guarantees shortest path** in unweighted graphs (fewest hops)
- **Dijkstra's finds optimal path** in weighted graphs (lowest total cost)
- **Adjacency list is efficient** for sparse graphs (few connections relative to airports)
- **Priority queue is essential** for Dijkstra's performance

### Common Pitfalls to Avoid

1. **Forgetting to track visited nodes** in BFS (causes infinite loops)
2. **Not reconstructing path correctly** from parent map
3. **Confusing shortest hops vs cheapest cost** (different algorithms!)
4. **Case sensitivity issues** with airport codes
5. **Not handling disconnected graphs** (no route exists)

### Testing Strategy

- Start with simple 2-3 airport networks you create manually
- Verify each algorithm finds expected routes
- Test edge cases before complex scenarios
- Use debugger to step through BFS and Dijkstra's
- Draw graphs on paper to visualize expected paths

## 📅 Submission Requirements

### What to Submit

1. **All source code files** (.cs files including your completed FlightNetwork.cs)
2. **Project file** (Assignment10.csproj)
3. **Data files** (flights.csv and any additional test data you create)
4. **STUDY_NOTES.md** with your implementation notes, algorithm explanations, and testing documentation

### Submission Format

- Submit link to your GitHub repository
- Code should be in `assignments/assignment_10_graphs` directory
- Include clear commit messages showing your development process

### Due Date

**Due: December 5, 2024** by 11:59 PM

## 🚀 Getting Started

1. **Complete Lab 10** - Ensure you understand graph structure and CSV loading
2. **Review graph algorithms** - Understand BFS and Dijkstra's on paper first
3. **Start with basic search** - FindDirectFlights is the easiest entry point
4. **Implement BFS next** - This builds foundation for all pathfinding
5. **Tackle Dijkstra's carefully** - Draw examples and test incrementally

## 🔍 Research and Problem Solving

**No external resources or links are provided intentionally!** This assignment is designed to encourage you to develop essential programming research skills.

**When you get stuck, GOOGLE IT!** This is a critical skill for any developer. Examples of effective searches:

- `"C# BFS algorithm graph implementation"`
- `"Dijkstra's algorithm C# PriorityQueue example"`
- `"How to reconstruct path from parent map BFS"`
- `"C# Dictionary adjacency list graph"`
- `"Graph pathfinding algorithms comparison BFS vs Dijkstra"`
- `"C# PriorityQueue min heap usage"`
- `"How to detect if graph is connected C#"`

**Remember**: Stack Overflow, Microsoft Docs, and C# documentation are your friends. Learning to find solutions independently is just as important as implementing them!

## 🎓 Real-World Applications

This assignment teaches concepts used in:

- **Flight Booking Systems** (Google Flights, Kayak, Expedia route optimization)
- **Navigation Apps** (Google Maps, Waze shortest path algorithms)
- **Social Networks** (LinkedIn connections, Facebook friend suggestions)
- **Network Routing** (Internet packet routing, BGP protocols)
- **Supply Chain** (Logistics optimization, delivery route planning)
- **Game Development** (Pathfinding AI, navigation meshes)

---

## 📊 CSV File Format

Your `flights.csv` follows this structure:

```csv
Origin,Destination,Airline,Duration,Cost
SEA,SFO,Alaska,120,150.00
SEA,LAX,Delta,150,180.00
SFO,LAX,United,90,120.00
```

**Columns:**

- **Origin**: 3-letter airport code (e.g., SEA, LAX, JFK)
- **Destination**: 3-letter airport code
- **Airline**: Airline name (string)
- **Duration**: Flight duration in minutes (int)
- **Cost**: Flight price in dollars (decimal)

---

**Remember**: This assignment builds on Lab 10 graph concepts to solve real-world route optimization problems. Focus on understanding how BFS and Dijkstra's algorithms enable efficient pathfinding in network structures!

**Good luck building your flight route navigator! ✈️🗺️**
