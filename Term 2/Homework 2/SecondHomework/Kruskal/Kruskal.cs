namespace Kruskal;

public static class Kruskal
{
    static List<Edge> FindMinSpanningTree(List<Edge> edges, int vertexCount)
    {
        edges.Sort((edge1, edge2) => edge1.Length.CompareTo(edge2.Length));
        var DSU = new DisjointSetUnion(vertexCount);
        var minSpanningTree = new List<Edge>();

        for (int i = 0; i < edges.Count; i++)
        {
            Edge edge = edges[i];
            if (DSU.FindSet(edge.FirstVertex) == DSU.FindSet(edge.FirstVertex))
            {
                continue;
            }
            else
            {
                minSpanningTree.Add(edge);
                DSU.UnionSets(edge.FirstVertex, edge.SecondVertex);
            }
        }
        
        return minSpanningTree;
    }
}
