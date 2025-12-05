using System;
using System.Collections.Generic;

namespace Week3ArraysSorting.PartB
{
	/// <summary>
	/// Part B: Book Catalog with Recursive Quicksort and 2D Index
	/// Sorts book titles using quicksort and builds a multi-dimensional array index
	/// for fast lookup by first two letters.
	/// </summary>
	internal class BookCatalog
	{
		private readonly List<BookEntry> _books = new();
		private int[,] _indexStart = new int[26, 26];
		private int[,] _indexEnd = new int[26, 26];

		public int Count => _books.Count;

		/// <summary>
		/// Load books from a text file (one title per line)
		/// </summary>
		public void Load(string filePath)
		{
			if (!System.IO.File.Exists(filePath))
			{
				throw new System.IO.FileNotFoundException($"File not found: {filePath}");
			}

			var lines = System.IO.File.ReadAllLines(filePath);
			foreach (var line in lines)
			{
				string title = line.Trim();
				if (!string.IsNullOrEmpty(title))
				{
					_books.Add(new BookEntry(title));
				}
			}
		}

		/// <summary>
		/// Sort books using recursive Quicksort (O(n log n) average)
		/// Pivot strategy: last element in range
		/// </summary>
		public void Sort()
		{
			if (_books.Count > 0)
			{
				QuickSort(0, _books.Count - 1);
			}
		}

		private void QuickSort(int low, int high)
		{
			if (low < high)
			{
				int pivotIndex = Partition(low, high);
				QuickSort(low, pivotIndex - 1);  // Sort left partition
				QuickSort(pivotIndex + 1, high); // Sort right partition
			}
		}

		private int Partition(int low, int high)
		{
			// Choose last element as pivot
			string pivot = _books[high].Normalized;
			int i = low - 1;

			for (int j = low; j < high; j++)
			{
				if (string.Compare(_books[j].Normalized, pivot, StringComparison.Ordinal) <= 0)
				{
					i++;
					Swap(i, j);
				}
			}
			Swap(i + 1, high);
			return i + 1;
		}

		private void Swap(int i, int j)
		{
			var temp = _books[i];
			_books[i] = _books[j];
			_books[j] = temp;
		}

		/// <summary>
		/// Build 2D index for fast lookup by first two letters
		/// Time: O(n), Space: O(1) - fixed 26x26 arrays
		/// </summary>
		public void BuildIndex()
		{
			// Initialize all ranges to empty (-1 means no books with these letters)
			for (int i = 0; i < 26; i++)
			{
				for (int j = 0; j < 26; j++)
				{
					_indexStart[i, j] = -1;
					_indexEnd[i, j] = -1;
				}
			}

			// Scan sorted list and record boundaries for each [first, second] letter pair
			for (int idx = 0; idx < _books.Count; idx++)
			{
				string norm = _books[idx].Normalized;
				if (norm.Length == 0) continue;

				int first = LetterIndex(norm[0]);
				int second = norm.Length > 1 ? LetterIndex(norm[1]) : 0;

				if (first < 0 || second < 0) continue; // skip non-letters

				if (_indexStart[first, second] == -1)
				{
					_indexStart[first, second] = idx; // First book with these letters
				}
				_indexEnd[first, second] = idx + 1; // Exclusive end (always update)
			}
		}

		/// <summary>
		/// Lookup a book title with exact match and suggestions
		/// Time: O(1) to find range + O(log k) binary search in slice
		/// </summary>
		public void Lookup(string query)
		{
			string normQuery = Normalize(query);
			if (normQuery.Length == 0)
			{
				Console.WriteLine("Invalid query.");
				return;
			}

			int first = LetterIndex(normQuery[0]);
			int second = normQuery.Length > 1 ? LetterIndex(normQuery[1]) : 0;

			if (first < 0 || second < 0)
			{
				Console.WriteLine("Query must start with letters.");
				return;
			}

			int start = _indexStart[first, second];
			int end = _indexEnd[first, second];

			if (start == -1 || end == -1)
			{
				Console.WriteLine("No match found.");
				ShowSuggestions(normQuery, 0, _books.Count);
				return;
			}

			// Binary search in range [start, end)
			int foundIdx = BinarySearch(normQuery, start, end);
			if (foundIdx >= 0)
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"✓ Exact match: \"{_books[foundIdx].Original}\"");
				Console.ResetColor();
			}
			else
			{
				Console.WriteLine("No exact match.");
				ShowSuggestions(normQuery, start, end);
			}
		}

		private int BinarySearch(string normQuery, int start, int end)
		{
			int low = start, high = end - 1;
			while (low <= high)
			{
				int mid = low + (high - low) / 2;
				int cmp = string.Compare(_books[mid].Normalized, normQuery, StringComparison.Ordinal);
				if (cmp == 0) return mid;
				if (cmp < 0) low = mid + 1;
				else high = mid - 1;
			}
			return -1;
		}

		private void ShowSuggestions(string normQuery, int start, int end)
		{
			Console.WriteLine("Suggestions:");
			int count = 0;
			int prefixLen = Math.Min(2, normQuery.Length);
			string prefix = normQuery.Substring(0, prefixLen);

			for (int i = start; i < end && count < 5; i++)
			{
				if (_books[i].Normalized.StartsWith(prefix))
				{
					Console.WriteLine($"  - {_books[i].Original}");
					count++;
				}
			}
			if (count == 0)
			{
				Console.WriteLine("  (none found in this range)");
			}
		}

		/// <summary>
		/// Normalize title for sorting and lookup:
		/// - Trim whitespace
		/// - Convert to uppercase
		/// - Remove leading articles: THE, AN, A
		/// </summary>
		private static string Normalize(string title)
		{
			string s = title.Trim().ToUpperInvariant();
			// Strip leading articles
			if (s.StartsWith("THE ")) s = s.Substring(4);
			else if (s.StartsWith("AN ")) s = s.Substring(3);
			else if (s.StartsWith("A ")) s = s.Substring(2);
			return s.Trim();
		}

		/// <summary>
		/// Map letter A-Z to index 0-25, non-letters to 0
		/// </summary>
		private static int LetterIndex(char c)
		{
			if (c >= 'A' && c <= 'Z') return c - 'A';
			if (c >= 'a' && c <= 'z') return c - 'a';
			return 0; // treat non-letters as 'A'
		}

		/// <summary>
		/// Internal book entry storing both original and normalized title
		/// </summary>
		private class BookEntry
		{
			public string Original { get; }
			public string Normalized { get; }

			public BookEntry(string title)
			{
				Original = title;
				Normalized = Normalize(title);
			}
		}
	}
}
