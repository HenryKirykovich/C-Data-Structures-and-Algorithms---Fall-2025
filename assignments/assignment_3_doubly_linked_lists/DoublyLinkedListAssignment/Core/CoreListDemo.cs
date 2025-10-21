namespace DoublyLinkedListAssignment.Core;

public static class CoreListDemo
{
    public static void MyClassRun()
    {
        var list = new DoublyLinkedList<int>();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Part A - Core DoublyLinkedList Demo (interactive)");
            Console.WriteLine($"List: [ {string.Join(", ", list)} ] | Count={list.Count}");
            Console.WriteLine();
            Console.WriteLine("P) Print forward");
            Console.WriteLine("B) Print backward");
            Console.WriteLine("1) AddFirst(x)");
            Console.WriteLine("2) AddLast(x)");
            Console.WriteLine("3) InsertAt(index, x)");
            Console.WriteLine("4) RemoveFirst()");
            Console.WriteLine("5) RemoveLast()");
            Console.WriteLine("6) RemoveAt(index)");
            Console.WriteLine("7) Remove(value)");
            Console.WriteLine("8) Contains(value)");
            Console.WriteLine("9) IndexOf(value)");
            Console.WriteLine("C) Clear()");
            Console.WriteLine("0) Back");
            Console.Write("Select: ");
            var choice = Console.ReadLine()?.Trim();
            try
            {
                switch (choice)
                {
                    case "P":
                    case "p":
                        Console.WriteLine("Forward: [ " + string.Join(", ", list) + " ]");
                        Pause();
                        break;
                    case "B":
                    case "b":
                        Console.WriteLine("Backward: [ " + string.Join(", ", list.EnumerateBackward()) + " ]");
                        Pause();
                        break;
                    case "1":
                        Console.Write("x = ");
                        if (int.TryParse(Console.ReadLine(), out var a1))
                            list.AddFirst(a1);
                        break;
                    case "2":
                        Console.Write("x = ");
                        if (int.TryParse(Console.ReadLine(), out var a2))
                            list.AddLast(a2);
                        break;
                    case "3":
                        Console.Write("index = ");
                        var iTxt = Console.ReadLine();
                        Console.Write("x = ");
                        var vTxt = Console.ReadLine();
                        if (int.TryParse(iTxt, out var idx) && int.TryParse(vTxt, out var v))
                            list.InsertAt(idx, v);
                        break;
                    case "4":
                        if (!list.IsEmpty) Console.WriteLine($"Removed: {list.RemoveFirst()}"); else Console.WriteLine("(empty)");
                        break;
                    case "5":
                        if (!list.IsEmpty) Console.WriteLine($"Removed: {list.RemoveLast()}"); else Console.WriteLine("(empty)");
                        break;
                    case "6":
                        Console.Write("index = ");
                        if (int.TryParse(Console.ReadLine(), out var rIdx))
                            list.RemoveAt(rIdx);
                        break;
                    case "7":
                        Console.Write("value = ");
                        if (int.TryParse(Console.ReadLine(), out var rv))
                            Console.WriteLine(list.Remove(rv) ? "Removed" : "Not found");
                        break;
                    case "8":
                        Console.Write("value = ");
                        if (int.TryParse(Console.ReadLine(), out var cv))
                            Console.WriteLine(list.Contains(cv) ? "Yes" : "No");
                        break;
                    case "9":
                        Console.Write("value = ");
                        if (int.TryParse(Console.ReadLine(), out var iv))
                            Console.WriteLine("Index: " + list.IndexOf(iv));
                        break;
                    case "C":
                    case "c":
                        list.Clear();
                        break;
                    case "0":
                        return;
                    default:
                        Pause();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                Pause();
            }
        }
    }

    private static void Pause()
    {
        Console.WriteLine("Press any key...");
        Console.ReadKey(true);
    }
}
