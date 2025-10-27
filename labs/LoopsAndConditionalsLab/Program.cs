using System.Linq;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Task 1: Sum of Even Numbers");
//Write a method that calculates the sum of even numbers between 1 and 100 using:
//A for loop
//A while loop
//A foreach loop (with a list of numbers from 1–100)
// Print out the result from each approach.

// Method 1: For loop
System.Console.WriteLine();
System.Console.WriteLine("Method 1: Using for loop");
int sum1 = 0;
for (int i = 1; i <= 100; i++)
{
    if (i % 2 == 0) // Check if the number is even
    {
        sum1 += i;
    }
}
Console.WriteLine($"The Sum of Even numbers using for loop is: {sum1}");

//Method2:using while loop
System.Console.WriteLine();
System.Console.WriteLine("Method 2: Using while loop");
int sum2 = 0;
int x = 2;
while (x <= 100) // generates even numbers from 2 to 100 AND adds to sum
{
    sum2 += x;
    x += 2;
}
Console.WriteLine($"The Sum of Even numbers using while loop is: {sum2}");

//Method3:using foreach loop
System.Console.WriteLine();
System.Console.WriteLine("Method 3: Using foreach loop");
List<int> numbers = new List<int>();
int sum3 = 0;
foreach (int num in Enumerable.Range(1, 100)) // generates numbers from 1 to 100 and put in list
{
    numbers.Add(num);
}
foreach (int num in numbers)
{
    if (num % 2 == 0)  // Check if the number is even
    {
        sum3 += num; // Add it to the sum
    }
}
System.Console.WriteLine($"The Sum of Even numbers using foreach loop is: {sum3}");

//Method3.2:using foreach loop (Alternative approach)
System.Console.WriteLine();
Console.WriteLine("Method 3.2: Using foreach loop (Alternative)");
int sum32 = 0;
List<int> numbersList = new List<int>();
for (int number = 1; number <= 100; number++)
{
    numbersList.Add(number);
}

foreach (int num in numbersList)
{
    if (num % 2 == 0)  // Only add even numbers
    {
        sum32 += num;
    }
}
Console.WriteLine($"The Sum of Even numbers using foreach loop (Alternative) is: {sum32}");


System.Console.WriteLine("Question to consider: Which loop felt most natural for this task? Why?");
System.Console.WriteLine("I found the for loop to be the most straightforward for this task, as it clearly defines the range and the condition for summing even numbers.");



System.Console.WriteLine();
System.Console.WriteLine("Task 2: Grading with Conditionals");
System.Console.WriteLine("Please enter a numerical grade (0-100):");
int grade = int.Parse(Console.ReadLine());
string letterGrade;
if (grade >= 90)
{
    letterGrade = "A";
}
else if (grade >= 80)
{
    letterGrade = "B";
}
else if (grade >= 70)
{
    letterGrade = "C";
}
else if (grade >= 60)
{
    letterGrade = "D";
}
else
{
    letterGrade = "F";
}
Console.WriteLine($"Using if-else,your grade is : {letterGrade}");

//Using switch expression

letterGrade = grade switch
{
    >= 90 => "A",
    >= 80 => "B",
    >= 70 => "C",
    >= 60 => "D",
    _ => "F"
};
System.Console.WriteLine($"using switch, your grade is : {letterGrade}");

System.Console.WriteLine();
System.Console.WriteLine("Question to consider: Which approach is easier to read and maintain?");
System.Console.WriteLine("the switch is easier to read especially when have many condition and new shorter syntax.");


System.Console.WriteLine();
System.Console.WriteLine("Task 3: Mini Challenge");

// Modify your sum of even numbers method so that if the sum is greater than 2000, it prints: "That’s a big number!"
// Use at least two different conditional structures to implement this check (e.g., if/else and the ternary ?: operator).

System.Console.WriteLine();
System.Console.WriteLine("Using for loop");
sum1 = 0;
for (int i = 1; i <= 100; i++)
{
    if (i % 2 == 0) // Check if the number is even
    {
        sum1 += i;
    }
}
if (sum1 > 2000)
{
    Console.WriteLine("That’s a big number!");
}
else
{
    Console.WriteLine("All is cool, your sum is: " + sum1);

}

System.Console.WriteLine();
System.Console.WriteLine("Using for TERNARY operator");
Console.WriteLine((sum1 > 2000) ? "That’s a big number!" : "All is cool, your sum is: " + sum1);
