# Assignment 9: BST File System Navigator

## 📚 Overview

Build a **BST File System Navigator** that demonstrates the power of Binary Search Trees for efficient file organization, searching, and management! This assignment applies the BST concepts from Lab 9 to create a real-world application that manages files and directories, performs lightning-fast searches, and provides interactive file system analysis with O(log n) performance.

## 🎯 Learning Objectives

By completing this assignment, you will:

- **Master Binary Search Tree operations** for hierarchical data storage and fast file lookups
- **Apply recursive tree algorithms** for file system navigation and management
- **Implement efficient search and filtering** using O(log n) tree traversal techniques
- **Distinguish between file types and directory structures** using custom comparison logic
- **Build interactive file system applications** with professional menu systems
- **Practice performance optimization** with tree-based data organization vs linear search

## 📋 Requirements Overview

### Core Features (Required)

1. **File System Management**

   - Create files and directories in BST structure
   - Maintain proper BST ordering with custom file/directory comparison
   - Handle file metadata (size, extension, creation dates)

2. **Efficient Search Operations**

   - O(log n) file lookup by name
   - Filter files by extension (.txt, .cs, etc.)
   - Range-based file size queries
   - Find largest files in the system

3. **Tree-Based Analysis**

   - Calculate total storage usage through tree traversal
   - Display file system hierarchy visually
   - Generate comprehensive statistics and performance metrics

4. **Advanced Tree Operations**
   - Complex BST deletion maintaining tree structure
   - Custom comparison for mixed file/directory ordering
   - Recursive traversal with filtering and aggregation

### Stretch Features (Optional - Extra Credit)

**Choose ONE for extra credit (5 points):**

1. **File Pattern Matching System**

   - Implement wildcard search (\*.txt, file?.cs)
   - Regular expression-based file finding
   - Advanced pattern matching with multiple criteria

2. **Directory Size Calculation**
   - Recursive directory size computation
   - Hierarchical storage analysis
   - Directory tree depth and breadth metrics

## 🔧 Technical Specifications

### Core Classes

The assignment includes these key components:

- **`FileSystemBST`** - Main class with BST operations and file management (you implement 8 core methods + helper methods)
- **`FileSystemNavigator`** - Interactive menu system and user interface (provided)
- **`FileNode`** - File/directory data model with metadata (provided)
- **`TreeNode`** - BST node structure containing FileNode data (provided)

### Data Structures

```csharp
// Core file system storage with custom ordering
private TreeNode? root;

// File system collections for analysis
List<FileNode> allFiles = new List<FileNode>();        // All files during traversal
HashSet<string> uniqueExtensions = new HashSet<string>(); // File type tracking
Dictionary<string, int> extensionCounts = new Dictionary<string, int>(); // Analytics
```

### Key Operations

1. **Create Files/Directories**: Insert into BST with proper ordering
2. **Search Files**: O(log n) lookup by name with recursive navigation
3. **Filter Operations**: Tree traversal with extension/size/date filtering
4. **Aggregate Analysis**: Calculate totals and statistics through traversal
5. **Complex Deletion**: Remove nodes while maintaining BST structure

## 🎯 Implementation Requirements

You will need to implement **8 key methods** in the `FileSystemBST` class:

### File Management Methods

- `CreateFile(string path, long size)` - Insert files into BST with metadata
- `CreateDirectory(string path)` - Insert directories with proper ordering
- `DeleteItem(string fileName)` - Complex BST deletion with structure maintenance

### Search & Query Methods

- `FindFile(string fileName)` - O(log n) file lookup with recursive search
- `FindFilesByExtension(string extension)` - Filter files by type during traversal
- `FindFilesBySize(long minSize, long maxSize)` - Range queries with size bounds

### Analysis & Reporting Methods

- `FindLargestFiles(int count)` - Top-N analysis combining traversal + sorting
- `CalculateTotalSize()` - Aggregate calculation through recursive tree traversal

_All UI components, menu system, and data models are provided - focus on these BST operations!_

## 📝 Detailed Requirements

### 1. File System Creation Logic

- **BST Insertion**: Use recursive insertion maintaining file/directory ordering
- **Path Processing**: Extract file names and validate path format
- **Metadata Handling**: Set file size, extension, creation dates properly
- **Custom Comparison**: Directories before files, then alphabetical ordering

### 2. Search and Navigation

- **Recursive Search**: Implement O(log n) file lookup using BST navigation
- **Filtering**: Tree traversal with condition checking for extensions/sizes
- **Range Queries**: Find files within size bounds using efficient traversal
- **Performance**: Leverage BST structure for fast operations vs linear search

### 3. File System Analysis

- **Tree Traversal**: In-order traversal collecting files matching criteria
- **Aggregation**: Recursive calculation of total sizes and statistics
- **Top-N Queries**: Combine traversal with sorting for largest file analysis
- **Complex Deletion**: Handle all three BST deletion cases properly

## 🧪 Testing Requirements

Your application must handle these scenarios:

### Basic Functionality Tests

1. Create files and directories with proper BST ordering
2. Search for files by exact name with case-insensitive matching
3. Filter files by extension returning sorted results
4. Calculate total file system size accurately

### File System Operations Tests

1. **Path Handling**: "/documents/file.txt" vs "file.txt" should work correctly
2. **Case Sensitivity**: "README.txt" and "readme.TXT" should be treated consistently
3. **File Types**: Mix of files (.txt, .cs, .jpg) and directories
4. **Size Analysis**: Range queries and largest file identification

### Edge Case Tests

1. Try to search for files in empty file system
2. Create files with duplicate names (should handle gracefully)
3. Delete non-existent files and verify tree structure maintained
4. Process very large file sizes and verify calculations

