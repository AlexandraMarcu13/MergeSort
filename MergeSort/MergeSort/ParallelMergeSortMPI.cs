using MPI;

namespace MergeSort
{
    class ParallelMergeSortMPI
    {
        public static void Sort(float[] arr, int rank, int size)
        {
            int n = arr.Length;
            int fromIndex = rank * n / size;
            int toIndex = (rank + 1) * n / size;

            if (n < 2)
            {
                return;
            }

    
            for (int subSize = 1; subSize < n; subSize *= 2)
            {
                for (int leftStart = fromIndex; leftStart < toIndex; leftStart += 2 * subSize)
                {
                    int mid = Math.Min(leftStart + subSize, toIndex);
                    int rightStart = Math.Min(leftStart + 2 * subSize, toIndex);

                    float[] left = arr.Skip(leftStart).Take(mid - leftStart).ToArray();
                    float[] right = arr.Skip(mid).Take(rightStart - mid).ToArray();

                    float[] merged = Merge(left, right);

                    Array.Copy(merged, 0, arr, leftStart, merged.Length);
                }

    
                Communicator.world.Barrier();
            }

            if (rank == 0)
            {
                float[] sorted = Communicator.world.Reduce<float>(arr, Operation<float>.Min, 0);
                sorted.CopyTo(arr, 0);
            }
        }


        private static float[] Merge(float[] left, float[] right)
        {
            float[] merged = new float[left.Length + right.Length];
            int i = 0, j = 0, k = 0;
            while (i < left.Length && j < right.Length)
            {
                if (left[i] < right[j])
                {
                    merged[k++] = left[i++];
                }
                else
                {
                    merged[k++] = right[j++];
                }
            }
            while (i < left.Length)
            {
                merged[k++] = left[i++];
            }
            while (j < right.Length)
            {
                merged[k++] = right[j++];
            }
            return merged;
        }
    }
}
