using System;
using System.Collections.Generic;

class Program
{
    // Graph represented as an adjacency list using a Dictionary.
    static Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>()
    {
        { "A", new List<string> { "B", "C" } },
        { "B", new List<string> { "D", "E" } },
        { "C", new List<string> { "F" } },
        { "D", new List<string>() },
        { "E", new List<string> { "F" } },
        { "F", new List<string>() }
    };

    /// <summary>
    /// Performs a recursive depth-first search on the graph.
    /// </summary>
    /// <param name="vertex">The starting vertex.</param>
    /// <param name="visited">A set to keep track of visited vertices.</param>
    static void DFSRecursive(string vertex, HashSet<string> visited)
    {
        // Mark the current vertex as visited
        visited.Add(vertex);
        Console.WriteLine(vertex);  // Process the vertex (e.g., print its value)

        // Recursively visit each neighbor that hasn't been visited
        foreach (var neighbor in graph[vertex])
        {
            if (!visited.Contains(neighbor))
            {
                DFSRecursive(neighbor, visited);
            }
        }
    }

    /// <summary>
    /// Performs an iterative depth-first search on the graph.
    /// </summary>
    /// <param name="start">The starting vertex.</param>
    static void DFSIterative(string start)
    {
        var visited = new HashSet<string>();
        var stack = new Stack<string>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            // Pop the last vertex from the stack
            var vertex = stack.Pop();

            // Process the vertex if it hasn't been visited yet
            if (!visited.Contains(vertex))
            {
                visited.Add(vertex);
                Console.WriteLine(vertex);

                // Push all unvisited neighbors onto the stack.
                // Pushing in reverse order helps maintain a similar order as the recursive version.
                var neighbors = graph[vertex];
                for (int i = neighbors.Count - 1; i >= 0; i--)
                {
                    if (!visited.Contains(neighbors[i]))
                    {
                        stack.Push(neighbors[i]);
                    }
                }
            }
        }
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Recursive DFS:");
        DFSRecursive("A", new HashSet<string>());

        Console.WriteLine("\nIterative DFS:");
        DFSIterative("A");
    }
}