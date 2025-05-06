#ifndef GRAPH_H
#define GRAPH_H

#include <vector>
#include <unordered_map>

class Graph {
public:
    // If directed=false, edges are added bidirectionally
    Graph(bool directed = false);

    // Add an edge from u to v
    void addEdge(int u, int v);

    // Perform DFS starting from 'start' and return the visitation order
    std::vector<int> dfs(int start);

private:
    void dfsUtil(int u, std::unordered_map<int,bool>& visited, std::vector<int>& result);

    std::unordered_map<int, std::vector<int>> adjList;
    bool directed;
};

#endif // GRAPH_H