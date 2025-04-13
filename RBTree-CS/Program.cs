using System;

namespace RedBlackTreeImplementation
{
    /// <summary>
    /// Enumeration representing node colors.
    /// </summary>
    public enum NodeColor
    {
        Red,
        Black
    }

    /// <summary>
    /// Node class for the Red Black Tree.
    /// </summary>
    /// <typeparam name="T">Generic type that must implement IComparable.</typeparam>
    public class RedBlackNode<T> where T : IComparable<T>
    {
        public T Value;
        public NodeColor Color;
        public RedBlackNode<T> Left;
        public RedBlackNode<T> Right;
        public RedBlackNode<T> Parent;

        public RedBlackNode(T value)
        {
            Value = value;
            // New nodes are inserted as red by default.
            Color = NodeColor.Red;
            Left = null;
            Right = null;
            Parent = null;
        }
    }

    /// <summary>
    /// Implementation of a Red Black Tree.
    /// </summary>
    /// <typeparam name="T">Generic type that must implement IComparable.</typeparam>
    public class RedBlackTree<T> where T : IComparable<T>
    {
        private RedBlackNode<T> root;

        public RedBlackTree()
        {
            root = null;
        }

        /// <summary>
        /// Inserts a new value into the tree.
        /// </summary>
        public void Insert(T value)
        {
            RedBlackNode<T> node = new RedBlackNode<T>(value);
            RedBlackNode<T> y = null;
            RedBlackNode<T> x = root;

            // Standard BST insertion.
            while (x != null)
            {
                y = x;
                if (node.Value.CompareTo(x.Value) < 0)
                {
                    x = x.Left;
                }
                else
                {
                    x = x.Right;
                }
            }

            node.Parent = y;
            if (y == null)
            {
                // The tree was empty; new node becomes the root.
                root = node;
            }
            else if (node.Value.CompareTo(y.Value) < 0)
            {
                y.Left = node;
            }
            else
            {
                y.Right = node;
            }

            // Fix the tree to maintain Red Black properties.
            InsertFixup(node);
        }

        /// <summary>
        /// Restores the Red Black properties after insertion.
        /// </summary>
        private void InsertFixup(RedBlackNode<T> node)
        {
            while (node != root && node.Parent != null && node.Parent.Color == NodeColor.Red)
            {
                RedBlackNode<T> parent = node.Parent;
                RedBlackNode<T> grandparent = parent.Parent;

                if (grandparent == null)
                    break; // Should not occur if the tree is correctly maintained.

                // Parent is a left child of grandparent.
                if (parent == grandparent.Left)
                {
                    RedBlackNode<T> uncle = grandparent.Right;
                    // Case 1: Uncle is red.
                    if (uncle != null && uncle.Color == NodeColor.Red)
                    {
                        parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        grandparent.Color = NodeColor.Red;
                        node = grandparent;
                    }
                    else
                    {
                        // Case 2: Node is right child.
                        if (node == parent.Right)
                        {
                            node = parent;
                            RotateLeft(node);
                        }
                        // Case 3: Node is left child.
                        node.Parent.Color = NodeColor.Black;
                        grandparent.Color = NodeColor.Red;
                        RotateRight(grandparent);
                    }
                }
                // Parent is a right child of grandparent (mirror case).
                else
                {
                    RedBlackNode<T> uncle = grandparent.Left;
                    // Case 1 mirror: Uncle is red.
                    if (uncle != null && uncle.Color == NodeColor.Red)
                    {
                        parent.Color = NodeColor.Black;
                        uncle.Color = NodeColor.Black;
                        grandparent.Color = NodeColor.Red;
                        node = grandparent;
                    }
                    else
                    {
                        // Case 2 mirror: Node is left child.
                        if (node == parent.Left)
                        {
                            node = parent;
                            RotateRight(node);
                        }
                        // Case 3 mirror: Node is right child.
                        node.Parent.Color = NodeColor.Black;
                        grandparent.Color = NodeColor.Red;
                        RotateLeft(grandparent);
                    }
                }
            }
            // Ensure the root is always black.
            root.Color = NodeColor.Black;
        }

