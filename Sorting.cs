using System.Diagnostics;

namespace Labs_ASD;

public static class SortingAlgorithms
{
    static void Main(string[] args)
    {
        TestSortingAlgorithms();
    }
    public static (int comparisons, int swaps) BubbleSort(int[] array)
    {
        int n = array.Length;
        int comparisons = 0;
        int swaps = 0;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                comparisons++;
                if (array[j] > array[j + 1])
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    swaps++;
                }
            }
        }
        return (comparisons, swaps);
    }

    public static (int comparisons, int swaps) InsertionSort(int[] array)
    {
        int n = array.Length;
        int comparisons = 0;
        int swaps = 0;
        for (int i = 1; i < n; i++)
        {
            int key = array[i];
            int j = i - 1;

            while (j >= 0 && array[j] > key)
            {
                comparisons++;
                array[j + 1] = array[j];
                swaps++;
                j--;
            }
            array[j + 1] = key;
            swaps++;
        }
        return (comparisons, swaps);
    }

    public static (int comparisons, int swaps) QuickSort(int[] array)
    {
        int comparisons = 0;
        int swaps = 0;
        QuickSortInternal(array, 0, array.Length - 1, ref comparisons, ref swaps);
        return (comparisons, swaps);
    }

    private static void QuickSortInternal(int[] array, int low, int high, ref int comparisons, ref int swaps)
    {
        if (low < high)
        {
            int pi = Partition(array, low, high, ref comparisons, ref swaps);
            QuickSortInternal(array, low, pi - 1, ref comparisons, ref swaps);
            QuickSortInternal(array, pi + 1, high, ref comparisons, ref swaps);
        }
    }

    private static int Partition(int[] array, int low, int high, ref int comparisons, ref int swaps)
    {
        int pivot = array[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            comparisons++;
            if (array[j] < pivot)
            {
                i++;
                (array[i], array[j]) = (array[j], array[i]);
                swaps++;
            }
        }
        (array[i + 1], array[high]) = (array[high], array[i + 1]);
        swaps++;
        return i + 1;
    }

    public static (int comparisons, int swaps) MergeSort(int[] array)
    {
        int comparisons = 0;
        int swaps = 0;
        int[] sortedArray = MergeSortInternal(array, ref comparisons, ref swaps);
        Array.Copy(sortedArray, array, array.Length);
        return (comparisons, swaps);
    }

    private static int[] MergeSortInternal(int[] array, ref int comparisons, ref int swaps)
    {
        if (array.Length <= 1)
            return array;

        int mid = array.Length / 2;
        int[] left = MergeSortInternal(array[..mid], ref comparisons, ref swaps);
        int[] right = MergeSortInternal(array[mid..], ref comparisons, ref swaps);

        return Merge(left, right, ref comparisons, ref swaps);
    }

    private static int[] Merge(int[] left, int[] right, ref int comparisons, ref int swaps)
    {
        int[] result = new int[left.Length + right.Length];
        int i = 0, j = 0, k = 0;

        while (i < left.Length && j < right.Length)
        {
            comparisons++;
            if (left[i] <= right[j])
                result[k++] = left[i++];
            else
                result[k++] = right[j++];
            swaps++;
        }

        while (i < left.Length) { result[k++] = left[i++]; swaps++; }
        while (j < right.Length) { result[k++] = right[j++]; swaps++; }

        return result;
    }

    public static void TestSortingAlgorithms()
    {
        int[] sizes = {  1000, 5000, 10000, 100000 };
        foreach (int size in sizes)
        {
            Console.WriteLine($"\n=== Testing on array of size {size} ===");
            int[] array = new int[size];
            Random rand = new Random();
            for (int i = 0; i < size; i++) array[i] = rand.Next(1, 10000);

            RunTest("BubbleSort", array, BubbleSort);
            RunTest("InsertionSort", array, InsertionSort);
            RunTest("QuickSort", array, QuickSort);
            RunTest("MergeSort", array, MergeSort);
        }
    }

    private static void RunTest(string name, int[] originalArray, Func<int[], (int, int)> sortMethod)
    {
        int[] array = (int[])originalArray.Clone();
        var watch = Stopwatch.StartNew();
        var (comparisons, swaps) = sortMethod(array);
        watch.Stop();
        Console.WriteLine($"{name}>| Size: {originalArray.Length} | Time: {watch.ElapsedMilliseconds} ms | Comparisons: {comparisons} | Swaps: {swaps}");
    }
}