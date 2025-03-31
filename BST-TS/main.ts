// Define the node class for the tree
class TreeNode {
    value: number;
    left: TreeNode | null;
    right: TreeNode | null;
  
    constructor(value: number) {
      this.value = value;
      this.left = null;
      this.right = null;
    }
  }
  
  // Define the binary search tree class
  class BinarySearchTree {
    root: TreeNode | null;
  
    constructor() {
      this.root = null;
    }
  
    // Insert a new value into the BST
    insert(value: number): void {
      const newNode = new TreeNode(value);
      if (!this.root) {
        this.root = newNode;
        return;
      }
      let current = this.root;
      while (true) {
        // If the value is less, go left
        if (value < current.value) {
          if (current.left === null) {
            current.left = newNode;
            return;
          }
          current = current.left;
        } else {
          // If the value is greater or equal, go right
          if (current.right === null) {
            current.right = newNode;
            return;
          }
          current = current.right;
        }
      }
    }
  
    // Search for a node with a given value
    search(value: number): TreeNode | null {
      let current = this.root;
      while (current !== null) {
        if (value === current.value) {
          return current;
        } else if (value < current.value) {
          current = current.left;
        } else {
          current = current.right;
        }
      }
      return null;
    }
  
    // Delete a node with a given value from the BST
    delete(value: number): void {
      this.root = this.deleteNode(this.root, value);
    }
  
    private deleteNode(node: TreeNode | null, value: number): TreeNode | null {
      if (node === null) return null;
  
      if (value < node.value) {
        node.left = this.deleteNode(node.left, value);
      } else if (value > node.value) {
        node.right = this.deleteNode(node.right, value);
      } else {
        // Node found - handle the three cases
  
        // Case 1: No child (leaf node)
        if (node.left === null && node.right === null) {
          return null;
        }
        // Case 2: One child (right child only)
        else if (node.left === null) {
          return node.right;
        }
        // Case 2: One child (left child only)
        else if (node.right === null) {
          return node.left;
        }
        // Case 3: Two children
        else {
          // Find the inorder successor (smallest in the right subtree)
          const minRight = this.findMin(node.right);
          // Replace node value with the inorder successor's value
          node.value = minRight.value;
          // Delete the inorder successor
          node.right = this.deleteNode(node.right, minRight.value);
        }
      }
      return node;
    }
  
    // Helper function to find the minimum value node in a subtree
    private findMin(node: TreeNode): TreeNode {
      while (node.left !== null) {
        node = node.left;
      }
      return node;
    }
  
    // In-order traversal: Left, Root, Right
    inorderTraversal(node: TreeNode | null = this.root, result: number[] = []): number[] {
      if (node !== null) {
        this.inorderTraversal(node.left, result);
        result.push(node.value);
        this.inorderTraversal(node.right, result);
      }
      return result;
    }
  }
  
  // Example usage:
  const bst = new BinarySearchTree();
  bst.insert(50);
  bst.insert(30);
  bst.insert(70);
  bst.insert(20);
  bst.insert(40);
  bst.insert(60);
  bst.insert(80);
  
  console.log("In-order Traversal:", bst.inorderTraversal()); // [20, 30, 40, 50, 60, 70, 80]
  console.log("Search for 40:", bst.search(40)); // TreeNode { value: 40, left: null, right: null }
  
  // Deleting a node
  bst.delete(30);
  console.log("In-order Traversal after deleting 30:", bst.inorderTraversal());