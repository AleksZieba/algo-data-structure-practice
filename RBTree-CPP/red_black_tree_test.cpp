#include "gtest/gtest.h"
#include "red_black_tree.h"

TEST(RedBlackTreeTest, InsertAndContains) {
    RedBlackTree<int> tree;
    EXPECT_FALSE(tree.contains(5));
    tree.insert(5);
    EXPECT_TRUE(tree.contains(5));
    tree.insert(3);
    tree.insert(7);
    EXPECT_TRUE(tree.contains(3));
    EXPECT_TRUE(tree.contains(7));
}

TEST(RedBlackTreeTest, Remove) {
    RedBlackTree<int> tree;
    tree.insert(10);
    tree.insert(20);
    tree.insert(30);
    EXPECT_TRUE(tree.contains(20));
    tree.remove(20);
    EXPECT_FALSE(tree.contains(20));
    EXPECT_TRUE(tree.contains(10));
    EXPECT_TRUE(tree.contains(30));
}

TEST(RedBlackTreeTest, MultipleOperations) {
    RedBlackTree<int> tree;
    for (int i = 0; i < 100; ++i) {
        tree.insert(i);
    }
    for (int i = 0; i < 100; i += 2) {
        tree.remove(i);
    }
    for (int i = 0; i < 100; ++i) {
        if (i % 2 == 0) EXPECT_FALSE(tree.contains(i));
        else EXPECT_TRUE(tree.contains(i));
    }
}

int main(int argc, char **argv) {
    ::testing::InitGoogleTest(&argc, argv);
    return RUN_ALL_TESTS();
}