        /// <summary>
        /// Left rotation around the given node.
        /// </summary>
        public void RotateLeft(RedBlackNode<T> node)
        {
            if (node == null)
                return;

            RedBlackNode<T> y = node.Right;
            if (y == null)
                return; // Cannot perform left rotation without a right child.

            node.Right = y.Left;
            if (y.Left != null)
            {
                y.Left.Parent = node;
            }

            y.Parent = node.Parent;
            if (node.Parent == null)
            {
                root = y;
            }
            else if (node == node.Parent.Left)
            {
                node.Parent.Left = y;
            }
            else
            {
                node.Parent.Right = y;
            }

            y.Left = node;
            node.Parent = y;
        }

        /// <summary>
        /// Right rotation around the given node.
        /// </summary>
        public void RotateRight(RedBlackNode<T> node)
        {
            if (node == null)
                return;

            RedBlackNode<T> y = node.Left;
            if (y == null)
                return; // Cannot perform right rotation without a left child.

            node.Left = y.Right;
            if (y.Right != null)
            {
                y.Right.Parent = node;
            }

            y.Parent = node.Parent;
            if (node.Parent == null)
            {
                root = y;
            }
            else if (node == node.Parent.Right)
            {
                node.Parent.Right = y;
            }
            else
            {
                node.Parent.Left = y;
            }

            y.Right = node;
            node.Parent = y;
        }

        /// <summary>
        /// Searches for a value in the tree.
        /// </summary>
        public bool Contains(T value)
        {
            return FindNode(root, value) != null;
        }

        /// <summary>
        /// Helper method to search for a node with the specified value.
        /// </summary>
        private RedBlackNode<T> FindNode(RedBlackNode<T> node, T value)
        {
            if (node == null)
                return null;

            int cmp = value.CompareTo(node.Value);
            if (cmp == 0)
                return node;
            else if (cmp < 0)
                return FindNode(node.Left, value);
            else
                return FindNode(node.Right, value);
        }

        /// <summary>
        /// Deletes a value from the tree.
        /// Returns true if deletion was successful.
        /// </summary>
        public bool Delete(T value)
        {
            RedBlackNode<T> node = FindNode(root, value);
            if (node == null)
                return false;

            DeleteNode(node);
            return true;
        }

        /// <summary>
        /// Deletes the given node from the tree and restores tree properties.
        /// </summary>
        private void DeleteNode(RedBlackNode<T> z)
        {
            RedBlackNode<T> y = z;
            NodeColor yOriginalColor = y.Color;
            RedBlackNode<T> x; // x is the node that moves into y's original position.
            RedBlackNode<T> xParent; // Track parent in case x is null

            if (z.Left == null)
            {
                x = z.Right;
                xParent = z.Parent;
                Transplant(z, z.Right);
            }
            else if (z.Right == null)
            {
                x = z.Left;
                xParent = z.Parent;
                Transplant(z, z.Left);
            }
            else
            {
                // z has two children; find its in-order successor.
                y = Minimum(z.Right);
                yOriginalColor = y.Color;
                x = y.Right;
                if (y.Parent == z)
                {
                    if (x != null)
                        x.Parent = y;
                    xParent = y;
                }
                else
                {
                    Transplant(y, y.Right);
                    y.Right = z.Right;
                    if (y.Right != null)
                        y.Right.Parent = y;
                    xParent = y.Parent;
                }
                Transplant(z, y);
                y.Left = z.Left;
                if (y.Left != null)
                    y.Left.Parent = y;
                y.Color = z.Color;
            }
            // If the removed node was black, fix the tree.
            if (yOriginalColor == NodeColor.Black)
            {
                DeleteFixup(x, xParent);
            }
        }

        /// <summary>
        /// Replaces one subtree as a child of its parent with another subtree.
        /// </summary>
        private void Transplant(RedBlackNode<T> u, RedBlackNode<T> v)
        {
            if (u.Parent == null)
            {
                root = v;
            }
            else if (u == u.Parent.Left)
            {
                u.Parent.Left = v;
            }
            else
            {
                u.Parent.Right = v;
            }
            if (v != null)
            {
                v.Parent = u.Parent;
            }
        }

