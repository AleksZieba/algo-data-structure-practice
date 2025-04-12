using System;
using System.Collections.Generic;

class Program
{
    /// <summary>
    /// Performs a breadth-first search (BFS) on the given graph starting from the specified node.
    /// </summary>
    /// <typeparam name="T">The type of the graph node.</typeparam>
    /// <param name="graph">The graph represented as a dictionary where each key is a node and its value is a list of adjacent nodes.</param>
    /// <param name="start">The starting node for the search.</param>
    /// <returns>A list of nodes in the order they were visited.</returns>
    public static List<T> BreadthFirstSearch<T>(Dictionary<T, List<T>> graph, T start)
    {
        // This list stores the order of visited nodes.
        List<T> visitedOrder = new List<T>();

        // A HashSet to track visited nodes, so we don't process a node more than once.
        HashSet<T> visited = new HashSet<T>();

        // Queue to control the order of visiting nodes.
        Queue<T> queue = new Queue<T>();

        // Begin with the start node.
        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            // Dequeue the next node and record it.
            T current = queue.Dequeue();
            visitedOrder.Add(current);

            // If the current node has neighbors in the graph, process them.
            if (graph.TryGetValue(current, out List<T>? neighbors))
            {
                foreach (T neighbor in neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }
        }

        return visitedOrder;
    }

    static void Main(string[] args)
    {
        // Define a simple graph as a dictionary.
        // For example, the graph:
        //      A
        //     / \
        //    B   C
        //   / \   \
        //  D   E   F
        //       \
        //        F
        var graph = new Dictionary<string, List<string>>
        {
            { "A", new List<string> { "B", "C" } },
            { "B", new List<string> { "D", "E" } },
            { "C", new List<string> { "F" } },
            { "D", new List<string>() },
            { "E", new List<string> { "F" } },
            { "F", new List<string>() }
        };

        // Perform BFS starting from node "A"
        List<string> bfsResult = BreadthFirstSearch(graph, "A");

        // Output the BFS traversal order.
        Console.WriteLine("BFS Traversal: " + string.Join(", ", bfsResult));
    }
}