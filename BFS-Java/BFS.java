import java.util.*;

public class BFSExample {

    /**
     * Performs a Breadth-First Search (BFS) on the given graph starting from the specified node.
     *
     * @param graph The graph represented as a map where each key is a node and the corresponding
     *              value is a list of adjacent nodes.
     * @param start The starting node for the BFS.
     * @param <T>   The type of the nodes.
     * @return A list of nodes in the order they were visited.
     */
    public static <T> List<T> breadthFirstSearch(Map<T, List<T>> graph, T start) {
        // List to store the order in which nodes are visited.
        List<T> visitedOrder = new ArrayList<>();
        // Set to keep track of visited nodes to avoid processing a node more than once.
        Set<T> visited = new HashSet<>();
        // Queue to control the order of node processing (FIFO).
        Queue<T> queue = new LinkedList<>();

        // Begin with the starting node.
        visited.add(start);
        queue.add(start);

        // Continue processing until there are no more nodes in the queue.
        while (!queue.isEmpty()) {
            // Remove the node at the front of the queue.
            T current = queue.poll();
            visitedOrder.add(current);

            // Retrieve the neighbors of the current node.
            // If the node has no entry in the graph, return an empty list.
            List<T> neighbors = graph.getOrDefault(current, new ArrayList<>());
            for (T neighbor : neighbors) {
                // If the neighbor hasn't been visited yet, mark it as visited and add it to the queue.
                if (!visited.contains(neighbor)) {
                    visited.add(neighbor);
                    queue.add(neighbor);
                }
            }
        }
        return visitedOrder;
    }

    public static void main(String[] args) {
        // Define an example graph:
        //      A
        //     / \
        //    B   C
        //   / \   \
        //  D   E   F
        Map<String, List<String>> graph = new HashMap<>();
        graph.put("A", Arrays.asList("B", "C"));
        graph.put("B", Arrays.asList("D", "E"));
        graph.put("C", Arrays.asList("F"));
        graph.put("D", Collections.emptyList());
        graph.put("E", Arrays.asList("F"));
        graph.put("F", Collections.emptyList());

        // Perform BFS starting at node "A"
        List<String> bfsResult = breadthFirstSearch(graph, "A");
        System.out.println("BFS Traversal: " + bfsResult);
    }
}