#ifndef RED_BLACK_TREE_H
#define RED_BLACK_TREE_H

#include <memory>

enum Color { RED, BLACK };

template <typename T>
class RedBlackTree {
public:
    RedBlackTree();
    ~RedBlackTree();

    void insert(const T& value);
    bool contains(const T& value) const;
    void remove(const T& value);

private:
    struct Node {
        T data;
        Color color;
        Node* parent;
        Node* left;
        Node* right;
        Node(const T& value);
    };

    Node* root;
    Node* TNULL;

    void initializeNULLNode(Node* node, Node* parent);
    void preOrderHelper(Node* node) const;
    Node* searchTreeHelper(Node* node, const T& key) const;
    void fixInsert(Node* k);
    void rbTransplant(Node* u, Node* v);
    void fixRemove(Node* x);
    Node* minimum(Node* node) const;

public:
    // For testing
    Node* getRoot() const { return root; }
};

#include "red_black_tree.hpp"

#endif // RED_BLACK_TREE_H