export function mergeSort(arr: number[]): number[] {
    // Base case: if the array has 0 or 1 element, it's already sorted.
    if (arr.length <= 1) {
      return arr;
    }
  
    // Find the middle index and split the array into two halves.
    const mid = Math.floor(arr.length / 2);
    const left = arr.slice(0, mid);
    const right = arr.slice(mid);
  
    // Recursively sort both halves and merge them.
    return merge(mergeSort(left), mergeSort(right));
  }
  
  function merge(left: number[], right: number[]): number[] {
    const result: number[] = [];
    let i = 0, j = 0;
  
    // Compare elements from left and right arrays and merge them in order.
    while (i < left.length && j < right.length) {
      if (left[i] < right[j]) {
        result.push(left[i]);
        i++;
      } else {
        result.push(right[j]);
        j++;
      }
    }
  
    // If there are remaining elements in left or right, add them.
    return result.concat(left.slice(i)).concat(right.slice(j));
  }