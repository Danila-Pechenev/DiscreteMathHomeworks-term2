namespace CommonClasses;

public class Edge
{
    public int FirstVertex { get; init; }

    public int SecondVertex { get; init; }

    public int Weight { get; init; }

    public Edge(int firstVertex, int secondVertex, int weight)
    {
        FirstVertex = firstVertex;
        SecondVertex = secondVertex;
        Weight = weight;
    }
}