        /// <summary>
        /// Restores the Red Black properties after deletion.
        /// </summary>
        /// <param name="x">The node that has moved into y's original location (may be null).</param>
        /// <param name="xParent">The parent of x (used when x is null).</param>
        private void DeleteFixup(RedBlackNode<T> x, RedBlackNode<T> xParent)
        {
            // When x is null, we use xParent to determine context.
            while ((x != root) && ((x == null) || (x.Color == NodeColor.Black)))
            {
                if (x == xParent.Left)
                {
                    RedBlackNode<T> w = xParent.Right;
                    if (w != null && w.Color == NodeColor.Red)
                    {
                        // Case 1: Sibling is red.
                        w.Color = NodeColor.Black;
                        xParent.Color = NodeColor.Red;
                        RotateLeft(xParent);
                        w = xParent.Right;
                    }
                    if ((w == null) ||
                        ((w.Left == null || w.Left.Color == NodeColor.Black) &&
                         (w.Right == null || w.Right.Color == NodeColor.Black)))
                    {
                        // Case 2: Sibling's children are black.
                        if (w != null)
                            w.Color = NodeColor.Red;
                        x = xParent;
                        xParent = xParent.Parent;
                    }
                    else
                    {
                        if (w.Right == null || w.Right.Color == NodeColor.Black)
                        {
                            // Case 3: Sibling's left child is red and right child is black.
                            if (w.Left != null)
                                w.Left.Color = NodeColor.Black;
                            w.Color = NodeColor.Red;
                            RotateRight(w);
                            w = xParent.Right;
                        }
                        // Case 4: Sibling's right child is red.
                        if (w != null)
                            w.Color = xParent.Color;
                        xParent.Color = NodeColor.Black;
                        if (w != null && w.Right != null)
                            w.Right.Color = NodeColor.Black;
                        RotateLeft(xParent);
                        x = root;
                        break;
                    }
                }
                else
                {
                    // Mirror of the above code.
                    RedBlackNode<T> w = xParent.Left;
                    if (w != null && w.Color == NodeColor.Red)
                    {
                        w.Color = NodeColor.Black;
                        xParent.Color = NodeColor.Red;
                        RotateRight(xParent);
                        w = xParent.Left;
                    }
                    if ((w == null) ||
                        ((w.Right == null || w.Right.Color == NodeColor.Black) &&
                         (w.Left == null || w.Left.Color == NodeColor.Black)))
                    {
                        if (w != null)
                            w.Color = NodeColor.Red;
                        x = xParent;
                        xParent = xParent.Parent;
                    }
                    else
                    {
                        if (w.Left == null || w.Left.Color == NodeColor.Black)
                        {
                            if (w.Right != null)
                                w.Right.Color = NodeColor.Black;
                            w.Color = NodeColor.Red;
                            RotateLeft(w);
                            w = xParent.Left;
                        }
                        if (w != null)
                            w.Color = xParent.Color;
                        xParent.Color = NodeColor.Black;
                        if (w != null && w.Left != null)
                            w.Left.Color = NodeColor.Black;
                        RotateRight(xParent);
                        x = root;
                        break;
                    }
                }
            }
            if (x != null)
                x.Color = NodeColor.Black;
        }

        /// <summary>
        /// Finds the node with the minimum value in the subtree.
        /// </summary>
        private RedBlackNode<T> Minimum(RedBlackNode<T> node)
        {
            while (node.Left != null)
                node = node.Left;
            return node;
        }

        /// <summary>
        /// Performs an in-order traversal of the tree and prints each node’s value and color.
        /// </summary>
        public void InOrderTraversal()
        {
            InOrderHelper(root);
            Console.WriteLine();
        }

        private void InOrderHelper(RedBlackNode<T> node)
        {
            if (node != null)
            {
                InOrderHelper(node.Left);
                // Print value and color (R for red, B for black)
                Console.Write($"{node.Value}({(node.Color == NodeColor.Red ? "R" : "B")}) ");
                InOrderHelper(node.Right);
            }
        }
    }

    /// <summary>
    /// Example usage of the Red Black Tree implementation.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // Create a new Red Black Tree for integers.
            RedBlackTree<int> rbt = new RedBlackTree<int>();

            int[] valuesToInsert = { 10, 20, 30, 15, 25, 5, 1 };
            Console.WriteLine("Inserting values into the Red Black Tree:");
            foreach (var value in valuesToInsert)
            {
                rbt.Insert(value);
                rbt.InOrderTraversal();
            }

            // Example search for a value.
            int searchValue = 15;
            Console.WriteLine($"Does the tree contain {searchValue}? {rbt.Contains(searchValue)}");

            // Example deletion.
            Console.WriteLine("Deleting 20 from the tree.");
            bool deleted = rbt.Delete(20);
            Console.WriteLine("Deletion successful? " + deleted);
            rbt.InOrderTraversal();

            // Keep console window open.
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}