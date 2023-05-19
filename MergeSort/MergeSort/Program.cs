using MPI;
using System.Diagnostics;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "DataSet5.txt";
            string[] stringArr = File.ReadAllText(filePath)
                      .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            float[] arr = stringArr.Select(float.Parse).ToArray();
            float[] arr1 = new float[arr.Length];
            float[] arr2 = new float[arr.Length];
            arr.CopyTo(arr1, 0);
            arr.CopyTo(arr2, 0);

            Console.WriteLine("Array before sorting:");
            foreach (float num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

            Stopwatch stopwatch = Stopwatch.StartNew();
            MergeSort.Sort(arr);
            stopwatch.Stop();

            Console.WriteLine("Array was sorted using MergeSort:");
            foreach (float num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Stopwatch time taken: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

            Stopwatch stopwatch1 = Stopwatch.StartNew();
            ParallelMergeSortLinq.Sort(arr1);
            stopwatch1.Stop();

            Console.WriteLine("Array was sorted using ParallelMergeSortLINQ:");
            foreach (float num in arr1)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Stopwatch time taken: " + stopwatch1.Elapsed.TotalMilliseconds + " ms");

            int rank, size;
            using (new MPI.Environment(ref args))
            {
                rank = Communicator.world.Rank;
                size = Communicator.world.Size;

                Stopwatch stopwatch2 = Stopwatch.StartNew();
                ParallelMergeSortMPI.Sort(arr2, rank, size);
                stopwatch2.Stop();

                if (rank == 0)
                {
                    Console.WriteLine("Array was sorted using ParallelMergeSortMPI:");
                    foreach (float num in arr2)
                    {
                        Console.Write(num + " ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Stopwatch time taken: " + stopwatch2.Elapsed.TotalMilliseconds + " ms");
                }
                using (StreamWriter writer = new StreamWriter("StopwatchTimes.txt"))
                {
                writer.WriteLine("Stopwatch time taken for MergeSort: " + stopwatch.Elapsed.TotalMilliseconds + " ms");
                writer.WriteLine("Stopwatch time taken for ParallelMergeSortLinq: " + stopwatch1.Elapsed.TotalMilliseconds + " ms");
                writer.WriteLine("Stopwatch time taken for ParallelMergeSortMPI: " + stopwatch2.Elapsed.TotalMilliseconds + " ms");
                }
            }
            
        }

    }
}
