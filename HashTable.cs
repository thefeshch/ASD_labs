namespace Labs_ASD;

class HashTable
{
    static void Main()
    {
        TestHashTable();
    }
    
    static void TestHashTable()
    {
        var hashTable = new HashTable(10);
        hashTable.Add("key1", 100);
        hashTable.Add("key2", 200);
        Console.WriteLine("Find key1: " + hashTable.Find("key1"));
        Console.WriteLine("Find key2: " + hashTable.Find("key2"));
        hashTable.Remove("key1");
        Console.WriteLine("Find key1 after removal: " + hashTable.Find("key1"));
    }
    
    private class BucketNode
    {
        public string Key;
        public int Value;
        public BucketNode Next;
        public BucketNode(string key, int value) { Key = key; Value = value; Next = null; }
    }

    private BucketNode[] buckets;
    private int capacity;

    public HashTable(int capacity)
    {
        this.capacity = capacity;
        buckets = new BucketNode[capacity];
    }

    private int ComputeIndex(string key) => Math.Abs(key.GetHashCode()) % capacity;

    public void Add(string key, int value)
    {
        int index = ComputeIndex(key);
        var newNode = new BucketNode(key, value) { Next = buckets[index] };
        buckets[index] = newNode;
    }

    public int? Find(string key)
    {
        int index = ComputeIndex(key);
        var current = buckets[index];
        while (current != null)
        {
            if (current.Key == key) return current.Value;
            current = current.Next;
        }
        return null;
    }

    public void Remove(string key)
    {
        int index = ComputeIndex(key);
        BucketNode prev = null, current = buckets[index];
        while (current != null)
        {
            if (current.Key == key)
            {
                if (prev == null) buckets[index] = current.Next;
                else prev.Next = current.Next;
                return;
            }
            prev = current;
            current = current.Next;
        }
    }
}