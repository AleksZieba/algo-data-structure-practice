#include "graph.h"

Graph::Graph(bool directed) : directed(directed) {}

void Graph::addEdge(int u, int v) {
    adjList[u].push_back(v);
    if (!directed) {
        adjList[v].push_back(u);
    }
}

std::vector<int> Graph::dfs(int start) {
    std::unordered_map<int,bool> visited;
    std::vector<int> result;
    dfsUtil(start, visited, result);
    return result;
}

void Graph::dfsUtil(int u, std::unordered_map<int,bool>& visited, std::vector<int>& result) {
    visited[u] = true;
    result.push_back(u);
    for (int v : adjList[u]) {
        if (!visited[v]) {
            dfsUtil(v, visited, result);
        }
    }
}