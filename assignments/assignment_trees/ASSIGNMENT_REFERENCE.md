# Assignment 9: BST File System Navigator 🗂️

## Quick Reference for Development

**Due Date:** November 28th by 11:59 PM  
**Total Points:** 105 (110 with extra credit)

---

## 🎯 Learning Objectives

- **Master Binary Search Tree operations** for hierarchical data storage and fast file lookups
- **Apply recursive tree algorithms** for file system navigation and management
- **Implement efficient search and filtering** using O(log n) tree traversal techniques
- **Distinguish between file types and directory structures** using custom comparison logic
- **Build interactive file system applications** with professional menu systems
- **Practice performance optimization** with tree-based data organization vs linear search

---

## 📋 Core Requirements

### 1. File System Management System

- Create files and directories in BST structure with proper ordering
- Maintain custom file/directory comparison logic (directories before files)
- Handle file metadata (size, extension, creation dates)

### 2. Efficient Search Operations Engine

- O(log n) file lookup by name using recursive BST navigation
- Filter files by extension (.txt, .cs, etc.) through tree traversal
- Range-based file size queries with efficient bounds checking
- Find largest files using traversal combined with sorting

### 3. Tree-Based Analysis Tools

- Calculate total storage usage through recursive tree traversal
- Display file system hierarchy visually with proper formatting
- Generate comprehensive statistics and performance metrics

### 4. Advanced BST Operations

- Complex BST deletion maintaining tree structure integrity
- Custom comparison for mixed file/directory ordering
- Recursive traversal with filtering and aggregation capabilities

---

## ⭐ Stretch Features (Extra Credit - Choose ONE)

**Pick ONE of these two options for extra credit (5 points):**

### Option 1: File Pattern Matching System

- Implement wildcard search (\*.txt, file?.cs) with pattern recognition
- Regular expression-based file finding with advanced criteria
- Advanced pattern matching with multiple search conditions

### Option 2: Directory Size Calculation

- Recursive directory size computation with hierarchical analysis
- Hierarchical storage analysis with depth and breadth metrics
- Directory tree structure analysis and optimization suggestions

---

## 🔧 Implementation Requirements

You will implement **8 key methods** in the `FileSystemBST` class:

### File Management Methods:

- `CreateFile(string path, long size)` - Insert files into BST with metadata and path processing
- `CreateDirectory(string path)` - Insert directories with proper BST ordering
- `DeleteItem(string fileName)` - Complex BST deletion with all three deletion cases

### Search & Query Methods:

- `FindFile(string fileName)` - O(log n) file lookup with recursive BST navigation
- `FindFilesByExtension(string extension)` - Filter files by type during tree traversal
- `FindFilesBySize(long minSize, long maxSize)` - Range queries with size bounds checking

### Analysis & Reporting Methods:

- `FindLargestFiles(int count)` - Top-N analysis combining tree traversal with sorting
- `CalculateTotalSize()` - Aggregate calculation through recursive tree traversal

_All UI components, menu system, data models, and tree display functionality are provided - focus on these BST operations!_

---

## 📊 Grading Breakdown

| Component                    | Points | Requirements                                                              |
| ---------------------------- | ------ | ------------------------------------------------------------------------- |
| **File Creation Operations** | 25     | CreateFile and CreateDirectory with proper BST insertion, path handling   |
| **Search Operations**        | 25     | FindFile, FindFilesByExtension, FindFilesBySize with efficient algorithms |
| **Analysis Operations**      | 20     | CalculateTotalSize and FindLargestFiles with correct tree traversal       |
| **Complex Operations**       | 20     | DeleteItem with all three BST deletion cases handled properly             |
| **Code Quality**             | 5      | Clean recursive implementations, proper BST algorithm usage               |
| **Error Handling**           | 5      | File I/O exceptions, edge cases, meaningful error handling                |
| **Documentation**            | 5      | Complete STUDY_NOTES.md with thoughtful implementation reflection         |
| **Extra Credit**             | 5      | Complete implementation of ONE stretch feature option                     |

**Total: 105 points (110 with extra credit)**

---

## 🔍 Research and Problem Solving

**No external resources or links are provided intentionally!** This assignment is designed to encourage you to develop essential programming research skills.

**When you get stuck, GOOGLE IT!** This is a critical skill for any developer. Examples of effective searches:

- `"C# Binary Search Tree recursive insertion algorithm"`
- `"BST deletion three cases C# implementation examples"`
- `"C# tree traversal with filtering LINQ techniques"`
- `"How to extract file extension C# Path.GetExtension method"`
- `"Custom IComparer C# for complex object sorting"`
- `"C# recursive tree algorithms base case patterns"`
- `"BST find operation recursive C# implementation"`
- `"Tree traversal in-order with condition checking C#"`

**Remember:** Stack Overflow, Microsoft Docs, and C# documentation are your friends. Learning to find solutions independently is just as important as implementing them correctly!

---

## 💡 Tips for Success

### Understanding BST vs Linear Search Benefits

- **O(log n) Operations:** File searches are exponentially faster than scanning lists
- **Automatic Organization:** Files stay properly sorted without manual sorting operations
- **Hierarchical Structure:** Natural fit for file system organization and navigation

### Common Pitfalls to Avoid

- **Inconsistent file name comparison** between insertion and search operations
- **Breaking BST property** during complex deletion operations
- **Not handling case sensitivity** properly for realistic file name scenarios
- **Mixing up file vs directory** ordering in custom comparison logic

### Testing Strategy

- Start with simple file creation and search to verify basic BST operations
- Test edge cases like empty trees, duplicate names, and missing files
- Use the provided tree display feature to visualize BST structure integrity
- Verify file counts and sizes match expected values through manual verification

---

## 📅 Submission Requirements

### What to Submit

1. **All source code files** (.cs files including your completed FileSystemBST.cs)
2. **Project file** (FileSystemNavigator.csproj with proper configuration)
3. **Data files** (Any additional test files you create for comprehensive testing)
4. **STUDY_NOTES.md** with your implementation notes, challenges, and testing documentation

### Submission Format

- Submit link to your GitHub repository
- Code should be in `assignments/assignment_9_trees` directory
- Include clear commit messages showing your systematic development process

---

### File Processing Requirements

1. **Path Processing**: Extract file names from full paths using `Path.GetFileName()`
2. **Extension Handling**: Use `Path.GetExtension()` with proper normalization
3. **Size Validation**: Ensure file sizes are non-negative and reasonable
4. **Comparison Logic**: Use provided `CompareFileNodes` for consistent ordering

---

**Remember:** This assignment builds directly on Lab 9 BST concepts to solve practical file management problems. Focus on understanding how recursive tree algorithms make file operations both efficient and elegant!

**Good luck building your BST file system navigator! 🗂️✨**
