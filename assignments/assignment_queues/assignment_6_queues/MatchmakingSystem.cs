namespace Assignment6
{
    /// <summary>
    /// Main matchmaking system managing queues and matches
    /// Students implement the core methods in this class
    /// </summary>
    public class MatchmakingSystem
    {
        // Data structures for managing the matchmaking system
        private Queue<Player> casualQueue = new Queue<Player>();
        private Queue<Player> rankedQueue = new Queue<Player>();
        private Queue<Player> quickPlayQueue = new Queue<Player>();
        private List<Player> allPlayers = new List<Player>();
        private List<Match> matchHistory = new List<Match>();

        // Statistics tracking
        private int totalMatches = 0;
        private DateTime systemStartTime = DateTime.Now;

        /// <summary>
        /// Create a new player and add to the system
        /// </summary>
        public Player CreatePlayer(string username, int skillRating, GameMode preferredMode = GameMode.Casual)
        {
            // Check for duplicate usernames
            if (allPlayers.Any(p => p.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
            {
                throw new ArgumentException($"Player with username '{username}' already exists");
            }

            var player = new Player(username, skillRating, preferredMode);
            allPlayers.Add(player);
            return player;
        }

        /// <summary>
        /// Get all players in the system
        /// </summary>
        public List<Player> GetAllPlayers() => allPlayers.ToList();

        /// <summary>
        /// Get match history
        /// </summary>
        public List<Match> GetMatchHistory() => matchHistory.ToList();

        /// <summary>
        /// Get system statistics
        /// </summary>
        public string GetSystemStats()
        {
            var uptime = DateTime.Now - systemStartTime;
            var avgMatchQuality = matchHistory.Count > 0 
                ? matchHistory.Average(m => m.SkillDifference) 
                : 0;

            return $"""
                🎮 Matchmaking System Statistics
                ================================
                Total Players: {allPlayers.Count}
                Total Matches: {totalMatches}
                System Uptime: {uptime.ToString("hh\\:mm\\:ss")}
                
                Queue Status:
                - Casual: {casualQueue.Count} players
                - Ranked: {rankedQueue.Count} players  
                - QuickPlay: {quickPlayQueue.Count} players
                
                Match Quality:
                - Average Skill Difference: {avgMatchQuality:F1}
                - Recent Matches: {Math.Min(5, matchHistory.Count)}
                """;
        }

        // ============================================
        // STUDENT IMPLEMENTATION METHODS (TO DO)
        // ============================================

        /// <summary>
        /// TODO: Add a player to the appropriate queue based on game mode
        /// 
        /// Requirements:
        /// - Add player to correct queue (casualQueue, rankedQueue, or quickPlayQueue)
        /// - Call player.JoinQueue() to track queue time
        /// - Handle any validation needed
        /// </summary>
        public void AddToQueue(Player player, GameMode mode)
        {
            if (player == null)
                throw new ArgumentNullException(nameof(player));

            // If player is already in a queue, do not add again
            if (player.JoinedQueue != DateTime.MinValue)
            {
                Console.WriteLine($"Player {player.Username} is already in a queue.");
                return;
            }

            // Put player into the requested queue and mark join time
            switch (mode)
            {
                case GameMode.Casual:
                    casualQueue.Enqueue(player);
                    break;
                case GameMode.Ranked:
                    rankedQueue.Enqueue(player);
                    break;
                case GameMode.QuickPlay:
                    quickPlayQueue.Enqueue(player);
                    break;
                default:
                    throw new ArgumentException($"Unknown game mode: {mode}");
            }

            player.JoinQueue();
            Console.WriteLine($"{player.Username} joined {mode} queue.");
        }

        /// <summary>
        /// TODO: Try to create a match from the specified queue
        /// 
        /// Requirements:
        /// - Return null if not enough players (need at least 2)
        /// - For Casual: Any two players can match (simple FIFO)
        /// - For Ranked: Only players within ±2 skill levels can match
        /// - For QuickPlay: Prefer skill matching, but allow any match if queue > 4 players
        /// - Remove matched players from queue and call LeaveQueue() on them
        /// - Return new Match object if successful
        /// </summary>
        public Match? TryCreateMatch(GameMode mode)
        {
            var queue = GetQueueByMode(mode);

            // Need at least two players
            if (queue.Count < 2)
                return null;

            // Convert to list for flexible inspection/removal
            var players = queue.ToList();

            // Helper to rebuild queue after selecting players
            void RebuildQueueExcluding(params Player[] excluded)
            {
                queue.Clear();
                foreach (var p in players.Where(p => !excluded.Contains(p)))
                    queue.Enqueue(p);
            }

            // Mode-specific logic
            if (mode == GameMode.Casual)
            {
                // FIFO: just dequeue two players
                var p1 = queue.Dequeue();
                var p2 = queue.Dequeue();
                p1.LeaveQueue();
                p2.LeaveQueue();
                return new Match(p1, p2, mode);
            }

            if (mode == GameMode.Ranked)
            {
                // Find earliest pair within ±2 skill
                for (int i = 0; i < players.Count; i++)
                {
                    for (int j = i + 1; j < players.Count; j++)
                    {
                        if (CanMatchInRanked(players[i], players[j]))
                        {
                            var a = players[i];
                            var b = players[j];
                            RebuildQueueExcluding(a, b);
                            a.LeaveQueue();
                            b.LeaveQueue();
                            return new Match(a, b, mode);
                        }
                    }
                }

                // No suitable pair found
                return null;
            }

            // QuickPlay: try ranked-style match first, otherwise allow FIFO when queue is large
            if (mode == GameMode.QuickPlay)
            {
                // Try skill match first (prefer balanced matches)
                for (int i = 0; i < players.Count; i++)
                {
                    for (int j = i + 1; j < players.Count; j++)
                    {
                        if (CanMatchInRanked(players[i], players[j]))
                        {
                            var a = players[i];
                            var b = players[j];
                            RebuildQueueExcluding(a, b);
                            a.LeaveQueue();
                            b.LeaveQueue();
                            return new Match(a, b, mode);
                        }
                    }
                }

                // If queue is large, prefer speed over perfect match
                if (players.Count > 4)
                {
                    var p1 = queue.Dequeue();
                    var p2 = queue.Dequeue();
                    p1.LeaveQueue();
                    p2.LeaveQueue();
                    return new Match(p1, p2, mode);
                }

                // Otherwise, no match yet
                return null;
            }

            return null;
        }

        /// <summary>
        /// TODO: Process a match by simulating outcome and updating statistics
        /// 
        /// Requirements:
        /// - Call match.SimulateOutcome() to determine winner
        /// - Add match to matchHistory
        /// - Increment totalMatches counter
        /// - Display match results to console
        /// </summary>
        public void ProcessMatch(Match match)
        {
            if (match == null)
            {
                Console.WriteLine("No match to process.");
                return;
            }

            // Run the simulation which updates player stats
            match.SimulateOutcome();

            // Record and increment stats
            matchHistory.Add(match);
            totalMatches++;

            // Display result
            Console.WriteLine(match.ToDetailedString());
        }

        /// <summary>
        /// TODO: Display current status of all queues with formatting
        /// 
        /// Requirements:
        /// - Show header "Current Queue Status"
        /// - For each queue (Casual, Ranked, QuickPlay):
        ///   - Show queue name and player count
        ///   - List players with position numbers and queue times
        ///   - Handle empty queues gracefully
        /// - Use proper formatting and emojis for readability
        /// </summary>
        public void DisplayQueueStatus()
        {
            Console.WriteLine("\n📋 Current Queue Status");
            Console.WriteLine("=================================");

            void DumpQueue(string title, Queue<Player> q)
            {
                Console.WriteLine($"\n{title} - {q.Count} players");
                if (q.Count == 0)
                {
                    Console.WriteLine("  (empty)");
                    return;
                }

                int pos = 1;
                foreach (var p in q)
                {
                    Console.WriteLine($"  {pos,2}. {p}  ⏱ {p.GetQueueTime()}");
                    pos++;
                }
            }

            DumpQueue("Casual", casualQueue);
            DumpQueue("Ranked", rankedQueue);
            DumpQueue("QuickPlay", quickPlayQueue);
            Console.WriteLine();
        }

        /// <summary>
        /// TODO: Display detailed statistics for a specific player
        /// 
        /// Requirements:
        /// - Use player.ToDetailedString() for basic info
        /// - Add queue status (in queue, estimated wait time)
        /// - Show recent match history for this player (last 3 matches)
        /// - Handle case where player has no matches
        /// </summary>
        public void DisplayPlayerStats(Player player)
        {
            if (player == null)
            {
                Console.WriteLine("Player not found.");
                return;
            }

            Console.WriteLine("\n🔎 Player Details");
            Console.WriteLine("------------------");
            Console.WriteLine(player.ToDetailedString());

            // Queue status
            var inQueue = player.JoinedQueue != DateTime.MinValue;
            Console.WriteLine($"In Queue: {(inQueue ? "Yes" : "No")}");
            if (inQueue)
            {
                Console.WriteLine($"  - Waiting: {player.GetQueueTime()}");
                Console.WriteLine($"  - Estimated Wait: {GetQueueEstimate(player.PreferredMode)}");
            }

            // Recent matches (last 3)
            var recent = matchHistory.Where(m => m.Player1 == player || m.Player2 == player)
                                     .OrderByDescending(m => m.MatchTime)
                                     .Take(3)
                                     .ToList();

            Console.WriteLine("\nRecent Matches:");
            if (!recent.Any())
            {
                Console.WriteLine("  No match history yet.");
            }
            else
            {
                foreach (var m in recent)
                    Console.WriteLine("  - " + m.GetSummary());
            }
        }

        /// <summary>
        /// TODO: Calculate estimated wait time for a queue
        /// 
        /// Requirements:
        /// - Return "No wait" if queue has 2+ players
        /// - Return "Short wait" if queue has 1 player
        /// - Return "Long wait" if queue is empty
        /// - For Ranked: Consider skill distribution (harder to match = longer wait)
        /// </summary>
        public string GetQueueEstimate(GameMode mode)
        {
            var q = GetQueueByMode(mode);

            // Basic rules
            if (q.Count >= 2)
            {
                if (mode == GameMode.Ranked)
                {
                    // If no pair can be matched within ±2, it's harder -> longer wait
                    var players = q.ToList();
                    for (int i = 0; i < players.Count; i++)
                    {
                        for (int j = i + 1; j < players.Count; j++)
                        {
                            if (CanMatchInRanked(players[i], players[j]))
                                return "No wait"; // at least one pair is matchable
                        }
                    }
                    return "Long wait"; // no compatible pairs found
                }

                return "No wait"; // enough players for immediate matching
            }

            if (q.Count == 1)
                return "Short wait";

            return "Long wait"; // empty queue
        }

        // ============================================
        // HELPER METHODS (PROVIDED)
        // ============================================

        /// <summary>
        /// Helper: Check if two players can match in Ranked mode (±2 skill levels)
        /// </summary>
        private bool CanMatchInRanked(Player player1, Player player2)
        {
            return Math.Abs(player1.SkillRating - player2.SkillRating) <= 2;
        }

        /// <summary>
        /// Helper: Remove player from all queues (useful for cleanup)
        /// </summary>
        private void RemoveFromAllQueues(Player player)
        {
            // Create temporary lists to avoid modifying collections during iteration
            var casualPlayers = casualQueue.ToList();
            var rankedPlayers = rankedQueue.ToList();
            var quickPlayPlayers = quickPlayQueue.ToList();

            // Clear and rebuild queues without the specified player
            casualQueue.Clear();
            foreach (var p in casualPlayers.Where(p => p != player))
                casualQueue.Enqueue(p);

            rankedQueue.Clear();
            foreach (var p in rankedPlayers.Where(p => p != player))
                rankedQueue.Enqueue(p);

            quickPlayQueue.Clear();
            foreach (var p in quickPlayPlayers.Where(p => p != player))
                quickPlayQueue.Enqueue(p);

            player.LeaveQueue();
        }

        /// <summary>
        /// Helper: Get queue by mode (useful for generic operations)
        /// </summary>
        private Queue<Player> GetQueueByMode(GameMode mode)
        {
            return mode switch
            {
                GameMode.Casual => casualQueue,
                GameMode.Ranked => rankedQueue,
                GameMode.QuickPlay => quickPlayQueue,
                _ => throw new ArgumentException($"Unknown game mode: {mode}")
            };
        }
    }
}