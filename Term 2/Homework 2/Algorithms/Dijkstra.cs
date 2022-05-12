namespace Algoritms;
using CommonClasses;

public static class Dijkstra
{
    public static List<Edge>? FindMinPath(int[,] matrix, int startVertex, int endVertex)
    {
        var path = new List<Edge>();

        int N = matrix.GetLength(0);
        int INF = Int32.MaxValue;
        var distances = new int[N];
        for (int i = 0; i < N; i++)
        {
            distances[i] = INF;
        }

        distances[startVertex] = 0;

        var parents = new int[N];
        for (int i = 0; i < N; i++)
        {
            parents[i] = -1;
        }

        var used = new bool[N];
        int minDistance = 0;
        int minVertex = startVertex;
        while (minDistance < INF)
        {
            int i = minVertex;
            used[i] = true;
            for (int j = 0; j < N; j++)
            {
                if (distances[i] + matrix[i, j] < distances[j] && matrix[i, j] != 0)
                {
                    distances[j] = distances[i] + matrix[i, j];
                    parents[j] = i;
                }
            }

            minDistance = INF;
            for (int j = 0; j < N; j++)
            {
                if (!used[j] && (distances[j] < minDistance))
                {
                    minDistance = distances[j];
                    minVertex = j;
                }
            }
        }

        int currentVertex = endVertex;
        while (parents[currentVertex] != -1)
        {
            path.Add(new Edge(currentVertex, parents[currentVertex], matrix[currentVertex, parents[currentVertex]]));
            currentVertex = parents[currentVertex];
        }

        if (currentVertex != startVertex)
        {
            return null;
        }

        path.Reverse();

        return path;
    }
}
