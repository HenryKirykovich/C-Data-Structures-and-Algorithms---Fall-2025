// See https://aka.ms/new-console-template for more information
Console.WriteLine("How stack is working");
Console.WriteLine();

Stack<int> number = new Stack<int>();

number.Push(000);
number.Push(10);
number.Push(20);
number.Push(30);

Console.WriteLine("Stack contents:");
int count = 0;
foreach (var num in number)
{
    count++;
    Console.WriteLine($"The number\n{count} from stack is {num}");
}

Console.WriteLine($"Top element: {number.Peek()}");

number.Pop();
Console.WriteLine("After popping one element:");

foreach (var num in number)
{
    Console.WriteLine(num);
}

