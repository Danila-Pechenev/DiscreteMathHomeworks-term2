namespace Algoritms;
using CommonClasses;

public static class Dijkstra
{
    public static List<Edge>? FindMinPath(int[,] matrix, int startVertex, int endVertex)
    {
        var path = new List<Edge>();

        int N = matrix.GetLength(0);
        int INF = (int)Math.Pow(10, 9);
        var distances = new int[N];
        var parents = new int[N];
        for (int i = 1; i < N; i++)
        {
            distances[i] = INF;
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

            if (minDistance == INF)
            {
                return null;
            }
        }

        int currentVertex = endVertex;
        while (currentVertex != startVertex)
        {
            path.Add(new Edge(currentVertex, parents[currentVertex], matrix[currentVertex, parents[currentVertex]]));
            currentVertex = parents[currentVertex];
        }

        path.Reverse();

        return path;
    }
}
