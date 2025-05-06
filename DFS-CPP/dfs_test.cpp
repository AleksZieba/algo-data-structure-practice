#include "graph.h"
#include <gtest/gtest.h>

TEST(DFS, SimpleTraversal) {
    Graph g(false);
    g.addEdge(1, 2);
    g.addEdge(1, 3);
    g.addEdge(2, 4);
    g.addEdge(2, 5);

    auto result = g.dfs(1);
    // Expected order given adjacency insertion: 1 -> 2 -> 4 -> 5 -> 3
    std::vector<int> expected = {1, 2, 4, 5, 3};
    EXPECT_EQ(result, expected);
}

TEST(DFS, DisconnectedGraph) {
    Graph g(false);
    g.addEdge(1, 2);
    g.addEdge(3, 4);

    auto result = g.dfs(1);
    std::vector<int> expected = {1, 2};
    EXPECT_EQ(result, expected);
}

int main(int argc, char** argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}
