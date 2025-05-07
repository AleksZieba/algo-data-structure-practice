#ifndef RED_BLACK_TREE_IMPL
#define RED_BLACK_TREE_IMPL

#include "red_black_tree.h"
#include <iostream>

// Node constructor
template <typename T>
RedBlackTree<T>::Node::Node(const T& value)
    : data(value), color(RED), parent(nullptr), left(nullptr), right(nullptr) {}

// Constructor
template <typename T>
RedBlackTree<T>::RedBlackTree() {
    TNULL = new Node(T());
    TNULL->color = BLACK;
    TNULL->left = nullptr;
    TNULL->right = nullptr;
    root = TNULL;
}

// Destructor
template <typename T>
RedBlackTree<T>::~RedBlackTree() {
    // TODO: implement tree cleanup
}

// Initialize NULL node
template <typename T>
void RedBlackTree<T>::initializeNULLNode(Node* node, Node* parent) {
    node->data = T();
    node->color = BLACK;
    node->parent = parent;
    node->left = nullptr;
    node->right = nullptr;
}

// Search helper
template <typename T>
typename RedBlackTree<T>::Node* RedBlackTree<T>::searchTreeHelper(Node* node, const T& key) const {
    if (node == TNULL || key == node->data) {
        return node;
    }
    if (key < node->data) {
        return searchTreeHelper(node->left, key);
    }
    return searchTreeHelper(node->right, key);
}

// Public contains
template <typename T>
bool RedBlackTree<T>::contains(const T& value) const {
    return searchTreeHelper(root, value) != TNULL;
}

// Left rotate
template <typename T>
void leftRotate(RedBlackTree<T>* tree, typename RedBlackTree<T>::Node* x) {
    auto y = x->right;
    x->right = y->left;
    if (y->left != tree->TNULL) {
        y->left->parent = x;
    }
    y->parent = x->parent;
    if (x->parent == nullptr) {
        tree->root = y;
    } else if (x == x->parent->left) {
        x->parent->left = y;
    } else {
        x->parent->right = y;
    }
    y->left = x;
    x->parent = y;
}

// Right rotate
template <typename T>
void rightRotate(RedBlackTree<T>* tree, typename RedBlackTree<T>::Node* x) {
    auto y = x->left;
    x->left = y->right;
    if (y->right != tree->TNULL) {
        y->right->parent = x;
    }
    y->parent = x->parent;
    if (x->parent == nullptr) {
        tree->root = y;
    } else if (x == x->parent->right) {
        x->parent->right = y;
    } else {
        x->parent->left = y;
    }
    y->right = x;
    x->parent = y;
}

// Insert fix
template <typename T>
void RedBlackTree<T>::fixInsert(Node* k) {
    Node* u;
    while (k->parent->color == RED) {
        if (k->parent == k->parent->parent->right) {
            u = k->parent->parent->left;
            if (u->color == RED) {
                u->color = BLACK;
                k->parent->color = BLACK;
                k->parent->parent->color = RED;
                k = k->parent->parent;
            } else {
                if (k == k->parent->left) {
                    k = k->parent;
                    rightRotate(this, k);
                }
                k->parent->color = BLACK;
                k->parent->parent->color = RED;
                leftRotate(this, k->parent->parent);
            }
        } else {
            u = k->parent->parent->right;
            if (u->color == RED) {
                u->color = BLACK;
                k->parent->color = BLACK;
                k->parent->parent->color = RED;
                k = k->parent->parent;
            } else {
                if (k == k->parent->right) {
                    k = k->parent;
                    leftRotate(this, k);
                }
                k->parent->color = BLACK;
                k->parent->parent->color = RED;
                rightRotate(this, k->parent->parent);
            }
        }
        if (k == root) break;
    }
    root->color = BLACK;
}

// Public insert
template <typename T>
void RedBlackTree<T>::insert(const T& value) {
    Node* node = new Node(value);
    node->left = TNULL;
    node->right = TNULL;

    Node* y = nullptr;
    Node* x = root;

    while (x != TNULL) {
        y = x;
        if (node->data < x->data) {
            x = x->left;
        } else {
            x = x->right;
        }
    }

    node->parent = y;
    if (y == nullptr) {
        root = node;
    } else if (node->data < y->data) {
        y->left = node;
    } else {
        y->right = node;
    }

    if (node->parent == nullptr) {
        node->color = BLACK;
        return;
    }

    if (node->parent->parent == nullptr) {
        return;
    }

    fixInsert(node);
}

// Transplant
template <typename T>
void RedBlackTree<T>::rbTransplant(Node* u, Node* v) {
    if (u->parent == nullptr) {
        root = v;
    } else if (u == u->parent->left) {
        u->parent->left = v;
    } else {
        u->parent->right = v;
    }
    v->parent = u->parent;
}

// Minimum
template <typename T>
typename RedBlackTree<T>::Node* RedBlackTree<T>::minimum(Node* node) const {
    while (node->left != TNULL) {
        node = node->left;
    }
    return node;
}

// Remove fix
template <typename T>
void RedBlackTree<T>::fixRemove(Node* x) {
    Node* s;
    while (x != root && x->color == BLACK) {
        if (x == x->parent->left) {
            s = x->parent->right;
            if (s->color == RED) {
                s->color = BLACK;
                x->parent->color = RED;
                leftRotate(this, x->parent);
                s = x->parent->right;
            }
            if (s->left->color == BLACK && s->right->color == BLACK) {
                s->color = RED;
                x = x->parent;
            } else {
                if (s->right->color == BLACK) {
                    s->left->color = BLACK;
                    s->color = RED;
                    rightRotate(this, s);
                    s = x->parent->right;
                }
                s->color = x->parent->color;
                x->parent->color = BLACK;
                s->right->color = BLACK;
                leftRotate(this, x->parent);
                x = root;
            }
        } else {
            s = x->parent->left;
            if (s->color == RED) {
                s->color = BLACK;
                x->parent->color = RED;
                rightRotate(this, x->parent);
                s = x->parent->left;
            }
            if (s->right->color == BLACK && s->left->color == BLACK) {
                s->color = RED;
                x = x->parent;
            } else {
                if (s->left->color == BLACK) {
                    s->right->color = BLACK;
                    s->color = RED;
                    leftRotate(this, s);
                    s = x->parent->left;
                }
                s->color = x->parent->color;
                x->parent->color = BLACK;
                s->left->color = BLACK;
                rightRotate(this, x->parent);
                x = root;
            }
        }
    }
    x->color = BLACK;
}

// Public remove
template <typename T>
void RedBlackTree<T>::remove(const T& value) {
    Node* z = searchTreeHelper(root, value);
    if (z == TNULL) return;

    Node* y = z;
    Color y_original_color = y->color;
    Node* x;

    if (z->left == TNULL) {
        x = z->right;
        rbTransplant(z, z->right);
    } else if (z->right == TNULL) {
        x = z->left;
        rbTransplant(z, z->left);
    } else {
        y = minimum(z->right);
        y_original_color = y->color;
        x = y->right;
        if (y->parent == z) {
            x->parent = y;
        } else {
            rbTransplant(y, y->right);
            y->right = z->right;
            y->right->parent = y;
        }
        rbTransplant(z, y);
        y->left = z->left;
        y->left->parent = y;
        y->color = z->color;
    }
    delete z;
    if (y_original_color == BLACK) {
        fixRemove(x);
    }
}

#endif // RED_BLACK_TREE_IMPL