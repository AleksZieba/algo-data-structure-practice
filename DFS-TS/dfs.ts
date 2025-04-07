/**
 * @param graph - The graph represented as an adjacency list.
 * @param start - The starting vertex.
 */
function dfsIterative(graph: Graph, start: string): void {
    const visited: Set<string> = new Set();
    const stack: string[] = [start];
  
    while (stack.length > 0) {
      // Pop the last vertex off the stack
      const vertex = stack.pop()!;
      
      // Process the vertex if it hasn't been visited
      if (!visited.has(vertex)) {
        console.log(vertex);
        visited.add(vertex);
  
        // Add all unvisited neighbors to the stack.
        // Iterating in reverse order ensures the same order as the recursive version.
        const neighbors = graph[vertex];
        for (let i = neighbors.length - 1; i >= 0; i--) {
          if (!visited.has(neighbors[i])) {
            stack.push(neighbors[i]);
          }
        }
      }
    }
  }
  
  console.log("\nIterative DFS:");
  dfsIterative(graph, "A");