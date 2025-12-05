namespace DoublyLinkedListAssignment.Applications;

public sealed class Song
{
    public string Title { get; }
    public string Artist { get; }
    public TimeSpan Duration { get; }

    public Song(string title, string artist, TimeSpan duration)
    {
        Title = title;
        Artist = artist;
        Duration = duration;
    }

    public override string ToString() => $"{Title} — {Artist} ({Duration:mm\\:ss})";
}
