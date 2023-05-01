namespace MergeSort
{
    class ParallelMergeSortLinq
    {
        public static void Sort(float[] arr)
        {
            if (arr.Length < 2) return; // Base case: array has 0 or 1 element
            int mid = arr.Length / 2;
            float[] left = arr.Take(mid).ToArray();
            float[] right = arr.Skip(mid).ToArray();
            Parallel.Invoke(
                () => Sort(left), // Recursively sort left half in parallel
                () => Sort(right) // Recursively sort right half in parallel
            );
            Merge(arr, left, right); // Merge sorted halves
        }

        private static void Merge(float[] arr, float[] left, float[] right)
        {
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                if (left[i] < right[j])
                {
                    arr[k++] = left[i++];
                }
                else
                {
                    arr[k++] = right[j++];
                }
            }
            while (i < left.Length)
            {
                arr[k++] = left[i++];
            }
            while (j < right.Length)
            {
                arr[k++] = right[j++];
            }
        }

    }
}
