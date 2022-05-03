namespace CommonClasses;

public static class GraphRepresentation
{
    public static int[,] EdgeListToAdjacencyMatrix(List<Edge> edgeList, int vertexCount)
    {
        int[,] matrix = new int[vertexCount, vertexCount];
        foreach (var edge in edgeList)
        {
            matrix[edge.FirstVertex, edge.SecondVertex] = edge.Length;
            matrix[edge.SecondVertex, edge.FirstVertex] = edge.Length;
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
}
