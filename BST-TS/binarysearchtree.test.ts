import { BinarySearchTree } from './BinarySearchTree';

describe("BinarySearchTree", () => {
  let bst: BinarySearchTree;

  beforeEach(() => {
    bst = new BinarySearchTree();
  });

  test("insertion and in-order traversal", () => {
    bst.insert(50);
    bst.insert(30);
    bst.insert(70);
    bst.insert(20);
    bst.insert(40);
    bst.insert(60);
    bst.insert(80);
    
    // Expect the in-order traversal to produce a sorted array.
    expect(bst.inorderTraversal()).toEqual([20, 30, 40, 50, 60, 70, 80]);
  });

  test("searching for an existing value", () => {
    bst.insert(50);
    bst.insert(30);
    bst.insert(70);

    const node = bst.search(30);
    expect(node).not.toBeNull();
    if (node) {
      expect(node.value).toBe(30);
    }
  });

  test("searching for a non-existing value", () => {
    bst.insert(50);
    bst.insert(30);
    bst.insert(70);

    expect(bst.search(100)).toBeNull();
  });

  test("deletion of a leaf node", () => {
    bst.insert(50);
    bst.insert(30);
    bst.insert(70);
    bst.insert(20); // Leaf node

    bst.delete(20);
    expect(bst.inorderTraversal()).toEqual([30, 50, 70]);
  });

  test("deletion of a node with one child", () => {
    bst.insert(50);
    bst.insert(30);
    bst.insert(70);
    bst.insert(20); // Leaf under 30
    bst.insert(40); // Leaf under 30

    bst.delete(30);
    // After deleting 30, 20 and 40 should be repositioned accordingly.
    expect(bst.inorderTraversal()).toEqual([20, 40, 50, 70]);
  });

  test("deletion of a node with two children", () => {
    bst.insert(50);
    bst.insert(30);
    bst.insert(70);
    bst.insert(20);
    bst.insert(40);
    bst.insert(60);
    bst.insert(80);

    bst.delete(50);
    // In-order traversal should now produce a sorted array without 50.
    expect(bst.inorderTraversal()).toEqual([20, 30, 40, 60, 70, 80]);
  });
});