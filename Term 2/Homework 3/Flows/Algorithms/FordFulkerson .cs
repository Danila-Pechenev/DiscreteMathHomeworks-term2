namespace Algorithms;

public class FordFulkerson
{
    public static Dictionary<int, int>[] FindMaxFlowGraph(Dictionary<int, int>[] capacityAdjacencyList, int source, int sink)
    {
        int N = capacityAdjacencyList.Length;
        int INF = (int)1e9;

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
  
        int delta = dfs(source, INF);
        while (delta != 0)
        {
            delta = dfs(source, INF);
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

        int dfs(int currentVertex, int MinC)
        {
            var visited = new bool[N];

            if (currentVertex == sink)
            {
                return MinC;
            }

            visited[currentVertex] = true;
            foreach (var edge in maxFlowGraph[currentVertex])
            {
                if (!visited[edge.Key] && (edge.Value < capacityAdjacencyList[currentVertex][edge.Key]))
                {
                    int delta = dfs(edge.Key, Math.Min(MinC, capacityAdjacencyList[currentVertex][edge.Key] - edge.Value));
                    if (delta > 0)
                    {
                        maxFlowGraph[currentVertex][edge.Key] += delta;
                        maxFlowGraph[edge.Key][currentVertex] -= delta;

                        return delta;
                    }
                }
            }

            return 0;
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
