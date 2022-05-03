namespace Algoritms;
using CommonClasses;

public static class Prima
{
    public static List<Edge> FindMinSpanningTree(int[,] matrix)
    {
        var minSpanningTree = new List<Edge>();
        
        int N = matrix.GetLength(0);
        int INF = (int)Math.Pow(10, 9);
        var distances = new int[N];
        var minEdges = new Edge[N];
        for (int i = 1; i < N; i++)
        {
            distances[i] = INF;
        }

        var used = new bool[N];
        for (int i = 0; i < N; i++)
        {
            int minDistance = INF;
            int currentVertex = 0;
            for (int j = 0; j < N; j++)
            {
                if (!used[j] && distances[j] < minDistance)
                {
                    minDistance = distances[j];
                    currentVertex = j;
                }
            }

            used[currentVertex] = true;
            if (currentVertex != 0)
            {
                minSpanningTree.Add(minEdges[currentVertex]);
            }
            
            for (int vertex = 0; vertex < N; vertex++)
            {
                if (matrix[currentVertex, vertex] < distances[vertex] && matrix[currentVertex, vertex] != 0)
                {
                    minEdges[vertex] = new Edge(currentVertex, vertex, matrix[currentVertex, vertex]);
                    distances[vertex] = Math.Min(distances[vertex], matrix[currentVertex, vertex]);
                }
            }
        }

        return minSpanningTree;
    }

    public static List<Edge> FindMinSpanningTree(List<Edge> edges, int vertexCount)
    {
        var matrix = GraphRepresentation.EdgeListToAdjacencyMatrix(edges, vertexCount);

        return FindMinSpanningTree(matrix);
    }
}
