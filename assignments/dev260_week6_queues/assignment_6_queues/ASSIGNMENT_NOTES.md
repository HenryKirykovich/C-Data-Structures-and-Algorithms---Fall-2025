## Assignment 6 — Implementation Notes

Name: Henry (implementation and test run performed)

Summary
-------
This folder contains a working Game Matchmaking System that implements the six required methods in `MatchmakingSystem.cs` and a small non-interactive smoke test runner in `Program.cs` (invoked with the `-- auto` argument). The implementation follows the assignment requirements: three queues (Casual, Ranked, QuickPlay), skill-based matching for Ranked, hybrid behavior for QuickPlay, and FIFO for Casual. I also added small I/O guards so the program runs safely in automated contexts.

What I implemented
------------------
- `AddToQueue(Player player, GameMode mode)` — validates and enqueues a player, calls `player.JoinQueue()` and prints a confirmation.
- `TryCreateMatch(GameMode mode)` — returns a `Match` when two compatible players are found:
	- Casual: strict FIFO — dequeue two players.
	- Ranked: search the queue for the earliest pair with skill difference ≤ 2, remove them and return a match.
	- QuickPlay: try Ranked-style pairing first, otherwise if queue size > 4 use FIFO to prioritize speed.
- `ProcessMatch(Match match)` — runs `match.SimulateOutcome()`, updates `matchHistory`, increments counters and prints match details.
- `DisplayQueueStatus()` — prints all three queues with positions and wait times using `Player.GetQueueTime()`.
- `DisplayPlayerStats(Player player)` — prints `ToDetailedString()` plus queue status and last 3 matches for the player.
- `GetQueueEstimate(GameMode mode)` — returns a short status string (`No wait`, `Short wait`, `Long wait`) with Ranked considering whether any compatible pair exists.

Additional small changes
------------------------
- Added a small non-interactive test runner in `Program.cs` which runs when the program is started with `-- auto`. This creates demo players, enqueues them and processes matches so you can verify behavior without interactive input.
- Protected `Console.ReadKey()` calls with `if (!Console.IsInputRedirected) Console.ReadKey();` so the app runs cleanly in automated/non-interactive environments (CI or `-- auto`).

How to run
----------
- Interactive (manual):
```powershell
dotnet run --project "assignments/dev260_week6_queues/assignment_6_queues/Assignment6.csproj"
```

- Automatic smoke test (no prompts):
```powershell
dotnet run --project "assignments/dev260_week6_queues/assignment_6_queues/Assignment6.csproj" -- auto
```

What I tested
-------------
I executed manual and automatic checks to verify the following scenarios:

- Casual FIFO: added players to Casual queue and verified the first two were dequeued to create a match.
- Ranked ±2 matching: verified that only players within ±2 skill matched. If no suitable pair exists `TryCreateMatch` returns null.
- QuickPlay hybrid: verified that QuickPlay prefers skill-matching (like Ranked) but when queue sizes grow it falls back to FIFO (for speed).
- Edge cases: empty queues and single-player queues produce no match; `GetQueueEstimate` returns expected strings for 0/1/2+ players.
- Automated run (`-- auto`) produced matches and printed `Match` details (this was used as the smoke test).

Known issues & remarks
----------------------
- Randomness: `Match.SimulateOutcome()` uses `Random`, so match winners vary between runs — this is expected behaviour.
- No persistent storage: this app keeps data in memory only. If you want persistence, we can add simple JSON save/load.
- Extra credit: I did not implement a stretch feature (team formation / recent opponent cooldown / advanced analytics). If you'd like extra credit, I can implement one option (recommendation: Avoid Recent Opponents or Advanced Queue Analytics).

Next steps / improvements
-------------------------
- Add unit tests for `MatchmakingSystem` logic (TryCreateMatch for each mode).
- Implement one extra credit feature — I recommend "Avoid Recent Opponents" as it integrates cleanly.
- Add a `.gitignore` for the assignment/project folder (if not already present) and remove `/bin` and `/obj` from tracking.

Time log
--------
- Implement core methods and tests: ~1.5–2 hours
- Debugging I/O / ReadKey behavior: ~0.25 hours
- Writing notes and push: ~0.25 hours

Conclusion
----------
The assignment implementation is complete and pushed to the branch `assignment-6-matchmaking`. The app can be run interactively or in automated mode with the `-- auto` flag for quick verification. If you want, I can now add `.gitignore`, remove build artifacts from the repository index, and/or open a PR on your behalf.
