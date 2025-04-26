namespace Labs_ASD;

class Program
{
    static void Main(string[] args)
    {
        TestGraph();
    }

    static void TestGraph()
    {
        var graph = new Graph();
        var nodeOne = graph.AddNode(1);
        var nodeTwo = graph.AddNode(2);
        var nodeThree = graph.AddNode(3);
        var nodeFour = graph.AddNode(4);

        graph.AddEdge(nodeOne, nodeTwo);
        graph.AddEdge(nodeOne, nodeThree);
        graph.AddEdge(nodeOne, nodeFour);
        graph.AddEdge(nodeTwo, nodeThree);
        graph.AddEdge(nodeThree, nodeFour);
        graph.AddEdge(nodeFour, nodeOne);

        Console.WriteLine("Sum of neighbors of node A: " + graph.SumOfNeighbors(nodeOne));

        Console.WriteLine("DFS Traversal:");
        graph.DFS(nodeOne, new HashSet<Graph.Node>());

        Console.WriteLine("BFS Traversal:");
        graph.BFS(nodeOne);
    }
}

class Graph
{
    public class Node
    {
        public int Value;
        public List<Node> Neighbors;

        public Node(int value)
        {
            Value = value;
            Neighbors = new List<Node>();
        }
    }

    private List<Node> nodes = new List<Node>();

    public Node AddNode(int value)
    {
        var newNode = new Node(value);
        nodes.Add(newNode);
        return newNode;
    }

    public void AddEdge(Node from, Node to, bool isUndirected = false)
    {
        from.Neighbors.Add(to);
        if (isUndirected)
        {
            to.Neighbors.Add(from);
        }
    }

    public int SumOfNeighbors(Node node)
    {
        int sum = 0;
        foreach (var neighbor in node.Neighbors)
        {
            sum += neighbor.Value;
        }

        return sum;
    }

    public void DFS(Node node, HashSet<Node> visited)
    {
        if (visited.Contains(node)) return;
        Console.WriteLine(node.Value);
        visited.Add(node);
        foreach (var neighbor in node.Neighbors)
        {
            DFS(neighbor, visited);
        }
    }

    public void BFS(Node start)
    {
        var visited = new HashSet<Node>();
        var queue = new Queue<Node>();
        queue.Enqueue(start);
        visited.Add(start);

        while (queue.Count > 0)
        {
            var node = queue.Dequeue();
            Console.WriteLine(node.Value);
            foreach (var neighbor in node.Neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    visited.Add(neighbor);
                    queue.Enqueue(neighbor);
                }
            }
        }
    }
}