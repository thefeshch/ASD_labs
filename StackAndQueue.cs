namespace Labs_ASD;

public class StackAndQueue
{
    static void Main()
    {
        TestStack();
        TestQueue();
    }

    static void TestStack()
    {
        var stack = new Stack();
        stack.Push(1);
        stack.Push(2);
        stack.Push(3);
        Console.WriteLine("Stack top: " + stack.Peek());
        stack.Pop();
        Console.WriteLine("Stack top after pop: " + stack.Peek());
    }

    static void TestQueue()
    {
        var queue = new Queue();
        queue.Enqueue(1);
        queue.Enqueue(2);
        queue.Enqueue(3);
        Console.WriteLine("Queue front: " + queue.Front());
        queue.Dequeue();
        Console.WriteLine("Queue front after dequeue: " + queue.Front());
    }
}

class Stack : DoublyLinkedList
{
    public void Push(int value)
    {
        var node = new Node(value);
        if (IsEmpty())
            Head = Tail = node;
        else
        {
            node.Prev = Tail;
            Tail.Next = node;
            Tail = node;
        }
    }

    public int Peek() => IsEmpty() ? throw new InvalidOperationException("Stack is empty") : Tail.Value;

    public void Pop()
    {
        if (IsEmpty()) throw new InvalidOperationException("Stack is empty");
        Tail = Tail.Prev;
        if (Tail == null) Head = null;
        else Tail.Next = null;
    }
}

class Queue : DoublyLinkedList
{
    public void Enqueue(int value)
    {
        var node = new Node(value);
        if (IsEmpty())
            Head = Tail = node;
        else
        {
            Tail.Next = node;
            node.Prev = Tail;
            Tail = node;
        }
    }

    public int Front() => IsEmpty() ? throw new InvalidOperationException("Queue is empty") : Head.Value;

    public void Dequeue()
    {
        if (IsEmpty()) throw new InvalidOperationException("Queue is empty");
        Head = Head.Next;
        if (Head == null) Tail = null;
        else Head.Prev = null;
    }
}