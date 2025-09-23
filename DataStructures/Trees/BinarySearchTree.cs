using System;
using System.Collections.Generic;

namespace DataStructures.Trees;

/// <summary>
/// A node in a binary tree
/// </summary>
/// <typeparam name="T">The type of data stored in the node</typeparam>
public class BinaryTreeNode<T>
{
    public T Data { get; set; }
    public BinaryTreeNode<T>? Left { get; set; }
    public BinaryTreeNode<T>? Right { get; set; }

    public BinaryTreeNode(T data)
    {
        Data = data;
        Left = null;
        Right = null;
    }
}

/// <summary>
/// A binary search tree implementation
/// </summary>
/// <typeparam name="T">The type of elements in the tree</typeparam>
public class BinarySearchTree<T> where T : IComparable<T>
{
    private BinaryTreeNode<T>? root;
    private int count;

    public int Count => count;
    public bool IsEmpty => root == null;

    /// <summary>
    /// Inserts a value into the binary search tree
    /// </summary>
    /// <param name="data">The value to insert</param>
    public void Insert(T data)
    {
        root = InsertRecursive(root, data);
        count++;
    }

    private BinaryTreeNode<T> InsertRecursive(BinaryTreeNode<T>? node, T data)
    {
        if (node == null)
            return new BinaryTreeNode<T>(data);

        var comparison = data.CompareTo(node.Data);
        if (comparison < 0)
            node.Left = InsertRecursive(node.Left, data);
        else if (comparison > 0)
            node.Right = InsertRecursive(node.Right, data);
        // Equal values are not inserted (no duplicates)

        return node;
    }

    /// <summary>
    /// Searches for a value in the binary search tree
    /// </summary>
    /// <param name="data">The value to search for</param>
    /// <returns>True if the value is found, false otherwise</returns>
    public bool Contains(T data)
    {
        return SearchRecursive(root, data);
    }

    private bool SearchRecursive(BinaryTreeNode<T>? node, T data)
    {
        if (node == null)
            return false;

        var comparison = data.CompareTo(node.Data);
        if (comparison == 0)
            return true;
        else if (comparison < 0)
            return SearchRecursive(node.Left, data);
        else
            return SearchRecursive(node.Right, data);
    }

    /// <summary>
    /// Removes a value from the binary search tree
    /// </summary>
    /// <param name="data">The value to remove</param>
    /// <returns>True if the value was found and removed, false otherwise</returns>
    public bool Remove(T data)
    {
        var originalCount = count;
        root = RemoveRecursive(root, data);
        return count < originalCount;
    }

    private BinaryTreeNode<T>? RemoveRecursive(BinaryTreeNode<T>? node, T data)
    {
        if (node == null)
            return null;

        var comparison = data.CompareTo(node.Data);
        if (comparison < 0)
        {
            node.Left = RemoveRecursive(node.Left, data);
        }
        else if (comparison > 0)
        {
            node.Right = RemoveRecursive(node.Right, data);
        }
        else
        {
            count--;
            // Node to be deleted found
            if (node.Left == null)
                return node.Right;
            if (node.Right == null)
                return node.Left;

            // Node with two children
            var successor = FindMin(node.Right);
            node.Data = successor.Data;
            count++; // Increment back since the recursive call will decrement again
            node.Right = RemoveRecursive(node.Right, successor.Data);
        }

        return node;
    }

    private BinaryTreeNode<T> FindMin(BinaryTreeNode<T> node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    /// <summary>
    /// Performs an in-order traversal of the tree
    /// </summary>
    /// <returns>A list of values in in-order sequence</returns>
    public List<T> InOrderTraversal()
    {
        var result = new List<T>();
        InOrderTraversalRecursive(root, result);
        return result;
    }

    private void InOrderTraversalRecursive(BinaryTreeNode<T>? node, List<T> result)
    {
        if (node != null)
        {
            InOrderTraversalRecursive(node.Left, result);
            result.Add(node.Data);
            InOrderTraversalRecursive(node.Right, result);
        }
    }

    /// <summary>
    /// Performs a pre-order traversal of the tree
    /// </summary>
    /// <returns>A list of values in pre-order sequence</returns>
    public List<T> PreOrderTraversal()
    {
        var result = new List<T>();
        PreOrderTraversalRecursive(root, result);
        return result;
    }

    private void PreOrderTraversalRecursive(BinaryTreeNode<T>? node, List<T> result)
    {
        if (node != null)
        {
            result.Add(node.Data);
            PreOrderTraversalRecursive(node.Left, result);
            PreOrderTraversalRecursive(node.Right, result);
        }
    }

    /// <summary>
    /// Performs a post-order traversal of the tree
    /// </summary>
    /// <returns>A list of values in post-order sequence</returns>
    public List<T> PostOrderTraversal()
    {
        var result = new List<T>();
        PostOrderTraversalRecursive(root, result);
        return result;
    }

    private void PostOrderTraversalRecursive(BinaryTreeNode<T>? node, List<T> result)
    {
        if (node != null)
        {
            PostOrderTraversalRecursive(node.Left, result);
            PostOrderTraversalRecursive(node.Right, result);
            result.Add(node.Data);
        }
    }

    /// <summary>
    /// Clears all elements from the tree
    /// </summary>
    public void Clear()
    {
        root = null;
        count = 0;
    }
}