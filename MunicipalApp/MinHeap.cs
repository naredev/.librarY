using System;
using System.Collections.Generic;

namespace MunicipalApp
{
    public class MinHeap<T>
    {
        private List<T> heap;
        private readonly IComparer<T> comparer;

        public MinHeap(IComparer<T> customComparer = null)
        {
            heap = new List<T>();
            comparer = customComparer ?? Comparer<T>.Default;
        }

        // Insert item and maintain heap order
        public void Insert(T item)
        {
            heap.Add(item);
            HeapifyUp(heap.Count - 1);
        }
        //Part of this code was adopted from Stackoverflow
        //https://stackoverflow.com/questions/38677401/c-sharp-passing-a-compare-class-as-a-generic-type
        //Accessed 09 November 2024
        // Get all elements in heap, optionally sorted
        public List<T> GetAll(bool sorted = false)
        {
            if (sorted)
            {
                List<T> sortedHeap = new List<T>(heap);
                sortedHeap.Sort(comparer);
                return sortedHeap;
            }

            return new List<T>(heap);
        }

        // Peek at the minimum element without removing it
        public T PeekMin()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");
            return heap[0];
        }

        // Extract the minimum element from the heap
        public T ExtractMin()
        {
            if (heap.Count == 0)
                throw new InvalidOperationException("Heap is empty");

            T min = heap[0];
            heap[0] = heap[heap.Count - 1];
            heap.RemoveAt(heap.Count - 1);

            if (heap.Count > 0)
                HeapifyDown(0);

            return min;
        }

        // Number of elements in the heap
        public int Count => heap.Count;

        // Helper method to restore heap order after insertion
        private void HeapifyUp(int index)
        {
            while (index > 0)
            {
                int parentIndex = (index - 1) / 2;
                if (comparer.Compare(heap[index], heap[parentIndex]) >= 0)
                    break;

                Swap(index, parentIndex);
                index = parentIndex;
            }
        }

        // Helper method to restore heap order after removal
        private void HeapifyDown(int index)
        {
            int lastIndex = heap.Count - 1;

            while (index <= lastIndex)
            {
                int leftChild = 2 * index + 1;
                int rightChild = 2 * index + 2;
                int smallest = index;

                if (leftChild <= lastIndex && comparer.Compare(heap[leftChild], heap[smallest]) < 0)
                    smallest = leftChild;

                if (rightChild <= lastIndex && comparer.Compare(heap[rightChild], heap[smallest]) < 0)
                    smallest = rightChild;

                if (smallest == index)
                    break;

                Swap(index, smallest);
                index = smallest;
            }
        }

        // Swap helper method
        private void Swap(int i, int j)
        {
            T temp = heap[i];
            heap[i] = heap[j];
            heap[j] = temp;
        }
    }
}
