namespace MergeSort
{
    class MergeSort
    {
        public static void Sort(int[] arr)
        {
            if (arr.Length < 2) return; // Base case: array has 0 or 1 element
            int mid = arr.Length / 2;
            int[] left = arr.Take(mid).ToArray();
            int[] right = arr.Skip(mid).ToArray();
            Sort(left); // Recursively sort left half
            Sort(right); // Recursively sort right half
            Merge(arr, left, right); // Merge sorted halves
        }

        private static void Merge(int[] arr, int[] left, int[] right)
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