## 🎯 Grading Rubric

### Core Implementation (90 points)

| Component                    | Points | Requirements                                                              |
| ---------------------------- | ------ | ------------------------------------------------------------------------- |
| **File Creation Operations** | 25     | CreateFile and CreateDirectory with proper BST insertion, path handling   |
| **Search Operations**        | 25     | FindFile, FindFilesByExtension, FindFilesBySize with efficient algorithms |
| **Analysis Operations**      | 20     | CalculateTotalSize and FindLargestFiles with correct tree traversal       |
| **Complex Operations**       | 20     | DeleteItem with all three BST deletion cases handled properly             |

### Code Quality and Documentation (15 points)

| Aspect                     | Points | Requirements                                                |
| -------------------------- | ------ | ----------------------------------------------------------- |
| **Implementation Quality** | 5      | Clean recursive implementations, proper BST algorithm usage |
| **Error Handling**         | 5      | Edge cases, null checks, meaningful error handling          |
| **Documentation**          | 5      | Clear code comments explaining BST operations and logic     |

### Stretch Features (5 points - Extra Credit)

| Feature                          | Points | Requirements                                              |
| -------------------------------- | ------ | --------------------------------------------------------- |
| **Choose ONE of the following:** | 5      | Complete implementation of any one stretch feature option |
| - File Pattern Matching          |        | Wildcard support, regex patterns, advanced search         |
| - Directory Size Analysis        |        | Recursive directory calculations, hierarchical metrics    |

**Total: 110 points (115 with extra credit)**

## 📚 Implementation Guide

### Phase 1: Foundation Setup

1. Review the provided `FileSystemBST` class structure with TODO comments
2. Understand the custom file/directory comparison logic in `CompareFileNodes`
3. Review the provided `BST_STRUCTURE_GUIDE.md` file for a better understanding of the file system tree structure you will be building
4. Implement basic file creation with proper BST insertion

### Phase 2: Search and Navigation Core

1. Implement recursive search algorithms for file lookup
2. Add extension-based filtering using tree traversal
3. Implement size-range queries with efficient filtering
4. Test with provided sample data and menu system

### Phase 3: Analysis and Advanced Operations

1. Implement tree traversal for total size calculation
2. Add largest files analysis combining traversal + sorting
3. Implement complex BST deletion with all three cases
4. Test all interactive menu commands thoroughly

### Phase 4: Polish & Testing

1. Add comprehensive error handling for edge cases
2. Test with various file scenarios and invalid inputs
3. Verify BST structure maintenance after operations
4. Consider implementing one stretch feature for extra credit

## 💡 Tips for Success

### Understanding BST vs Linear Search Benefits

- **O(log n) Operations**: File searches are much faster than scanning lists
- **Automatic Organization**: Files stay sorted without manual sorting
- **Hierarchical Structure**: Natural fit for file system organization

### Common Pitfalls to Avoid

1. **Inconsistent file name comparison** between insertion and search
2. **Breaking BST property** during deletion operations
3. **Not handling case sensitivity** properly for file names
4. **Mixing up file vs directory** ordering in custom comparison

### Testing Strategy

- Start with simple file creation and search to verify basic BST operations
- Test edge cases like empty trees and duplicate names
- Use the tree display feature to visualize BST structure
- Verify file counts and sizes match expected values manually

## 📅 Submission Requirements

### What to Submit

1. **All source code files** (.cs files including your completed FileSystemBST.cs)
2. **Project file** (FileSystemNavigator.csproj)
3. **STUDY_NOTES.md documentation** with your implementation notes and testing results

### Submission Format

- Submit link to your GitHub repository
- Code should be in `assignments/assignment_9_trees` directory
- Include clear commit messages showing your development process

## 🚀 Getting Started

1. **Review Lab 9 concepts** - This assignment builds directly on BST patterns from the employee management lab
2. **Examine the provided structure** - Understand the FileSystemBST class layout and TODO methods
3. **Start with CreateFile and CreateDirectory** - These are the foundation for all other operations
4. **Test incrementally** - Verify each TODO method works using the interactive menu before moving on
5. **Use the tree display** - Visualize your BST structure to verify proper ordering

## 🔍 Research and Problem Solving

**No external resources or links are provided intentionally!** This assignment is designed to encourage you to develop essential programming research skills.

**When you get stuck, GOOGLE IT!** This is a critical skill for any developer. Examples of effective searches:

- `"C# Binary Search Tree recursive insertion"`
- `"BST deletion three cases C# implementation"`
- `"C# tree traversal with filtering LINQ"`
- `"How to extract file extension C# Path.GetExtension"`
- `"Custom IComparer C# for complex objects"`
- `"C# recursive tree algorithms base case"`

**Remember**: Stack Overflow, Microsoft Docs, and C# documentation are your friends. Learning to find solutions independently is just as important as implementing them!

## 🎓 Real-World Applications

This assignment teaches concepts used in:

- **Operating Systems** (Windows File Explorer, macOS Finder file organization)
- **Database Systems** (B-tree indexing, query optimization in SQL Server/MySQL)
- **Version Control** (Git's tree-based file tracking and diff algorithms)
- **Search Engines** (File indexing, content organization at Google/Bing)
- **Cloud Storage** (Dropbox, Google Drive file synchronization and organization)
- **Backup Software** (Efficient file scanning, deduplication, incremental backups)

---

**Remember**: This assignment is about applying Lab 9 BST concepts to solve a practical file management problem. Focus on understanding how tree algorithms make file operations both efficient and elegant!

**Good luck building your file system navigator! 🗂️✨**
