using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("HashSet useful for combining many unique stuff");

            HashSet<string> setSongs = new HashSet<string>()
            { "Song1", "Song2", "Song3", "Song4" };
            
            Console.WriteLine("List of songs from first Set:");
            foreach (var item in setSongs)
            {
                Console.WriteLine(item);
            }
            
            HashSet<string> setSongs2 = new HashSet<string>()
            { "Song3", "Song4", "Song5", "Song6" };
            
            Console.WriteLine("List of songs from second Set:");
            foreach (var item in setSongs2)
            {
                Console.WriteLine(item);
            }

            // Create a copy to preserve original for intersection demo
            HashSet<string> combinedSongs = new HashSet<string>(setSongs);
            combinedSongs.UnionWith(setSongs2); // Combines two sets, removing duplicates
            
            Console.WriteLine("List of songs from both Sets (Union):");
            foreach (var item in combinedSongs)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine(); 
            setSongs.IntersectWith(setSongs2); // Keeps only the elements that are in both sets 
            Console.WriteLine("List of songs that are in both Sets (Intersection):");
            foreach (var item in setSongs)
            {
                Console.WriteLine(item);
            }



                // how to use a graph
            Console.WriteLine();
            Console.WriteLine("Graph");

            Node a = new Node("A");
            Node b = new Node("B");
            Node c = new Node("C");
            Node d = new Node("D");

            a.Connect(b);
            a.Connect(c);
            a.Connect(d);
            a.Display();
        }
    }

    class Node
    {
        public string node;
        public List<Node> Neighborn;

        public Node(string node)
        {
            this.node = node;
            Neighborn = new List<Node>();
        }
        
        public void Connect(Node other)
        {
            Neighborn.Add(other);
        }

        public void Display()
        {
            Console.WriteLine($"Node {node} is connected to:");
            foreach (var item in Neighborn)
            {
                Console.WriteLine($"  - {item.node}");
            }
            Console.WriteLine($"Total connections: {Neighborn.Count}");
        }
    }
}
