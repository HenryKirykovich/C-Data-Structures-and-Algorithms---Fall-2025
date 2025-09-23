using DataStructures.Trees;

namespace DataStructures.Tests;

public class BinarySearchTreeTests
{
    [Fact]
    public void Insert_ShouldAddElementsInCorrectOrder()
    {
        // Arrange
        var bst = new BinarySearchTree<int>();
        
        // Act
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);
        
        // Assert
        Assert.Equal(5, bst.Count);
        var inOrder = bst.InOrderTraversal();
        Assert.Equal(new[] { 20, 30, 40, 50, 70 }, inOrder);
    }

    [Fact]
    public void Contains_ExistingElement_ShouldReturnTrue()
    {
        // Arrange
        var bst = new BinarySearchTree<int>();
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        
        // Act & Assert
        Assert.True(bst.Contains(50));
        Assert.True(bst.Contains(30));
        Assert.True(bst.Contains(70));
    }

    [Fact]
    public void Contains_NonExistingElement_ShouldReturnFalse()
    {
        // Arrange
        var bst = new BinarySearchTree<int>();
        bst.Insert(50);
        bst.Insert(30);
        
        // Act & Assert
        Assert.False(bst.Contains(25));
        Assert.False(bst.Contains(80));
    }

    [Fact]
    public void Remove_LeafNode_ShouldRemoveCorrectly()
    {
        // Arrange
        var bst = new BinarySearchTree<int>();
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        
        // Act
        var result = bst.Remove(20);
        
        // Assert
        Assert.True(result);
        Assert.Equal(3, bst.Count);
        Assert.False(bst.Contains(20));
        var inOrder = bst.InOrderTraversal();
        Assert.Equal(new[] { 30, 50, 70 }, inOrder);
    }

    [Fact]
    public void Remove_NodeWithOneChild_ShouldRemoveCorrectly()
    {
        // Arrange
        var bst = new BinarySearchTree<int>();
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        
        // Act
        var result = bst.Remove(30);
        
        // Assert
        Assert.True(result);
        Assert.Equal(3, bst.Count);
        Assert.False(bst.Contains(30));
        var inOrder = bst.InOrderTraversal();
        Assert.Equal(new[] { 20, 50, 70 }, inOrder);
    }

    [Fact]
    public void Remove_NodeWithTwoChildren_ShouldRemoveCorrectly()
    {
        // Arrange
        var bst = new BinarySearchTree<int>();
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);
        bst.Insert(60);
        bst.Insert(80);
        
        // Act
        var result = bst.Remove(50);
        
        // Assert
        Assert.True(result);
        Assert.Equal(6, bst.Count);
        Assert.False(bst.Contains(50));
        var inOrder = bst.InOrderTraversal();
        Assert.Equal(new[] { 20, 30, 40, 60, 70, 80 }, inOrder);
    }

    [Fact]
    public void Remove_NonExistingElement_ShouldReturnFalse()
    {
        // Arrange
        var bst = new BinarySearchTree<int>();
        bst.Insert(50);
        bst.Insert(30);
        
        // Act
        var result = bst.Remove(25);
        
        // Assert
        Assert.False(result);
        Assert.Equal(2, bst.Count);
    }

    [Fact]
    public void PreOrderTraversal_ShouldReturnCorrectOrder()
    {
        // Arrange
        var bst = new BinarySearchTree<int>();
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);
        
        // Act
        var preOrder = bst.PreOrderTraversal();
        
        // Assert
        Assert.Equal(new[] { 50, 30, 20, 40, 70 }, preOrder);
    }

    [Fact]
    public void PostOrderTraversal_ShouldReturnCorrectOrder()
    {
        // Arrange
        var bst = new BinarySearchTree<int>();
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        bst.Insert(20);
        bst.Insert(40);
        
        // Act
        var postOrder = bst.PostOrderTraversal();
        
        // Assert
        Assert.Equal(new[] { 20, 40, 30, 70, 50 }, postOrder);
    }

    [Fact]
    public void Clear_ShouldRemoveAllElements()
    {
        // Arrange
        var bst = new BinarySearchTree<int>();
        bst.Insert(50);
        bst.Insert(30);
        bst.Insert(70);
        
        // Act
        bst.Clear();
        
        // Assert
        Assert.Equal(0, bst.Count);
        Assert.True(bst.IsEmpty);
        Assert.Empty(bst.InOrderTraversal());
    }
}