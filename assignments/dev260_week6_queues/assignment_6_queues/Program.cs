namespace Assignment6
{
    /// <summary>
    /// Entry point for the Game Matchmaking System
    /// 
    /// INSTRUCTOR NOTE: This is the main entry point for your application.
    /// While it looks simple, it demonstrates several important programming concepts
    /// that you should understand as you work on this assignment.
    /// </summary>
    class Program
    {
        // INSTRUCTOR NOTE: The Main method is where your program starts execution.
        // The 'static' keyword means this method belongs to the class itself,
        // not to any particular instance of the class. This is required for entry points.
        static void Main(string[] args)
        {
            // INSTRUCTOR NOTE: We use a try-catch block here for "top-level" error handling.
            // This is a safety net that catches any unhandled exceptions that might
            // bubble up from deeper in the application.
            try
            {
                // Create the main application controller
                // INSTRUCTOR NOTE: GameNavigator is our "controller" class that manages
                // the user interface and coordinates between different parts of the system.
                // This follows the Single Responsibility Principle - Program.cs just
                // starts the app, GameNavigator handles the UI and flow.
                // If the program is started with "auto" argument, run a non-interactive
                // smoke test that exercises the MatchmakingSystem methods. This is
                // helpful for automated verification and CI runs.
                if (args != null && args.Contains("auto"))
                {
                    RunAutoTest();
                }
                else
                {
                    var navigator = new GameNavigator();
                    // Start the interactive application loop
                    navigator.StartApplication();
                }
            }
                catch (Exception ex)
            {
                // INSTRUCTOR NOTE: If something goes catastrophically wrong anywhere
                // in the application, we'll end up here. This provides a graceful
                // way to show the error and exit, rather than crashing abruptly.

                Console.WriteLine($"❌ Fatal error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                if (!Console.IsInputRedirected)
                    Console.ReadKey();

                // INSTRUCTOR NOTE: After showing the error, the program will naturally
                // exit when this method ends. The user gets a chance to read the error
                // message before the console window closes.
            }
        }

        /// <summary>
        /// Simple non-interactive smoke test for basic matchmaking flows.
        /// This method is only executed when running with the "auto" argument.
        /// </summary>
        private static void RunAutoTest()
        {
            var mm = new MatchmakingSystem();

            // Create players
            var a = mm.CreatePlayer("P1", 5, GameMode.Casual);
            var b = mm.CreatePlayer("P2", 6, GameMode.Ranked);
            var c = mm.CreatePlayer("P3", 7, GameMode.Ranked);
            var d = mm.CreatePlayer("P4", 2, GameMode.QuickPlay);
            var e = mm.CreatePlayer("P5", 4, GameMode.QuickPlay);

            // Add to queues
            mm.AddToQueue(a, GameMode.Casual);
            mm.AddToQueue(b, GameMode.Ranked);
            mm.AddToQueue(c, GameMode.Ranked);
            mm.AddToQueue(d, GameMode.QuickPlay);
            mm.AddToQueue(e, GameMode.QuickPlay);

            // Process matches for each mode
            foreach (var mode in new[] { GameMode.Casual, GameMode.Ranked, GameMode.QuickPlay })
            {
                Console.WriteLine($"\n--- Processing {mode} ---");
                while (true)
                {
                    var match = mm.TryCreateMatch(mode);
                    if (match == null) break;
                    mm.ProcessMatch(match);
                }
            }

            Console.WriteLine($"\nAuto-test complete. Matches played: {mm.GetMatchHistory().Count}");
        }
    }
}