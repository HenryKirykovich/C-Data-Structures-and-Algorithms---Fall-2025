using System;
using Week3ArraysSorting.PartB;

namespace Week3ArraysSorting
{
	internal static class Program
	{
		// Entry point with a simple main menu and replay support
		private static void Main()
		{
			while (true)
			{
				Console.Clear();
				Console.WriteLine("=== Week 3: Arrays & Sorting ===\n");
				Console.WriteLine("Part A:");
				Console.WriteLine("1) Play Tic-Tac-Toe");
				Console.WriteLine("2) Rules (Tic-Tac-Toe)");
				Console.WriteLine("\nPart B:");
				Console.WriteLine("4) Book Catalog (Recursive Sort + Index)");
				Console.WriteLine("\n0) Quit");
				Console.Write("\nChoose: ");

				var choice = Console.ReadLine()?.Trim();
				if (choice == "1")
				{
					PlayGame();
				}
				else if (choice == "2")
				{
					ShowRules();
				}
				else if (choice == "4")
				{
					RunBookCatalog();
				}
				else if (choice == "0" || string.Equals(choice, "q", StringComparison.OrdinalIgnoreCase))
				{
					return;
				}
			}
		}

		// Core game loop
		private static void PlayGame()
		{
			char[,] board = CreateEmptyBoard();
			char current = 'X';

			while (true)
			{
				Console.Clear();
				RenderBoard(board);
				Console.WriteLine($"\nTurn: {current} (enter row and column 1-3). Type 'q' to return to menu.");

				if (!PromptMove(board, current))
				{
					// user chose to quit to menu
					return;
				}

				// Check end conditions
				if (HasWon(board, current))
				{
					Console.Clear();
					RenderBoard(board);
					Console.WriteLine($"\n{current} wins!\n");
					WaitForKey("Press any key to return to menu...");
					return;
				}
				if (IsDraw(board))
				{
					Console.Clear();
					RenderBoard(board);
					Console.WriteLine("\nDraw! No empty cells left.\n");
					WaitForKey("Press any key to return to menu...");
					return;
				}

				// Switch player
				current = (current == 'X') ? 'O' : 'X'; // Toggle between X and O
			}
		}

		// Initialize a 3x3 board filled with spaces
		private static char[,] CreateEmptyBoard()
		{
			var board = new char[3, 3];
			for (int r = 0; r < 3; r++)
			{
				for (int c = 0; c < 3; c++)
				{
					board[r, c] = ' ';
				}
			}
			return board;
		}

		// Render the board with row/col headers
		private static void RenderBoard(char[,] board)
		{
			Console.WriteLine("    1   2   3");
			Console.WriteLine("  +---+---+---+");
			for (int r = 0; r < 3; r++)
			{
				Console.Write($"{r + 1} | ");
				for (int c = 0; c < 3; c++)
				{
					Console.Write(board[r, c]);
					Console.Write(" | ");
				}
				Console.WriteLine();
				Console.WriteLine("  +---+---+---+");
			}
		}

		// Prompt and validate a move; returns false if user cancels ('q')
		private static bool PromptMove(char[,] board, char player)
		{
			while (true)
			{
				Console.Write("Row (1-3): ");
				var rowInput = Console.ReadLine();
				if (IsQuit(rowInput)) return false;
				Console.Write("Col (1-3): ");
				var colInput = Console.ReadLine();
				if (IsQuit(colInput)) return false;

				if (!int.TryParse(rowInput, out int row) || !int.TryParse(colInput, out int col))
				{
					PrintInvalid("Please enter numbers 1-3.");
					continue;
				}
				if (row < 1 || row > 3 || col < 1 || col > 3)
				{
					PrintInvalid("Row/Col must be between 1 and 3.");
					continue;
				}
				int r = row - 1, c = col - 1;
				if (board[r, c] != ' ')
				{
					PrintInvalid("That cell is already taken.");
					continue;
				}

				board[r, c] = player;
				return true;
			}
		}

		private static bool HasWon(char[,] board, char p)
		{
			// Rows and columns
			for (int i = 0; i < 3; i++)
			{
				if (board[i, 0] == p && board[i, 1] == p && board[i, 2] == p) return true; // row i
				if (board[0, i] == p && board[1, i] == p && board[2, i] == p) return true; // col i
			}
			// Diagonals
			if (board[0, 0] == p && board[1, 1] == p && board[2, 2] == p) return true;
			if (board[0, 2] == p && board[1, 1] == p && board[2, 0] == p) return true;
			return false;
		}

		private static bool IsDraw(char[,] board)
		{
			for (int r = 0; r < 3; r++)
				for (int c = 0; c < 3; c++)
					if (board[r, c] == ' ') return false;
			return true;
		}

		private static void ShowRules()
		{
			Console.Clear();
			Console.WriteLine("Rules — Tic-Tac-Toe (3x3)\n");
			Console.WriteLine("- Two players take turns placing X and O on a 3×3 grid.");
			Console.WriteLine("- X goes first. Enter row and column numbers (1-3).");
			Console.WriteLine("- A player wins by getting three in a row — horizontally, vertically, or diagonally.");
			Console.WriteLine("- If all 9 cells are filled with no winner, the game is a draw.");
			Console.WriteLine("\nThis program uses a C# rectangular array: char[3,3] to store the board state.");
			Console.WriteLine("Rendering reads that array and prints a grid; input writes X/O into empty cells.");
			Console.WriteLine("Win/Draw checks scan rows, columns, and diagonals in the array.");
			Console.WriteLine();
			WaitForKey("Press any key to return to menu...");
		}

		private static bool IsQuit(string? s)
			=> string.Equals(s?.Trim(), "q", StringComparison.OrdinalIgnoreCase);

		private static void PrintInvalid(string message) // Print invalid input message in yellow
		{
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.WriteLine($"Invalid: {message}\n");
			Console.ResetColor();
		}

		private static void WaitForKey(string prompt) // Wait for user input
		{
			Console.WriteLine(prompt);
			Console.ReadKey(intercept: true);
		}

		// ========== Part B: Book Catalog ==========
		private static void RunBookCatalog()
		{
			var catalog = new BookCatalog();
			string path = "books.txt";

			Console.Clear();
			Console.WriteLine("=== Part B: Book Catalog ===\n");
			Console.WriteLine($"Loading books from {path}...");

			try
			{
				catalog.Load(path);
				Console.WriteLine($"Loaded {catalog.Count} books. Sorting...");
				catalog.Sort();
				Console.WriteLine("Building index...");
				catalog.BuildIndex();
				Console.WriteLine("Ready!\n");
				WaitForKey("Press any key to start lookup...");

				// Lookup loop
				while (true)
				{
					Console.Clear();
					Console.WriteLine("=== Book Lookup ===");
					Console.Write("Enter a book title (or 'exit'): ");
					string? query = Console.ReadLine()?.Trim();
					if (string.IsNullOrEmpty(query) || string.Equals(query, "exit", StringComparison.OrdinalIgnoreCase))
					{
						return;
					}

					catalog.Lookup(query);
					Console.WriteLine();
					WaitForKey("Press any key to continue...");
				}
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine($"Error: {ex.Message}");
				Console.ResetColor();
				WaitForKey("\nPress any key to return to menu...");
			}
		}
	}
}

// End of Program
