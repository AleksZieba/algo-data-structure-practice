import { mergeSort } from './mergesort'; 

describe('mergeSort', () => {
  it('should return an empty array for an empty input', () => {
    expect(mergeSort([])).toEqual([]);
  });

  it('should return the same array for a single-element array', () => {
    expect(mergeSort([5])).toEqual([5]);
  });

  it('should sort an unsorted array of numbers', () => {
    const unsortedArray = [34, 7, 23, 32, 5, 62];
    const sortedArray = [5, 7, 23, 32, 34, 62];
    expect(mergeSort(unsortedArray)).toEqual(sortedArray);
  });

  it('should sort an array with duplicate numbers', () => {
    const unsortedArray = [3, 1, 2, 3, 1];
    const sortedArray = [1, 1, 2, 3, 3];
    expect(mergeSort(unsortedArray)).toEqual(sortedArray);
  });

  it('should handle an already sorted array', () => {
    const sortedArray = [1, 2, 3, 4, 5];
    expect(mergeSort(sortedArray)).toEqual(sortedArray);
  });

  it('should sort a reverse sorted array', () => {
    const unsortedArray = [5, 4, 3, 2, 1];
    const sortedArray = [1, 2, 3, 4, 5];
    expect(mergeSort(unsortedArray)).toEqual(sortedArray);
  });
});