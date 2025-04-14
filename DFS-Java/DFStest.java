import org.junit.Test;
import static org.junit.Assert.assertEquals;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

public class DFSExampleTest {

     // Helper method to build and return a sample graph.

    private DFSExample.Graph createSampleGraph() {
        DFSExample.Graph graph = new DFSExample.Graph();
        // Build the sample graph:
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
        return graph;
    }

    @Test
    public void testRecursiveDFS() {
        // Capture the console output.
        ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        PrintStream originalOut = System.out;
        System.setOut(new PrintStream(outputStream));

        // Create the graph and run the recursive DFS starting from "A".
        DFSExample.Graph graph = createSampleGraph();
        graph.dfsRecursive("A");

        // Restore the original System.out.
        System.setOut(originalOut);

        // The expected DFS order (each vertex printed on a new line)
        String expectedOutput = "A\nB\nD\nE\nF\nC\n";
        assertEquals(expectedOutput, outputStream.toString());
    }

    @Test
    public void testIterativeDFS() {
        // Capture the console output.
        ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
        PrintStream originalOut = System.out;
        System.setOut(new PrintStream(outputStream));

        // Create the graph and run the iterative DFS starting from "A".
        DFSExample.Graph graph = createSampleGraph();
        graph.dfsIterative("A");

        // Restore the original System.out.
        System.setOut(originalOut);

        // The expected DFS order (each vertex printed on a new line)
        String expectedOutput = "A\nB\nD\nE\nF\nC\n";
        assertEquals(expectedOutput, outputStream.toString());
    }
}