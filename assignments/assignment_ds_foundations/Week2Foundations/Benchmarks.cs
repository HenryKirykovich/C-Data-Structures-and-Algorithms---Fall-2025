using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

public static class Benchmarks
{
    public static void Run()
    {
        int[] Ns = { 50, 1000, 10_000, 100_000, 250_000 };

        foreach (int N in Ns)
        {
            Console.WriteLine($"\nN = {N}");

            // Standard collections
            var list = Enumerable.Range(0, N).ToList();
            var hashSet = new HashSet<int>(list);
            var dict = list.ToDictionary(x => x, x => true);

            // Stretch goal: SortedSet
            var sortedSet = new SortedSet<int>(list);

            // Queue vs LinkedList
            var queue = new Queue<int>();
            var linkedList = new LinkedList<int>();
            foreach (var x in list)
            {
                queue.Enqueue(x);
                linkedList.AddLast(x);
            }

            int existing = N - 1;
            int missing = -1;

            double BestOf(Action test, int repeats = 3)
            {
                double best = double.MaxValue;
                var sw = new Stopwatch();
                for (int i = 0; i < repeats; i++)
                {
                    sw.Restart();
                    test();
                    sw.Stop();
                    best = Math.Min(best, sw.Elapsed.TotalMilliseconds);
                }
                return best;
            }

            // Membership checks
            Console.WriteLine($"List.Contains({existing}):   {BestOf(() => list.Contains(existing)):F3} ms");
            Console.WriteLine($"HashSet.Contains:           {BestOf(() => hashSet.Contains(existing)):F3} ms");
            Console.WriteLine($"Dict.ContainsKey:           {BestOf(() => dict.ContainsKey(existing)):F3} ms");
            Console.WriteLine($"SortedSet.Contains:         {BestOf(() => sortedSet.Contains(existing)):F3} ms");

            Console.WriteLine($"List.Contains({missing}):   {BestOf(() => list.Contains(missing)):F3} ms");
            Console.WriteLine($"HashSet.Contains:           {BestOf(() => hashSet.Contains(missing)):F3} ms");
            Console.WriteLine($"Dict.ContainsKey:           {BestOf(() => dict.ContainsKey(missing)):F3} ms");
            Console.WriteLine($"SortedSet.Contains:         {BestOf(() => sortedSet.Contains(missing)):F3} ms");

            // Queue vs LinkedList enqueue/dequeue
            Console.WriteLine($"Queue Dequeue:              {BestOf(() => {
                var q = new Queue<int>(queue);
                while (q.Count > 0) q.Dequeue();
            }):F3} ms");

            Console.WriteLine($"LinkedList Dequeue:         {BestOf(() => {
                var ll = new LinkedList<int>(linkedList);
                while (ll.Count > 0)
                {
                    ll.RemoveFirst();
                }
            }):F3} ms");
        }
    }
}
