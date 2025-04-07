/**
 * @param graph - A Map where each key is a node and its value is an array of adjacent nodes.
 * @param start - The starting node for BFS.
 * @returns An array of nodes in the order they were visited.
 */

function bfs<T>(graph: Map<T, T[]>, start: T): T[] {
    const result: T[] = [];           // To store the BFS order
    const visited = new Set<T>();     // To keep track of visited nodes
    const queue: T[] = [];            // Queue for the BFS
  
    // Initialize by marking the start node as visited and enqueue it
    visited.add(start);
    queue.push(start);
  
    while (queue.length > 0) {
      // Dequeue the next node from the front of the queue
      const current = queue.shift()!;
      result.push(current);
  
      // Get all neighbors for the current node (or an empty array if none)
      const neighbors = graph.get(current) || [];
      for (const neighbor of neighbors) {
        if (!visited.has(neighbor)) {
          visited.add(neighbor);
          queue.push(neighbor);
        }
      }
    }
    return result;
  }
  
  // Example usage:
  
  // Define a graph where the keys are nodes and values are arrays of adjacent nodes
  const graph = new Map<string, string[]>([
    ['A', ['B', 'C']],
    ['B', ['D', 'E']],
    ['C', ['F']],
    ['D', []],
    ['E', ['F']],
    ['F', []]
  ]);
  
  console.log(bfs(graph, 'A')); // Expected output: ['A', 'B', 'C', 'D', 'E', 'F']