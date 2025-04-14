import java.util.*;

public class DFSExample {
    
    // Graph class represents the graph using an adjacency list.
    static class Graph {
        private Map<String, List<String>> adjList;

        public Graph() {
            this.adjList = new HashMap<>();
        }

        // Adds a directed edge from source to destination.
        public void addEdge(String source, String destination) {
            adjList.computeIfAbsent(source, k -> new ArrayList<>()).add(destination);
            // Optionally, if the graph is undirected, you can add the reverse edge:
            // adjList.computeIfAbsent(destination, k -> new ArrayList<>()).add(source);
        }

        // Recursive DFS helper function
        private void dfsRecursiveUtil(String vertex, Set<String> visited) {
            // Mark the vertex as visited and process it (here, we print it)
            visited.add(vertex);
            System.out.println(vertex);

            // Recurse for all the adjacent vertices
            List<String> neighbors = adjList.get(vertex);
            if (neighbors != null) {
                for (String neighbor : neighbors) {
                    if (!visited.contains(neighbor)) {
                        dfsRecursiveUtil(neighbor, visited);
                    }
                }
            }
        }

        // Public method to start recursive DFS
        public void dfsRecursive(String start) {
            Set<String> visited = new HashSet<>();
            dfsRecursiveUtil(start, visited);
        }

        // Iterative DFS using a stack
        public void dfsIterative(String start) {
            Set<String> visited = new HashSet<>();
            Stack<String> stack = new Stack<>();
            stack.push(start);

            while (!stack.isEmpty()) {
                String vertex = stack.pop();
                if (!visited.contains(vertex)) {
                    visited.add(vertex);
                    System.out.println(vertex);

                    // Add all adjacent vertices to the stack.
                    // Pushing in reverse order can preserve a similar processing order as recursive DFS.
                    List<String> neighbors = adjList.get(vertex);
                    if (neighbors != null) {
                        for (int i = neighbors.size() - 1; i >= 0; i--) {
                            String neighbor = neighbors.get(i);
                            if (!visited.contains(neighbor)) {
                                stack.push(neighbor);
                            }
                        }
                    }
                }
            }
        }
    }

    // Main method to demonstrate DFS implementations.
    public static void main(String[] args) {
        Graph graph = new Graph();

        // Build a sample graph:
        // A -> B, C
        // B -> D, E
        // C -> F
        // E -> F
        graph.addEdge("A", "B");
        graph.addEdge("A", "C");
        graph.addEdge("B", "D");
        graph.addEdge("B", "E");
        graph.addEdge("C", "F");
        graph.addEdge("E", "F");

        System.out.println("Recursive DFS:");
        graph.dfsRecursive("A");

        System.out.println("\nIterative DFS:");
        graph.dfsIterative("A");
    }
}