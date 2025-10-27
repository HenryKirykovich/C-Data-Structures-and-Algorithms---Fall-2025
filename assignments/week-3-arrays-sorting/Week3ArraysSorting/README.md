# Week 3 — Arrays & Sorting

## Part A: Board Game with Multi-Dimensional Arrays (Tic-Tac-Toe)

This project implements Tic-Tac-Toe (3×3) using a C# rectangular array `char[3,3]`.

### Rules and Controls
- Two players: `X` and `O`. `X` moves first.
- On your turn, enter Row and Column numbers (1–3) to place your mark.
- Win: three in a row horizontally, vertically, or diagonally.
- Draw: all 9 cells filled without a winner.
- Type `q` during move input to return to the main menu.

### How the Array is Used
- Board state is stored in a multi-dimensional array: `char[,] board = new char[3,3];`.
- Rendering iterates through rows/columns and prints the grid with headers.
- Input writes `X`/`O` into empty cells inside the array.
- Win detection scans rows, columns, and both diagonals of the array.
- Draw is detected when no `' '` spaces remain in the array.

---

## Part B: Book Catalog (Recursive Sort + Multi-Dimensional Index)

### Overview
Implements a book catalog with:
- **Recursive Quicksort** to sort book titles
- **2D array index** (`int[26,26]`) for fast lookup by first two letters
- **CLI search** with exact match and suggestions

### Algorithm Details

#### Quicksort (Recursive)
- **Pivot strategy**: Last element in range
- **Time complexity**: O(n log n) average, O(n²) worst case
- **Space complexity**: O(log n) stack depth (average)
- **Why quicksort**: In-place sorting, good cache locality, simple implementation

#### Multi-Dimensional Index
- **Structure**: Two 26×26 arrays (`_indexStart` and `_indexEnd`)
- Maps first two letters (A-Z) to start/end positions in sorted array
- **Build time**: O(n) single scan after sorting
- **Lookup time**: O(1) to find range, then O(log k) binary search within small slice k
- **Memory**: 26×26×2 integers = 2,704 integers (~10 KB) — very compact!

#### Normalization
- Convert to uppercase
- Trim whitespace
- Strip leading articles: "THE ", "A ", "AN "
- Original title preserved for display

### Usage
1. Place `books.txt` in the project folder (one title per line)
2. Run the app and select option 4
3. Enter book title to search
4. Get exact match or suggestions

---

## How to Run (PowerShell)
```powershell
# From repository root
cd "c:\Users\info\Desktop\C-Data-Structures-and-Algorithms---Fall-2025\assignments\week-3-arrays-sorting\Week3ArraysSorting"
dotnet run
```

Inside the app:
- 1 = Play Tic-Tac-Toe
- 2 = Rules (Part A)
- 4 = Book Catalog (Part B)
- 0 or q = Quit

