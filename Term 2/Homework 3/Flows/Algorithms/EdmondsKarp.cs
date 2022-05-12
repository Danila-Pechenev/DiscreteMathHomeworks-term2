namespace Algorithms;

public class EdmondsKarp
{
    public static Dictionary<int, int>[] FindMaxFlowGraph(Dictionary<int, int>[] capacityAdjacencyList, int source, int sink)
    {
        int N = capacityAdjacencyList.Length;

        for (int i = 0; i < N; i++)
        {
            foreach (var edge in capacityAdjacencyList[i])
            {
                capacityAdjacencyList[edge.Key].TryAdd(i, 0);
            }
        }

        var maxFlowGraph = new Dictionary<int, int>[N];
        for (int i = 0; i < N; i++)
        {
            maxFlowGraph[i] = new Dictionary<int, int>();
            foreach (var edge in capacityAdjacencyList[i])
            {
                maxFlowGraph[i].TryAdd(edge.Key, 0);
            }
        }

        int delta = bfs();
        while (delta != 0)
        {
            delta = bfs();
        }

        for (int i = 0; i < N; i++)
        {
            foreach (var edge in maxFlowGraph[i])
            {
                if (edge.Value <= 0)
                {
                    maxFlowGraph[i].Remove(edge.Key);
                }
            }
        }

        return maxFlowGraph;

        int bfs()
        {
            int[] parents = new int[N];
            int[] parentsDelta = new int[N];
            for (int i = 0; i < N; i++)
            {
                parents[i] = -1;
            }

            var visited = new bool[N];
            var q = new Queue<int>();
            q.Enqueue(source);
            visited[source] = true;

            while (q.Count > 0)
            {
                int vertex = q.Dequeue();
                if (vertex == sink)
                {
                    break;
                }

                foreach (var edge in maxFlowGraph[vertex])
                {
                    if (!visited[edge.Key] && (edge.Value < capacityAdjacencyList[vertex][edge.Key]))
                    {
                        parents[edge.Key] = vertex;
                        parentsDelta[edge.Key] = capacityAdjacencyList[vertex][edge.Key] - edge.Value;
                        visited[edge.Key] = true;
                        q.Enqueue(edge.Key);
                    }
                }
            }

            int MinC = Int32.MaxValue;
            int currentVertex = sink;
            while (parents[currentVertex] != -1)
            {
                MinC = Math.Min(MinC, parentsDelta[currentVertex]);
                currentVertex = parents[currentVertex];
            }

            if (MinC == Int32.MaxValue)
            {
                MinC = 0;
            }

            currentVertex = sink;
            while (parents[currentVertex] != -1)
            {
                maxFlowGraph[parents[currentVertex]][currentVertex] += MinC;
                maxFlowGraph[currentVertex][parents[currentVertex]] -= MinC;
                currentVertex = parents[currentVertex];
            }

            return MinC;
        }
    }

    public static int FindMaxFlowValue(Dictionary<int, int>[] adjacencyList, int source, int sink)
    {
        var maxFlowGraph = FindMaxFlowGraph(adjacencyList, source, sink);
        int flowValue = 0;
        foreach (var edge in maxFlowGraph[source])
        {
            flowValue += edge.Value;
        }

        return flowValue;
    }
}
