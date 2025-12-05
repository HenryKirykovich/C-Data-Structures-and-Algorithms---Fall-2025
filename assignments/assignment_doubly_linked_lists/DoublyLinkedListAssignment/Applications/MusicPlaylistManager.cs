using System.Globalization;

namespace DoublyLinkedListAssignment.Applications;

public static class MusicPlaylistManager
{
    public static void MyClassRun()
    {
        Console.Clear();
        var pl = new MusicPlaylist();
        while (true)
        {
            Console.WriteLine("Music Playlist Manager");
            Console.WriteLine("1) Add sample songs");
            Console.WriteLine("A) Add custom song");
            Console.WriteLine("2) Show playlist");
            Console.WriteLine("3) Show current");
            Console.WriteLine("4) Next");
            Console.WriteLine("5) Prev");
            Console.WriteLine("6) Remove current");
            Console.WriteLine("C) Clear playlist");
            Console.WriteLine("0) Back");
            Console.Write("Select: ");
            var key = Console.ReadLine();
            switch (key)
            {
                case "1":
                    pl.Add(new Song("Song A", "Artist 1", TimeSpan.FromSeconds(210)));
                    pl.Add(new Song("Song B", "Artist 2", TimeSpan.FromSeconds(185)));
                    pl.Add(new Song("Song C", "Artist 3", TimeSpan.FromSeconds(200)));
                    Console.WriteLine("Added 3 sample songs.\n");
                    break;
                case "A":
                case "a":
                    Console.Write("Title: ");
                    var title = Console.ReadLine()?.Trim();
                    Console.Write("Artist: ");
                    var artist = Console.ReadLine()?.Trim();
                    Console.Write("Duration (mm:ss or seconds): ");
                    var dtxt = Console.ReadLine()?.Trim();
                    if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(artist) || string.IsNullOrWhiteSpace(dtxt))
                    {
                        Console.WriteLine("Invalid input. Title, Artist, and Duration are required.\n");
                        break;
                    }
                    if (!TryParseDuration(dtxt!, out var dur))
                    {
                        Console.WriteLine("Invalid duration. Use mm:ss (e.g., 03:15) or total seconds (e.g., 195).\n");
                        break;
                    }
                    pl.Add(new Song(title!, artist!, dur));
                    Console.WriteLine("Song added.\n");
                    break;
                case "2":
                    pl.Print();
                    Console.WriteLine();
                    break;
                case "3": // show current
                    pl.ShowCurrent();
                    Console.WriteLine();
                    break;
                case "4": // next
                    pl.Next();
                    Console.WriteLine();
                    break;
                case "5": // prev
                    pl.Prev();
                    Console.WriteLine();
                    break;
                case "6": // remove current
                    pl.RemoveCurrent();
                    Console.WriteLine();
                    break;
                case "C":
                case "c":
                    pl.Clear();
                    Console.WriteLine("Playlist cleared.\n");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice.\n");
                    break;
            }
        }
    }

    private static bool TryParseDuration(string input, out TimeSpan duration)
    {
        // Accept mm:ss, m:ss, h:mm:ss, or plain seconds
        var formats = new[] { @"m\:ss", @"mm\:ss", @"h\:mm\:ss" };
        if (TimeSpan.TryParseExact(input, formats, CultureInfo.InvariantCulture, out duration))
        {
            return true;
        }
        if (int.TryParse(input, out var seconds) && seconds >= 0)
        {
            duration = TimeSpan.FromSeconds(seconds);
            return true;
        }
        duration = default;
        return false;
    }
}
