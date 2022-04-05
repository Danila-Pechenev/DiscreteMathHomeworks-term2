namespace Kruskal;

public class Edge
{
    public int FirstVertex { get; init; }

    public int SecondVertex { get; init; }

    public int Length { get; init; }

    public Edge(int firstVertex, int secondVertex, int length)
    {
        FirstVertex = firstVertex;
        SecondVertex = secondVertex;
        Length = length;
    }
}
