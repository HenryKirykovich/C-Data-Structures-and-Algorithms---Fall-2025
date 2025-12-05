using DoublyLinkedListAssignment.Core;
using DoublyLinkedListAssignment.Applications;

namespace DoublyLinkedListAssignment;

public class Program
{
    public static void Main()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Doubly Linked List Assignment");
            Console.WriteLine("1 or A) Part A - Core List Demo");
            Console.WriteLine("2 or B) Part B - Music Playlist Manager");
            Console.WriteLine("0 or Q) Exit");
            Console.Write("Select: ");
            var key = Console.ReadLine()?.Trim();
            switch (key?.ToLowerInvariant())
            {
                case "1":
                case "a":
                    CoreListDemo.MyClassRun();
                    break;
                case "2":
                case "b":
                    MusicPlaylistManager.MyClassRun();
                    break;
                case "0":
                case "q":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Press any key...");
                    Console.ReadKey(true);
                    break;
            }
        }
    }
}
