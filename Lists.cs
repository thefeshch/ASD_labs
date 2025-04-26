namespace Labs_ASD;

public class Lists
{
    static void Main(string[] args)
    {
        TestSinglyLinkedList();
        TestDoublyLinkedList();
    }
    
    static void TestSinglyLinkedList()
    {
        var list = new SinglyLinkedList();
        var node1 = list.InsertAfter(null, 1);
        var node2 = list.InsertAfter(node1, 2);
        var node3 = list.InsertAfter(node2, 3);

        Console.WriteLine(list.Find(2).Node?.Value == 2);
        Console.WriteLine(list.Find(2).Previous?.Value == 1);

        list.RemoveAfter(node1);
        Console.WriteLine(list.Find(2).Node == null);

        list.AssertNoCycles();
    }

    static void TestDoublyLinkedList()
    {
        var list = new DoublyLinkedList();
        var node1 = list.InsertAfter(null, 1);
        var node2 = list.InsertAfter(node1, 2);
        var node3 = list.InsertBefore(node2, 3);

        Console.WriteLine(list.Find(3)?.Value == 3);

        list.Remove(node3);
        Console.WriteLine(list.Find(3) == null);

        list.AssertNoCycles();
    }
}

class SinglyLinkedList
{
    public class Node
    {
        public int Value;
        public Node? Next;

        public Node(int value)
        {
            Value = value;
        }
    }

    public class FindNodeResult
    {
        public Node? Previous;
        public Node? Node;
    }

    private Node? head;
    private Node? tail;
    private int count;

    public Node InsertAfter(Node? node, int value)
    {
        var newNode = new Node(value);
        if (node == null)
        {
            newNode.Next = head;
            head = newNode;
            if (tail == null) tail = newNode;
        }
        else
        {
            newNode.Next = node.Next;
            node.Next = newNode;
            if (node == tail) tail = newNode;
        }

        count++;
        return newNode;
    }

    public FindNodeResult Find(int value)
    {
        Node? prev = null;
        Node? curr = head;
        while (curr != null)
        {
            if (curr.Value == value) return new FindNodeResult { Previous = prev, Node = curr };
            prev = curr;
            curr = curr.Next;
        }

        return new FindNodeResult();
    }

    public void RemoveAfter(Node? node)
    {
        if (node == null)
        {
            if (head != null) head = head.Next;
            if (head == null) tail = null;
        }
        else if (node.Next != null)
        {
            node.Next = node.Next.Next;
            if (node.Next == null) tail = node;
        }

        count--;
    }

    public void AssertNoCycles()
    {
        int actualCount = 0;
        var current = head;
        while (current != null)
        {
            actualCount++;
            if (actualCount > count) throw new InvalidOperationException("Cycle detected!");
            current = current.Next;
        }
    }
}

class DoublyLinkedList
{
    public class Node
    {
        public int Value;
        public Node? Next;
        public Node? Prev;

        public Node(int value)
        {
            Value = value;
        }
    }

    protected Node? Head;
    protected Node? Tail;
    
    public bool IsEmpty() => Head == null;

    public Node InsertAfter(Node? node, int value)
    {
        var newNode = new Node(value);
        if (node == null)
        {
            newNode.Next = Head;
            if (Head != null) Head.Prev = newNode;
            Head = newNode;
            if (Tail == null) Tail = newNode;
        }
        else
        {
            newNode.Next = node.Next;
            newNode.Prev = node;
            if (node.Next != null) node.Next.Prev = newNode;
            node.Next = newNode;
            if (node == Tail) Tail = newNode;
        }

        return newNode;
    }

    public Node InsertBefore(Node? node, int value)
    {
        if (node == null) return InsertAfter(null, value);
        var newNode = new Node(value)
        {
            Next = node,
            Prev = node.Prev
        };
        if (node.Prev != null) node.Prev.Next = newNode;
        node.Prev = newNode;
        if (node == Head) Head = newNode;
        return newNode;
    }

    public Node? Find(int value)
    {
        var current = Head;
        while (current != null)
        {
            if (current.Value == value) return current;
            current = current.Next;
        }

        return null;
    }

    public void Remove(Node? node)
    {
        if (node == null) return;
        if (node.Prev != null) node.Prev.Next = node.Next;
        if (node.Next != null) node.Next.Prev = node.Prev;
        if (node == Head) Head = node.Next;
        if (node == Tail) Tail = node.Prev;
    }

    public void AssertNoCycles()
    {
        var slow = Head;
        var fast = Head;
        while (fast != null && fast.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;
            if (slow == fast) throw new InvalidOperationException("Cycle detected!");
        }
    }
}