using DataStructures.LinkedLists;
using DataStructures.Trees;
using DataStructures.Multithreading;

namespace DataStructures.Tests;

public class SinglyLinkedListTests
{
    [Fact]
    public void AddFirst_ShouldAddElementToBeginning()
    {
        // Arrange
        var list = new SinglyLinkedList<int>();
        
        // Act
        list.AddFirst(1);
        list.AddFirst(2);
        
        // Assert
        Assert.Equal(2, list.Count);
        var array = list.ToArray();
        Assert.Equal(2, array[0]);
        Assert.Equal(1, array[1]);
    }

    [Fact]
    public void AddLast_ShouldAddElementToEnd()
    {
        // Arrange
        var list = new SinglyLinkedList<int>();
        
        // Act
        list.AddLast(1);
        list.AddLast(2);
        
        // Assert
        Assert.Equal(2, list.Count);
        var array = list.ToArray();
        Assert.Equal(1, array[0]);
        Assert.Equal(2, array[1]);
    }

    [Fact]
    public void Remove_ExistingElement_ShouldReturnTrueAndRemoveElement()
    {
        // Arrange
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        
        // Act
        var result = list.Remove(2);
        
        // Assert
        Assert.True(result);
        Assert.Equal(2, list.Count);
        Assert.False(list.Contains(2));
        var array = list.ToArray();
        Assert.Equal(new[] { 1, 3 }, array);
    }

    [Fact]
    public void Remove_NonExistingElement_ShouldReturnFalse()
    {
        // Arrange
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        
        // Act
        var result = list.Remove(3);
        
        // Assert
        Assert.False(result);
        Assert.Equal(2, list.Count);
    }

    [Fact]
    public void Contains_ExistingElement_ShouldReturnTrue()
    {
        // Arrange
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        
        // Act & Assert
        Assert.True(list.Contains(1));
        Assert.True(list.Contains(2));
    }

    [Fact]
    public void Contains_NonExistingElement_ShouldReturnFalse()
    {
        // Arrange
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        
        // Act & Assert
        Assert.False(list.Contains(3));
    }

    [Fact]
    public void Clear_ShouldRemoveAllElements()
    {
        // Arrange
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        
        // Act
        list.Clear();
        
        // Assert
        Assert.Equal(0, list.Count);
        Assert.True(list.IsEmpty);
        Assert.Empty(list.ToArray());
    }

    [Fact]
    public void Enumeration_ShouldIterateInCorrectOrder()
    {
        // Arrange
        var list = new SinglyLinkedList<int>();
        list.AddLast(1);
        list.AddLast(2);
        list.AddLast(3);
        
        // Act
        var result = new List<int>();
        foreach (var item in list)
        {
            result.Add(item);
        }
        
        // Assert
        Assert.Equal(new[] { 1, 2, 3 }, result);
    }
}