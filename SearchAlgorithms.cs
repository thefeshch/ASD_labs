using System.Diagnostics;

public class SearchAlgorithms
{
   public static void Main()
    {
        int[] sizes = { 1000, 10000, 50000, 100000 };
        int testRuns = 5;
        Random rand = new Random();

        foreach (int size in sizes)
        {
            Console.WriteLine($"=========================\nTesting array size: {size}");

            int[] sortedArray = Enumerable.Range(1, size).ToArray();
            int[] randomArray = sortedArray.OrderBy(x => rand.Next()).ToArray();

            TestSearch("Linear Search", LinearSearch, randomArray, testRuns);
            TestSearch("Binary Search", BinarySearch, sortedArray, testRuns);
            TestSearch("Jump Search", JumpSearch, sortedArray, testRuns);
            if (size <= 50000)
                TestBinaryTreeSearch(size, rand.Next(1, size + 1), testRuns);
        }
    }

    static void TestSearch(string name, Func<int[], int, int> searchFunc, int[] array, int runs)
    {
        Stopwatch sw = new Stopwatch();
        long totalTime = 0;

        for (int i = 0; i < runs; i++)
        {
            int target = new Random().Next(1, array.Length + 1);
            sw.Restart();
            searchFunc(array, target);
            sw.Stop();
            totalTime += sw.ElapsedMilliseconds;
        }

        Console.WriteLine($"{name}: {totalTime / runs} ticks");
    }

    static int LinearSearch(int[] array, int target)
    {
        for (int i = 0; i < array.Length; i++)
            if (array[i] == target)
                return i;
        return -1;
    }

    static int BinarySearch(int[] array, int target)
    {
        int left = 0, right = array.Length - 1;
        while (left <= right)
        {
            int mid = left + (right - left) / 2;
            if (array[mid] == target) return mid;
            
            if (array[mid] < target) 
                left = mid + 1;
            else 
                right = mid - 1;
        }
        return -1;
    }

    static int JumpSearch(int[] array, int target)
    {
        int length = array.Length;
        int step = (int)Math.Sqrt(length);
        int prev = 0;

        while (prev < length && array[Math.Min(step, length) - 1] < target)
        {
            prev = step;
            step += (int)Math.Sqrt(length);
            if (prev >= length) return -1;
        }

        for (int i = prev; i < Math.Min(step, length); i++)
        {
            if (array[i] == target) return i;
        }

        return -1;
    }

    static void TestBinaryTreeSearch(int size, int target, int runs)
    {
        BinarySearchTree tree = new BinarySearchTree();
        foreach (var num in Enumerable.Range(1, size))
            tree.Insert(num);

        Stopwatch sw = new Stopwatch();
        long totalTime = 0;

        for (int i = 0; i < runs; i++)
        {
            sw.Restart();
            tree.Search(target);
            sw.Stop();
            totalTime += sw.ElapsedMilliseconds;
        }

        Console.WriteLine($"Binary Tree Search: {totalTime / runs} ms\n");
    }
}

class TreeNode
{
    public int Value;
    public TreeNode Left, Right;
    public TreeNode(int value) => Value = value;
}

class BinarySearchTree
{
    private TreeNode root;
    public void Insert(int value)
    {
        if (root == null)
        {
            root = new TreeNode(value);
            return;
        }
        
        TreeNode current = root;
        while (true)
        {
            if (value < current.Value)
            {
                if (current.Left == null)
                {
                    current.Left = new TreeNode(value);
                    return;
                }
                current = current.Left;
            }
            else
            {
                if (current.Right == null)
                {
                    current.Right = new TreeNode(value);
                    return;
                }
                current = current.Right;
            }
        }
    }

    public bool Search(int value)
    {
        TreeNode current = root;
        while (current != null)
        {
            if (current.Value == value) return true;
            current = value < current.Value ? current.Left : current.Right;
        }

        return false;
    }
}