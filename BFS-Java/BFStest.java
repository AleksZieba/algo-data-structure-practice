import static org.junit.jupiter.api.Assertions.assertEquals;
import java.util.*;
import org.junit.jupiter.api.Test;

public class BFSExampleTest {

    /*
      Tests BFS with a simple graph.
      Graph structure:
            A
           / \
          B   C
         / \   \
        D   E   F
      Expected BFS order: [A, B, C, D, E, F]
     */
    @Test
    public void testBFSWithSimpleGraph() {
        Map<String, List<String>> graph = new HashMap<>();
        graph.put("A", Arrays.asList("B", "C"));
        graph.put("B", Arrays.asList("D", "E"));
        graph.put("C", Arrays.asList("F"));
        graph.put("D", Collections.emptyList());
        graph.put("E", Arrays.asList("F"));
        graph.put("F", Collections.emptyList());

        List<String> expectedOrder = Arrays.asList("A", "B", "C", "D", "E", "F");
        List<String> actualOrder = BFSExample.breadthFirstSearch(graph, "A");

        assertEquals(expectedOrder, actualOrder);
    }

    /*
      Tests BFS on a graph with a cycle to ensure that revisiting nodes is prevented.
      Graph structure:
              A
              |
              B
              |
              C ——→ D ——→ E
              |        
              └─────┘ (Cycle back to A)
     Expected BFS order: [A, B, C, D, E]
     */
    @Test
    public void testBFSWithCycle() {
        Map<String, List<String>> graph = new HashMap<>();
        graph.put("A", Arrays.asList("B"));
        graph.put("B", Arrays.asList("C"));
        // Node "C" has a cycle back to "A" and an edge to "D".
        graph.put("C", Arrays.asList("A", "D"));
        graph.put("D", Arrays.asList("E"));
        graph.put("E", Collections.emptyList());

        List<String> expectedOrder = Arrays.asList("A", "B", "C", "D", "E");
        List<String> actualOrder = BFSExample.breadthFirstSearch(graph, "A");

        assertEquals(expectedOrder, actualOrder);
    }

    /*
     Tests BFS with a single-node graph.
     Expected BFS order: [A]
     */
    @Test
    public void testBFSWithSingleNode() {
        Map<String, List<String>> graph = new HashMap<>();
        graph.put("A", Collections.emptyList());

        List<String> expectedOrder = Collections.singletonList("A");
        List<String> actualOrder = BFSExample.breadthFirstSearch(graph, "A");

        assertEquals(expectedOrder, actualOrder);
    }

    /*
     Tests BFS when the starting node is not present in the graph.
     Even if the node is not in the graph, our implementation enqueues the start node.
     Expected BFS order: [A]
     */
    @Test
    public void testBFSStartNodeNotInGraph() {
        Map<String, List<String>> graph = new HashMap<>();
        // Graph does not contain the starting node "A".
        graph.put("B", Arrays.asList("C"));
        graph.put("C", Collections.emptyList());

        List<String> expectedOrder = Collections.singletonList("A");
        List<String> actualOrder = BFSExample.breadthFirstSearch(graph, "A");

        assertEquals(expectedOrder, actualOrder);
    }
}