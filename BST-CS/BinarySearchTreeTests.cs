using System.Collections.Generic;
using Xunit;
using BinarySearchTreeExample; // Make sure this matches the namespace of your implementation

namespace BinarySearchTreeTests
{
    public class BinarySearchTreeTests
    {
        /// <summary>
        /// Verifies that inserting several nodes returns the correct in‑order (sorted) traversal.
        /// </summary>
        [Fact]
        public void InsertionAndInorderTraversalTest()
        {
            // Arrange
            var bst = new BinarySearchTree();
            bst.Insert(50);
            bst.Insert(30);
            bst.Insert(70);
            bst.Insert(20);
            bst.Insert(40);
            bst.Insert(60);
            bst.Insert(80);

            // Act
            List<int> result = GetInorderList(bst.Root);

            // Assert
            List<int> expected = new List<int> { 20, 30, 40, 50, 60, 70, 80 };
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verifies searching for an existing value.
        /// </summary>
        [Fact]
        public void SearchTest_ExistingValue()
        {
            // Arrange
            var bst = new BinarySearchTree();
            bst.Insert(50);
            bst.Insert(30);
            bst.Insert(70);

            // Act
            TreeNode node = bst.Search(30);

            // Assert
            Assert.NotNull(node);
            Assert.Equal(30, node.Value);
        }

        /// <summary>
        /// Verifies that searching for a non‑existent value returns null.
        /// </summary>
        [Fact]
        public void SearchTest_NonExistingValue()
        {
            // Arrange
            var bst = new BinarySearchTree();
            bst.Insert(50);
            bst.Insert(30);
            bst.Insert(70);

            // Act
            TreeNode node = bst.Search(100);

            // Assert
            Assert.Null(node);
        }

        /// <summary>
        /// Verifies deletion of a leaf node.
        /// </summary>
        [Fact]
        public void DeletionTest_LeafNode()
        {
            // Arrange
            var bst = new BinarySearchTree();
            bst.Insert(50);
            bst.Insert(30);
            bst.Insert(70);
            bst.Insert(20); // Leaf node under 30
            bst.Insert(40);
            bst.Insert(60);
            bst.Insert(80);

            // Act
            bst.Delete(20);  // Delete the leaf node

            // Assert: After deletion, the in‑order traversal should not contain 20.
            List<int> result = GetInorderList(bst.Root);
            List<int> expected = new List<int> { 30, 40, 50, 60, 70, 80 };
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verifies deletion of a node with one child.
        /// </summary>
        [Fact]
        public void DeletionTest_NodeWithOneChild()
        {
            // Arrange
            var bst = new BinarySearchTree();
            bst.Insert(50);
            bst.Insert(30);
            bst.Insert(70);
            // Here, let node 30 have one child (left child only)
            bst.Insert(20);  // Only child for node 30

            // Act
            bst.Delete(30);

            // Assert: After deleting 30, 20 should take its place.
            List<int> result = GetInorderList(bst.Root);
            List<int> expected = new List<int> { 20, 50, 70 };
            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Verifies deletion of a node with two children.
        /// </summary>
        [Fact]
        public void DeletionTest_NodeWithTwoChildren()
        {
            // Arrange
            var bst = new BinarySearchTree();
            bst.Insert(50);
            bst.Insert(30);
            bst.Insert(70);
            bst.Insert(20);
            bst.Insert(40);
            bst.Insert(60);
            bst.Insert(80);

            // Act: Delete the root node (50) which has two children.
            bst.Delete(50);

            // Assert:
            List<int> result = GetInorderList(bst.Root);
            // The resulting list should not include 50.
            List<int> expected = new List<int> { 20, 30, 40, 60, 70, 80 };
            Assert.Equal(expected, result);
        }

        // ---------------------------------------------------------------------------------
        // Helper methods for the tests:
        // ---------------------------------------------------------------------------------

        /// <summary>
        /// Performs an in‑order traversal on the BST and returns the node values in a list.
        /// </summary>
        /// <param name="node">The current node of the BST.</param>
        /// <returns>A list of integers representing the in‑order traversal of the BST.</returns>
        private List<int> GetInorderList(TreeNode node)
        {
            List<int> result = new List<int>();
            InorderTraversal(node, result);
            return result;
        }

        /// <summary>
        /// Recursive helper method to perform in‑order traversal.
        /// </summary>
        /// <param name="node">Current node in the BST.</param>
        /// <param name="result">The list collecting the node values.</param>
        private void InorderTraversal(TreeNode node, List<int> result)
        {
            if (node == null)
            {
                return;
            }
            InorderTraversal(node.Left, result);
            result.Add(node.Value);
            InorderTraversal(node.Right, result);
        }
    }
}