# Assignment 8: Spell Checker & Vocabulary Explorer

## 📚 Overview

Build a **Simple Spell Checker & Vocabulary Explorer** that demonstrates the power of HashSet<T> for efficient text analysis! This assignment applies the HashSet concepts from Lab 8 to create a real-world application that processes text files, identifies misspelled words, and provides interactive vocabulary analysis with lightning-fast lookups.

## 🎯 Learning Objectives

By completing this assignment, you will:

- **Master HashSet<string> operations** for dictionary storage and fast word lookups
- **Apply text normalization techniques** for consistent case and punctuation handling
- **Implement efficient text analysis** using O(1) membership testing
- **Distinguish between total vs unique word counts** using different collection types
- **Build interactive console applications** with professional menu systems
- **Practice defensive programming** with proper input validation and error handling

## 📋 Requirements Overview

### Core Features (Required)

1. **Dictionary Management System**

   - Load dictionary from `dictionary.txt` into HashSet<string>
   - Apply consistent normalization (lowercase, trim whitespace)
   - Display dictionary size and loading confirmation

2. **Text Analysis Engine**

   - Load and tokenize text files with proper normalization
   - Track both total word count and unique word count
   - Categorize words into correctly spelled vs misspelled

3. **Interactive Spell Checking**

   - Check individual words for dictionary membership
   - Count word occurrences in analyzed text
   - Display comprehensive word information

4. **Vocabulary Explorer**
   - List all misspelled words with formatting
   - Show sample of unique words found in text
   - Display comprehensive statistics and performance metrics

### Stretch Features (Optional - Extra Credit - Choose ONE)

**Pick ONE of these three options for extra credit (5 points):**

1. **Vocabulary Suggestions System**

   - Implement basic spell correction suggestions using string distance
   - Find dictionary words that are similar to misspelled words
   - Display top 3 suggestions for each misspelled word

2. **Enhanced Analytics Dashboard**
   - Track word frequency distributions
   - Identify most common misspellings
   - Generate vocabulary complexity scores and reading level estimates

## 🔧 Technical Specifications

### Core Classes

The assignment includes these key components:

- **`SpellChecker`** - Main class with HashSet operations and text analysis (you implement 6 core methods)
- **`SpellCheckerNavigator`** - Interactive menu system and user interface (provided)
- **`Program`** - Entry point and application orchestration (provided)

### Data Structures

```csharp
// Core dictionary storage with case-insensitive comparison
HashSet<string> dictionary = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

// Text analysis collections
List<string> allWordsInText = new List<string>();           // Preserves order and duplicates
HashSet<string> uniqueWordsInText = new HashSet<string>(); // Automatic uniqueness
HashSet<string> correctlySpelledWords = new HashSet<string>();
HashSet<string> misspelledWords = new HashSet<string>();
```

### Key Operations

1. **Load Dictionary**: Read words from file into HashSet with normalization
2. **Analyze Text**: Tokenize file content and populate all analysis collections
3. **Categorize Words**: Partition unique words into correct vs misspelled using Contains()
4. **Check Individual Words**: Fast lookup with occurrence counting
5. **Generate Reports**: Display formatted results and statistics

## 🎯 Implementation Requirements

You will need to implement **6 key methods** in the `SpellChecker` class:

### Core Dictionary & Analysis Methods

- `LoadDictionary(string filename)` - Load word list into HashSet with proper error handling
- `AnalyzeTextFile(string filename)` - Tokenize and normalize text file content
- `CategorizeWords()` - Split unique words into correct/misspelled using dictionary

### Query & Display Methods

- `CheckWord(string word)` - Individual word lookup with occurrence counting
- `GetMisspelledWords(int maxResults)` - Return sorted list of spelling errors
- `GetUniqueWordsSample(int maxResults)` - Return sample of unique words found

_All UI components, menu system, and file I/O handling are provided - focus on these HashSet operations!_

## 📝 Detailed Requirements

### 1. Dictionary Loading Logic

- **File Reading**: Use `File.ReadAllLines()` with proper exception handling
- **Normalization**: Apply `Trim()` and `ToLowerInvariant()` to each word
- **Storage**: Use HashSet with `StringComparer.OrdinalIgnoreCase` for case-insensitive operations
- **Validation**: Check file exists and handle empty files gracefully

### 2. Text Analysis Processing

- **Tokenization**: Split text on whitespace and remove punctuation
- **Normalization**: Apply same normalization as dictionary loading
- **Collections**: Populate both `List<string>` (all words) and `HashSet<string>` (unique words)
- **Performance**: Use efficient string processing techniques

### 3. Word Categorization

- **Dictionary Lookup**: Use `HashSet.Contains()` for O(1) membership testing
- **Set Operations**: Partition words into correct vs misspelled categories
- **Data Integrity**: Clear previous results before processing new text

## 🧪 Testing Requirements

Your application must handle these scenarios:

### Basic Functionality Tests

1. Load dictionary successfully and display word count
2. Analyze sample text files and show statistics
3. Check individual words (both correct and misspelled)
4. Display formatted lists of misspelled and unique words

### Text Processing Tests

1. **Normalization**: "Hello", "hello", and "hello!" should be treated as same word
2. **Case Handling**: Dictionary lookups should work regardless of input case
3. **Punctuation**: Remove punctuation properly without affecting word recognition
4. **Word Counting**: Distinguish between total occurrences and unique words

### Edge Case Tests

1. Try to analyze text before loading dictionary
2. Load non-existent files (both dictionary and text)
3. Process empty files and files with only punctuation
4. Check very long words and special characters

