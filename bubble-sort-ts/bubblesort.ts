function bubbleSort(arr: number[]): number[] {
    let n = arr.length;
    // Loop through each element in the array
    for (let i = 0; i < n - 1; i++) {
      // Last i elements are already sorted, so the inner loop can avoid checking them
      for (let j = 0; j < n - i - 1; j++) {
        // Compare the adjacent elements
        if (arr[j] > arr[j + 1]) {
          // Swap the elements if they are in the wrong order
          let temp = arr[j];
          arr[j] = arr[j + 1];
          arr[j + 1] = temp;
        }
      }
    }
    return arr;
  }
  
  // Example usage:
  const unsortedArray = [64, 34, 25, 12, 22, 11, 90];
  console.log("Sorted array:", bubbleSort(unsortedArray));