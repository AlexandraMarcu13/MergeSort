using System.Diagnostics;


namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] arr = { 9, 5, 1, 8, 2, 7, 3, 6, 4 };
            //int[] arr = new int[1000];
            //Random rand = new Random();
            //for (int i = 0; i < arr.Length; i++)
            //{
            //    arr[i] = rand.Next(1000);
            //}

            Console.WriteLine("Array before sorting:");
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();

            Stopwatch stopwatch = Stopwatch.StartNew();
            MergeSort.Sort(arr);
            stopwatch.Stop();

            Console.WriteLine("Array after sorting:");
            foreach (int num in arr)
            {
                Console.Write(num + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Stopwatch time taken: " + stopwatch.Elapsed.TotalMilliseconds + " ms");

        }
    }
}




