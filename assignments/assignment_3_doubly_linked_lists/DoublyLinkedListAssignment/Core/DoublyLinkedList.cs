using System.Collections;

namespace DoublyLinkedListAssignment.Core;

/// <summary>
/// Step-by-step generic Doubly Linked List scaffold.
/// This minimal version supports AddFirst/AddLast and enumeration.
/// Other methods will be added in subsequent steps.
/// </summary>
public class DoublyLinkedList<T> : IEnumerable<T> 
// for heritage foreach method like list 
{
    internal sealed class Node // insert class for saving node 
    {
        public T Value;
        public Node? Next;
        public Node? Prev;
        public Node(T value) => Value = value;
    }

    private Node? _head;   // creating double linked list head will be in left side 
    private Node? _tail; // tail will be in right side
    public int Count { get; private set; }
    public bool IsEmpty => Count == 0;

    public void AddFirst(T value)
    {
        var n = new Node(value);
        if (_head == null)
        {
            _head = _tail = n;
        }
        else
        {
            n.Next = _head;
            _head.Prev = n;
            _head = n;
        }
        Count++;
    }

    public void AddLast(T value)
    {
        var n = new Node(value);
        if (_tail == null)
        {
            _head = _tail = n;
        }
        else
        {
            _tail.Next = n;
            n.Prev = _tail;
            _tail = n;
        }
        Count++;
    }

    public void InsertAt(int index, T value)
    {
        if (index < 0 || index > Count)
            throw new ArgumentOutOfRangeException(nameof(index));
        if (index == 0) { AddFirst(value); return; }
        if (index == Count) { AddLast(value); return; }

        var next = NodeAt(index)!; // cannot be null here
        var prev = next.Prev!;
        var n = new Node(value)
        {
            Prev = prev,
            Next = next
        };
        prev.Next = n;
        next.Prev = n;
        Count++;
    }

    public T RemoveFirst()
    {
        if (_head == null) throw new InvalidOperationException("List is empty");
        var val = _head.Value;
        _head = _head.Next;
        if (_head != null)
        {
            _head.Prev = null;
        }
        else
        {
            _tail = null; // list became empty
        }
        Count--;
        return val;
    }

    public T RemoveLast()
    {
        if (_tail == null) throw new InvalidOperationException("List is empty");
        var val = _tail.Value;
        _tail = _tail.Prev;
        if (_tail != null)
        {
            _tail.Next = null;
        }
        else
        {
            _head = null;
        }
        Count--;
        return val;
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= Count)
            throw new ArgumentOutOfRangeException(nameof(index));
        if (index == 0) { RemoveFirst(); return; }
        if (index == Count - 1) { RemoveLast(); return; }

        var cur = NodeAt(index)!;
        var prev = cur.Prev!;
        var next = cur.Next!;
        prev.Next = next;
        next.Prev = prev;
        Count--;
    }

    public bool Remove(T value)
    {
        int idx = IndexOf(value);
        if (idx >= 0)
        {
            RemoveAt(idx);
            return true;
        }
        return false;
    }

    public bool Contains(T value) => IndexOf(value) >= 0;

    public int IndexOf(T value)
    {
        var cmp = EqualityComparer<T>.Default;
        int i = 0;
        for (var cur = _head; cur != null; cur = cur.Next)
        {
            if (cmp.Equals(cur.Value, value)) return i;
            i++;
        }
        return -1;
    }

    public void Clear()
    {
        _head = _tail = null;
        Count = 0;
    }

    private Node? NodeAt(int index)
    {
        // optimize by starting from head or tail depending on index
        if (index < 0 || index >= Count) return null;
        if (index <= Count / 2)
        {
            int i = 0;
            var cur = _head;
            while (cur != null)
            {
                if (i == index) return cur;
                i++;
                cur = cur.Next;
            }
            return null;
        }
        else
        {
            int i = Count - 1;
            var cur = _tail;
            while (cur != null)
            {
                if (i == index) return cur;
                i--;
                cur = cur.Prev;
            }
            return null;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        var cur = _head;
        while (cur != null)
        {
            yield return cur.Value;
            cur = cur.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Enumerate items from tail to head.
    /// </summary>
    public IEnumerable<T> EnumerateBackward()
    {
        var cur = _tail;
        while (cur != null)
        {
            yield return cur.Value;
            cur = cur.Prev;
        }
    }
}
