using DoublyLinkedListAssignment.Core;

namespace DoublyLinkedListAssignment.Applications;

public sealed class MusicPlaylist
{
    private readonly DoublyLinkedList<Song> _songs = new(); // put all my album inside  
    private int _currentIndex = -1; // -1 means no selection

    public int Count => _songs.Count;
    public bool IsEmpty => Count == 0;
    public Song? Current => GetAt(_currentIndex);

    public void Add(Song song)
    {
        _songs.AddLast(song);
        if (Count == 1)
        {
            _currentIndex = 0;
        }
    }

    public void Print()
    {
        if (IsEmpty)
        {
            Console.WriteLine("(playlist empty)");
            return;
        }
        int i = 0;
        foreach (var s in _songs)
        {
            var marker = (i == _currentIndex) ? "->" : "  ";
            Console.WriteLine($"{marker} {i + 1}. {s}");
            i++;
        }
    }

    public void ShowCurrent()
    {
        if (IsEmpty || _currentIndex < 0)
        {
            Console.WriteLine("(no current song)");
            return;
        }
        Console.WriteLine($"Current: {Current}");
    }

    public void Next()
    {
        if (IsEmpty)
        {
            Console.WriteLine("(playlist empty)");
            return;
        }
        _currentIndex = (_currentIndex + 1) % Count; // wrap-around
        Console.WriteLine($"Now playing: {Current}");
    }

    public void Prev()
    {
        if (IsEmpty)
        {
            Console.WriteLine("(playlist empty)");
            return;
        }
        _currentIndex = (_currentIndex - 1 + Count) % Count; // wrap-around
        Console.WriteLine($"Now playing: {Current}");
    }

    public void RemoveCurrent()
    {
        if (IsEmpty || _currentIndex < 0)
        {
            Console.WriteLine("(nothing to remove)");
            return;
        }
        var removed = Current;
        RemoveAt(_currentIndex);
        if (Count == 0)
        {
            _currentIndex = -1;
            Console.WriteLine($"Removed: {removed}. Playlist is now empty.");
            return;
        }
        if (_currentIndex >= Count)
        {
            _currentIndex = Count - 1; // move to last if we removed the last item
        }
        Console.WriteLine($"Removed: {removed}. Now playing: {Current}");
    }

    public void Clear()
    {
        _songs.Clear();
        _currentIndex = -1;
    }

    private void RemoveAt(int index) => _songs.RemoveAt(index);

    private Song? GetAt(int index)
    {
        if (index < 0 || index >= Count) return null;
        int i = 0;
        foreach (var s in _songs)
        {
            if (i == index) return s;
            i++;
        }
        return null;
    }
}