## 🎯 Grading Rubric

### Core Implementation (90 points)

| Component               | Points | Requirements                                                |
| ----------------------- | ------ | ----------------------------------------------------------- |
| **Dictionary Loading**  | 20     | LoadDictionary with file I/O, normalization, error handling |
| **Text Analysis**       | 25     | AnalyzeTextFile with tokenization, dual collections         |
| **Word Categorization** | 20     | CategorizeWords with proper HashSet operations              |
| **Word Checking**       | 15     | CheckWord with dictionary lookup and occurrence counting    |
| **Display Methods**     | 10     | GetMisspelledWords and GetUniqueWordsSample with formatting |

### Code Quality and Documentation (15 points)

| Aspect                     | Points | Requirements                                               |
| -------------------------- | ------ | ---------------------------------------------------------- |
| **Implementation Quality** | 5      | Clean method implementations, proper logic flow            |
| **Error Handling**         | 5      | File I/O exceptions, edge cases, meaningful error messages |
| **Documentation**          | 5      | Complete ASSIGNMENT_NOTES.md with thoughtful reflection    |

### Stretch Features (5 points - Extra Credit)

| Feature                          | Points | Requirements                                              |
| -------------------------------- | ------ | --------------------------------------------------------- |
| **Choose ONE of the following:** | 5      | Complete implementation of any one stretch feature option |
| - Vocabulary Suggestions         |        | String distance algorithms, top 3 suggestions display     |
| - Enhanced Analytics             |        | Frequency analysis, complexity scoring, reading levels    |

**Total: 105 points (110 with extra credit)**

## 📚 Implementation Guide

### Phase 1: Foundation Setup

1. Review the provided `SpellChecker` class structure with TODO comments
2. Understand the HashSet initialization with `StringComparer.OrdinalIgnoreCase`
3. Implement dictionary loading with proper file I/O error handling

### Phase 2: Text Processing Core

1. Implement text file reading and basic tokenization
2. Apply consistent normalization across all text processing
3. Populate both `List<string>` and `HashSet<string>` collections correctly
4. Test with provided sample text files

### Phase 3: Analysis & Categorization

1. Implement word categorization using dictionary Contains() operations
2. Add individual word checking with occurrence counting
3. Implement display methods with proper formatting and sorting
4. Test all interactive menu commands

### Phase 4: Polish & Documentation

1. Add comprehensive error handling and edge case management
2. Test with various file scenarios and invalid inputs
3. Complete ASSIGNMENT_NOTES.md with implementation details
4. Consider implementing one stretch feature for extra credit

## 💡 Tips for Success

### Understanding HashSet Benefits

- **O(1) Lookups**: Contains() is much faster than List.Contains()
- **Automatic Uniqueness**: No need for manual duplicate checking
- **Memory Efficiency**: Stores each unique item only once

### Common Pitfalls to Avoid

1. **Inconsistent normalization** between dictionary loading and text processing
2. **Forgetting case-insensitive comparison** for real-world text data
3. **Not handling file I/O exceptions** properly
4. **Mixing up total word count vs unique word count**

### Testing Strategy

- Start with simple, known text to verify basic functionality
- Test edge cases like empty files and special characters
- Use the interactive menu to verify each operation immediately
- Compare manual counts with program output to verify accuracy

## 📅 Submission Requirements

### What to Submit

1. **All source code files** (.cs files including your completed SpellChecker.cs)
2. **Project file** (Assignment8.csproj)
3. **Data files** (dictionary.txt and any additional test files you create)
4. **ASSIGNMENT_NOTES.md** with your implementation notes and testing documentation

### Submission Format

- Submit link to your GitHub repository
- Code should be in `assignments/assignment_8_sets` directory
- Include clear commit messages showing your development process

### Due Date

**Due: November 21, 2024** by 11:59 PM

## 🚀 Getting Started

1. **Review Lab 8 concepts** - This assignment builds directly on HashSet patterns
2. **Examine the provided structure** - Understand the SpellChecker class layout
3. **Start with LoadDictionary** - This is the foundation for all other operations
4. **Test incrementally** - Verify each TODO method works before moving on
5. **Use the interactive menu** - Test your implementations immediately

## 🔍 Research and Problem Solving

**No external resources or links are provided intentionally!** This assignment is designed to encourage you to develop essential programming research skills.

**When you get stuck, GOOGLE IT!** This is a critical skill for any developer. Examples of effective searches:

- `"C# HashSet StringComparer OrdinalIgnoreCase"`
- `"How to read text file lines C# File.ReadAllLines"`
- `"C# string Split remove empty entries"`
- `"Regex remove punctuation C# string"`
- `"C# LINQ Count occurrences in list"`

**Remember**: Stack Overflow, Microsoft Docs, and C# documentation are your friends. Learning to find solutions independently is just as important as implementing them!

## 🎓 Real-World Applications

This assignment teaches concepts used in:

- **Search Engines** (Google, Bing indexing and spell correction)
- **Word Processors** (Microsoft Word, Google Docs spell checking)
- **Content Management** (WordPress, Medium content validation)
- **Social Media** (Twitter, Facebook content filtering and analysis)
- **E-learning Platforms** (vocabulary assessment and learning tools)
- **Data Cleaning** (ETL processes, text preprocessing pipelines)

---

**Remember**: This assignment is about applying Lab 8 concepts to solve a practical text analysis problem. Focus on understanding how HashSet operations make text processing both efficient and elegant!

**Good luck building your spell checker! 📚✨**
