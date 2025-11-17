# Lab 8: Set Operations - The Power of Uniqueness

**Due Date:** November 19, 2024 by 11:59 PM  
**Total Points:** 20

## Assignment Overview

In this hands-on lab, you'll build a complete interactive HashSet<T> application featuring **User Management & Permissions System** with email deduplication and enrollment analysis by following along with your instructor. This lab focuses on understanding set operations (Union, Intersection, Difference) and implementing real-world programming patterns used in email marketing platforms, security systems, and analytics applications.

## Learning Objectives

By completing this lab, you will:

- Master HashSet<T> operations (Add, Contains, UnionWith, IntersectWith, ExceptWith)
- Understand set theory concepts through hands-on coding (Union, Intersection, Difference)
- Implement case-insensitive operations with StringComparer for real-world data handling
- Build permission validation systems with O(1) lookup performance
- Create data analysis systems using set-based logic for enrollment tracking
- Practice proper defensive programming and edge case handling
- Compare HashSet vs List performance characteristics and use cases
- Apply set operations to solve practical business problems

## What You'll Build

You'll create an **Interactive Set Operations System** that demonstrates:

- **Email Deduplication**: Remove case-insensitive duplicates from contact lists
- **Permission Management**: O(1) authorization checking and role-based access control
- **Enrollment Analysis**: Track student changes between quarters using set operations
- **Business Intelligence**: Calculate retention rates and generate meaningful analytics

## Instructions

1. **Follow Along in Class**: This is a guided lab where you'll code along with your instructor
2. **Start with Program.cs**: Use the student version with 8 TODO methods as your starting point
3. **Complete All 8 TODO Methods**: Work through each implementation as demonstrated in class
4. **Test Your Code**: Use the interactive menu to verify each method after implementation
5. **Ask Questions**: If you get stuck or confused, ask for help immediately

## Files Provided

- **`Program.cs`** - Your starting point with 8 TODO methods and HashSet Quick Reference Guide
- **`LabSupport.cs`** - Complete UI system with interactive menu and demo data
- **`README.md`** - Complete lab guide with concepts, examples, and real-world applications

## Submission Requirements

### What to Submit

Submit a link to your GitHub repository where your completed lab code is located in the `labs/lab-8-sets` directory.

### Completion Criteria

Your submitted code must demonstrate:

- ✅ All 8 TODO methods completed and implemented correctly
- ✅ Application runs without crashing and handles edge cases
- ✅ Basic set operations work (Add, Contains, UnionWith, IntersectWith, ExceptWith)
- ✅ Proper case-insensitive email deduplication using StringComparer
- ✅ Permission system with defensive programming (user existence checking)
- ✅ Set-based enrollment analysis with correct mathematical relationships
- ✅ **TODO #1**: `DeduplicateEmails()` - Case-insensitive duplicate removal
- ✅ **TODO #2**: `HasPermission()` - O(1) authorization checking with error handling
- ✅ **TODO #3**: `AddPermissions()` - Set union operations for role management
- ✅ **TODO #4**: `GetMissingPermissions()` - Access validation with set difference
- ✅ **TODO #5**: `FindNewStudents()` - Growth analysis using set difference
- ✅ **TODO #6**: `FindDroppedStudents()` - Churn analysis using inverse set difference
- ✅ **TODO #7**: `FindContinuingStudents()` - Loyalty tracking using set intersection
- ✅ **TODO #8**: `CalculateRetentionRate()` - Business metrics combining sets with math

### Testing Checklist

Before submitting, verify your application can:

1. **Deduplicate emails** with case-insensitive handling ("user@email.com" = "USER@EMAIL.COM")
2. **Check permissions** for existing and non-existent users without crashing
3. **Grant permissions** to new and existing users, returning accurate counts
4. **Validate access** by identifying missing permissions for security features
5. **Analyze enrollment** changes between quarters with correct set operations
6. **Calculate retention** rates with proper edge case handling (division by zero)
7. **Handle empty sets** and invalid inputs gracefully
8. **Display meaningful results** through the interactive menu system
9. **Show session statistics** and demonstrate understanding of set performance benefits

## Grading

This lab uses **completion-based grading**:

### Full Credit (20 points)

- Submitted working code that completes all 8 TODO methods correctly
- Application demonstrates clear understanding of HashSet<T> operations and set theory
- Code shows evidence of following the guided implementation steps
- All basic and advanced features work correctly with proper error handling

### Partial Credit (14 points)

- Most TODO methods completed but some functionality incomplete or incorrect
- Basic set operations work but advanced features may have issues
- Clear effort demonstrated but minor logical or implementation errors present

### Minimal Credit (8 points)

- Some TODO methods completed showing basic understanding
- Code shows participation but significant functionality missing or incorrect
- Application may have issues but demonstrates engagement with core concepts

### No Credit (0 points)

- No submission or no evidence of participation
- Code doesn't compile or shows no understanding of set concepts

## Key Concepts Covered

### HashSet<T> vs List<T> Comparison

- **HashSet (Uniqueness + Speed)**: O(1) Contains(), automatic duplicate prevention, set operations
- **List (Order + Indexing)**: O(n) Contains(), allows duplicates, maintains insertion order

### Set Theory in Practice

- **Union (A ∪ B)**: Combining permission sets without duplicates
- **Intersection (A ∩ B)**: Finding students enrolled in both quarters
- **Difference (A - B)**: Identifying new enrollments or missing permissions

### Professional Programming Patterns

- Case-insensitive string operations with StringComparer.OrdinalIgnoreCase
- Defensive programming with user existence validation
- Edge case handling (empty sets, division by zero, non-existent users)
- Business logic implementation (retention rate calculations, permission validation)

### Performance Optimization

- O(1) membership testing vs O(n) linear search
- Automatic deduplication without manual checking
- Set operations in single method calls vs nested loops

## Real-World Applications

This lab teaches patterns used in:

- **Email Marketing Platforms** (Mailchimp, Constant Contact deduplication systems)
- **Security Systems** (Auth0, Okta permission validation and role management)
- **Social Media Platforms** (Facebook friend suggestions, LinkedIn connection analysis)
- **Analytics Platforms** (Google Analytics unique visitor tracking, user behavior analysis)
- **E-commerce Systems** (Amazon product filtering, recommendation engines)
- **Data Processing Pipelines** (ETL operations, data cleaning and deduplication workflows)

---

**Remember**: This lab is about learning set theory through practical implementation. Focus on understanding how HashSet<T> operations solve real business problems as you build each method. Don't hesitate to ask questions during the guided session!
