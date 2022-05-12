namespace Algoritms;
using CommonClasses;

public static class EffectivePrima
{
    public static List<Edge>? FindMinSpanningTree(Dictionary<int, int>[] adjacencyList)
    {
        var minSpanningTree = new List<Edge>();

        int N = adjacencyList.Length;
        int INF = Int32.MaxValue;
        var distances = new int[N];
        var edgeEnds = new int[N];
        var used = new bool[N];
        for (int i = 1; i < N; i++)
        {
            distances[i] = INF;
        }

        for (int i = 0; i < N; i++)
        {
            edgeEnds[i] = -1;
        }

        var q = new PriorityQueue<int, int>();
        q.Enqueue(0, 0);
        for (int i = 0; i < N; i++)
        {
            if (q.Count == 0)
            {
                return null;
            }

            int currentVertex = q.Dequeue();
            while (used[currentVertex])
            {
                currentVertex = q.Dequeue();
            }

            used[currentVertex] = true;

            if (edgeEnds[currentVertex] != -1)
            {
                minSpanningTree.Add(new Edge(currentVertex,
                    edgeEnds[currentVertex], adjacencyList[currentVertex][edgeEnds[currentVertex]]));
            }

            foreach (var edge in adjacencyList[currentVertex])
            {
                int edgeEnd = edge.Key;
                int length = edge.Value;

                if (length < distances[edgeEnd])
                {
                    distances[edgeEnd] = length;
                    edgeEnds[edgeEnd] = currentVertex;
                    q.Enqueue(edgeEnd, length);
                }
            }
        }

        return minSpanningTree;
    }
}
