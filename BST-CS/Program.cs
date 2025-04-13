using System;

namespace BinarySearchTreeExample
{
    // Node class representing each element in the BST.
    public class TreeNode
    {
        public int Value { get; set; }
        public TreeNode Left { get; set; }
        public TreeNode Right { get; set; }

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    // Binary Search Tree class with basic operations.
    public class BinarySearchTree
    {
        public TreeNode Root { get; private set; }

        public BinarySearchTree()
        {
            Root = null;
        }

        // Insert a value into the BST.
        public void Insert(int value)
        {
            if (Root == null)
            {
                Root = new TreeNode(value);
            }
            else
            {
                InsertRecursively(Root, value);
            }
        }

        private void InsertRecursively(TreeNode current, int value)
        {
            if (value < current.Value)
            {
                if (current.Left == null)
                    current.Left = new TreeNode(value);
                else
                    InsertRecursively(current.Left, value);
            }
            else
            {
                if (current.Right == null)
                    current.Right = new TreeNode(value);
                else
                    InsertRecursively(current.Right, value);
            }
        }

        // Search for a node with the given value.
        public TreeNode Search(int value)
        {
            return SearchRecursively(Root, value);
        }

        private TreeNode SearchRecursively(TreeNode node, int value)
        {
            if (node == null || node.Value == value)
                return node;
            if (value < node.Value)
                return SearchRecursively(node.Left, value);
            return SearchRecursively(node.Right, value);
        }

        // Delete a node with the given value from the BST.
        public void Delete(int value)
        {
            Root = DeleteRecursively(Root, value);
        }

        private TreeNode DeleteRecursively(TreeNode node, int value)
        {
            if (node == null)
                return node;

            if (value < node.Value)
            {
                node.Left = DeleteRecursively(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = DeleteRecursively(node.Right, value);
            }
            else
            {
                // Case 1: Node with no child (leaf node)
                if (node.Left == null && node.Right == null)
                {
                    return null;
                }
                // Case 2: Node with one child (only right child)
                else if (node.Left == null)
                {
                    return node.Right;
                }
                // Case 2: Node with one child (only left child)
                else if (node.Right == null)
                {
                    return node.Left;
                }
                // Case 3: Node with two children: Find the in-order successor (smallest in the right subtree)
                TreeNode successor = FindMin(node.Right);
                node.Value = successor.Value;
                node.Right = DeleteRecursively(node.Right, successor.Value);
            }
            return node;
        }

        // Find the node with the minimum value in the given subtree.
        private TreeNode FindMin(TreeNode node)
        {
            while (node.Left != null)
            {
                node = node.Left;
            }
            return node;
        }

        // In-order traversal: Left, Root, Right.
        public void InorderTraversal(TreeNode node)
        {
            if (node != null)
            {
                InorderTraversal(node.Left);
                Console.Write($"{node.Value} ");
                InorderTraversal(node.Right);
            }
        }
    }

    // Sample usage of the BinarySearchTree.
    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree bst = new BinarySearchTree();

            // Insert elements into the BST.
            bst.Insert(50);
            bst.Insert(30);
            bst.Insert(70);
            bst.Insert(20);
            bst.Insert(40);
            bst.Insert(60);
            bst.Insert(80);

            Console.WriteLine("Inorder Traversal (sorted order):");
            bst.InorderTraversal(bst.Root);
            Console.WriteLine();

            // Search for a node.
            TreeNode foundNode = bst.Search(40);
            if (foundNode != null)
            {
                Console.WriteLine($"\nNode with value 40 found: {foundNode.Value}");
            }
            else
            {
                Console.WriteLine("\nNode with value 40 not found.");
            }

            // Delete a node.
            bst.Delete(30);
            Console.WriteLine("\nInorder Traversal after deleting 30:");
            bst.InorderTraversal(bst.Root);
            Console.WriteLine();
        }
    }
}