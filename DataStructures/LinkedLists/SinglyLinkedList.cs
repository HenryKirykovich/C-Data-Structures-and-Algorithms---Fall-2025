using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LinkedLists;

/// <summary>
/// A node in a singly linked list
/// </summary>
/// <typeparam name="T">The type of data stored in the node</typeparam>
public class SinglyLinkedListNode<T>
{
    public T Data { get; set; }
    public SinglyLinkedListNode<T>? Next { get; set; }

    public SinglyLinkedListNode(T data)
    {
        Data = data;
        Next = null;
    }
}

/// <summary>
/// A singly linked list implementation
/// </summary>
/// <typeparam name="T">The type of elements in the list</typeparam>
public class SinglyLinkedList<T> : IEnumerable<T>
{
    private SinglyLinkedListNode<T>? head;
    private int count;

    public int Count => count;
    public bool IsEmpty => head == null;

    /// <summary>
    /// Adds an element to the beginning of the list
    /// </summary>
    /// <param name="data">The data to add</param>
    public void AddFirst(T data)
    {
        var newNode = new SinglyLinkedListNode<T>(data)
        {
            Next = head
        };
        head = newNode;
        count++;
    }

    /// <summary>
    /// Adds an element to the end of the list
    /// </summary>
    /// <param name="data">The data to add</param>
    public void AddLast(T data)
    {
        var newNode = new SinglyLinkedListNode<T>(data);
        
        if (head == null)
        {
            head = newNode;
        }
        else
        {
            var current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
        }
        count++;
    }

    /// <summary>
    /// Removes the first occurrence of the specified value
    /// </summary>
    /// <param name="data">The value to remove</param>
    /// <returns>True if the value was found and removed, false otherwise</returns>
    public bool Remove(T data)
    {
        if (head == null)
            return false;

        if (EqualityComparer<T>.Default.Equals(head.Data, data))
        {
            head = head.Next;
            count--;
            return true;
        }

        var current = head;
        while (current.Next != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Next.Data, data))
            {
                current.Next = current.Next.Next;
                count--;
                return true;
            }
            current = current.Next;
        }

        return false;
    }

    /// <summary>
    /// Checks if the list contains the specified value
    /// </summary>
    /// <param name="data">The value to search for</param>
    /// <returns>True if the value is found, false otherwise</returns>
    public bool Contains(T data)
    {
        var current = head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(current.Data, data))
                return true;
            current = current.Next;
        }
        return false;
    }

    /// <summary>
    /// Removes all elements from the list
    /// </summary>
    public void Clear()
    {
        head = null;
        count = 0;
    }

    /// <summary>
    /// Gets an array representation of the list
    /// </summary>
    /// <returns>An array containing all elements in the list</returns>
    public T[] ToArray()
    {
        var result = new T[count];
        var current = head;
        var index = 0;

        while (current != null)
        {
            result[index++] = current.Data;
            current = current.Next;
        }

        return result;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var current = head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}