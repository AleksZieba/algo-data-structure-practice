import { bubbleSort } from './bubbleSort';

describe('bubbleSort', () => {
  it('should sort an unsorted array correctly', () => {
    const unsorted = [64, 34, 25, 12, 22, 11, 90];
    const sorted = bubbleSort(unsorted);
    expect(sorted).toEqual([11, 12, 22, 25, 34, 64, 90]);
  });

  it('should handle an already sorted array', () => {
    const sortedArray = [1, 2, 3, 4, 5];
    expect(bubbleSort(sortedArray)).toEqual(sortedArray);
  });

  it('should handle an array with one element', () => {
    expect(bubbleSort([42])).toEqual([42]);
  });

  it('should handle an empty array', () => {
    expect(bubbleSort([])).toEqual([]);
  });

  it('should correctly sort an array with negative numbers', () => {
    const unsorted = [0, -3, 5, -10, 8];
    expect(bubbleSort(unsorted)).toEqual([-10, -3, 0, 5, 8]);
  });
});