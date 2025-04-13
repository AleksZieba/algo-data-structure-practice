using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using RedBlackTreeImplementation; 

namespace RedBlackTreeTests
{
    public class RedBlackTreeTests
    {
        [Fact]
        public void Insert_ShouldContainInsertedValues()
        {
            // Arrange: Create a new Red Black Tree and an array of test values.
            var tree = new RedBlackTree<int>();
            int[] values = { 10, 20, 30, 15, 25, 5, 1 };

            // Act: Insert values into the tree.
            foreach (var value in values)
            {
                tree.Insert(value);
            }

            // Assert: Validate that each inserted value can be found in the tree.
            foreach (var value in values)
            {
                Assert.True(tree.Contains(value), $"Tree should contain the value {value}.");
            }

            // Assert: A value that was not inserted should not be found.
            Assert.False(tree.Contains(100), "Tree should not contain the value 100.");
        }

        [Fact]
        public void Delete_ShouldRemoveValue()
        {
            // Arrange: Create a new Red Black Tree and insert a set of values.
            var tree = new RedBlackTree<int>();
            int[] values = { 10, 20, 30, 15, 25, 5, 1 };
            foreach (var value in values)
            {
                tree.Insert(value);
            }

            // Act & Assert: Delete a leaf node and verify its removal.
            bool deletedLeaf = tree.Delete(1);
            Assert.True(deletedLeaf, "Deletion should return true for a leaf node (1).");
            Assert.False(tree.Contains(1), "The tree should not contain the value 1 after deletion.");

            // Act & Assert: Delete a node with one or two children and verify removal.
            bool deletedNode = tree.Delete(20);
            Assert.True(deletedNode, "Deletion should return true for an internal node (20).");
            Assert.False(tree.Contains(20), "The tree should not contain the value 20 after deletion.");

            // Act & Assert: Attempt to delete a non-existent value.
            bool deleteNonExistent = tree.Delete(100);
            Assert.False(deleteNonExistent, "Deletion of a non-existent value should return false.");
        }

        [Fact]
        public void InOrderTraversal_ShouldPrintSortedValues()
        {
            // Arrange: Create a new Red Black Tree and insert values.
            var tree = new RedBlackTree<int>();
            int[] values = { 10, 20, 30, 15, 25, 5, 1 };
            foreach (var value in values)
            {
                tree.Insert(value);
            }

            // Act: Capture the output of the in-order traversal.
            string output;
            using (var sw = new StringWriter())
            {
                var originalOut = Console.Out;
                Console.SetOut(sw);
                tree.InOrderTraversal();
                Console.SetOut(originalOut);
                output = sw.ToString().Trim();
            }

            // The InOrderTraversal prints each node in the format "Value(Color) " (e.g., "10(B)").
            // We extract the numeric part from each token.
            var extractedValues = new List<int>();
            string[] tokens = output.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var token in tokens)
            {
                // Find the index of the '(' character.
                int parenIndex = token.IndexOf('(');
                if (parenIndex > 0)
                {
                    string numberPart = token.Substring(0, parenIndex);
                    if (int.TryParse(numberPart, out int parsedValue))
                    {
                        extractedValues.Add(parsedValue);
                    }
                }
            }

            // Assert: The extracted numbers must be in sorted order.
            int[] expectedOrder = values.OrderBy(v => v).ToArray();
            Assert.Equal(expectedOrder.Length, extractedValues.Count);
            Assert.Equal(expectedOrder, extractedValues.ToArray());
        }
    }
}