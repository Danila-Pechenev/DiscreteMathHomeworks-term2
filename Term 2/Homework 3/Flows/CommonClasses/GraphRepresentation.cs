namespace CommonClasses;

public static class GraphRepresentation
{
    public static int[,] EdgeListToAdjacencyMatrix(List<Edge> edgeList, int vertexCount)
    {
        int[,] matrix = new int[vertexCount, vertexCount];
        foreach (var edge in edgeList)
        {
            matrix[edge.FirstVertex, edge.SecondVertex] = edge.Weight;
            matrix[edge.SecondVertex, edge.FirstVertex] = edge.Weight;
        }

        return matrix;
    }

    public static List<Edge> AdjacencyMatrixToEdgeList(int[,] matrix)
    {
        var edgeList = new List<Edge>();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = i + 1; j < matrix.GetLength(0); j++)
            {
                if (matrix[i, j] != 0)
                {
                    edgeList.Add(new Edge(i, j, matrix[i, j]));
                }
            }
        }

        return edgeList;
    }

    public static Dictionary<int, int>[] EdgeListToAdjacencyList(List<Edge> edgeList, int vertexCount)
    {
        var adjacencyList = new Dictionary<int, int>[vertexCount];
        for (int i = 0; i < adjacencyList.Length; i++)
        {
            adjacencyList[i] = new Dictionary<int, int>();
        }

        foreach (var edge in edgeList)
        {
            adjacencyList[edge.FirstVertex].Add(edge.SecondVertex, edge.Weight);
        }

        return adjacencyList;
    }

    public static List<Edge> AdjacencyListToEdgeList(Dictionary<int, int>[] adjacencyList)
    {
        var edgeList = new List<Edge>();
        for (int i = 0; i < adjacencyList.Length; i++)
        {
            foreach (var edge in adjacencyList[i])
            {
                if (edge.Key < i)
                {
                    edgeList.Add(new Edge(i, edge.Key, edge.Value));
                }
            }
        }

        return edgeList;
    }
}
