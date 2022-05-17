namespace Algorithms;

public class CapacityScaling
{
    public static Dictionary<int, int>[] FindMaxFlowGraph(Dictionary<int, int>[] capacityAdjacencyList, int source, int sink)
    {
        int N = capacityAdjacencyList.Length;
        int INF = Int32.MaxValue;
        int maxCapacity = 0;

        for (int i = 0; i < N; i++)
        {
            foreach (var edge in capacityAdjacencyList[i])
            {
                maxCapacity = Math.Max(maxCapacity, edge.Value);
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

        var visited = new bool[N];
        int threshold = (int)Math.Pow(2, (int)Math.Floor(Math.Log2(maxCapacity)) + 1);
        while (threshold != 1)
        {
            threshold /= 2;
            for (int i = 0; i < N; i++)
            {
                visited[i] = false;
            }

            int delta = dfs(source, INF, threshold);
            while (delta != 0)
            {
                for (int i = 0; i < N; i++)
                {
                    visited[i] = false;
                }

                delta = dfs(source, INF, threshold);
            }
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

        int dfs(int currentVertex, int MinC, int threshold)
        {
            if (currentVertex == sink)
            {
                return MinC;
            }

            visited[currentVertex] = true;
            foreach (var edge in maxFlowGraph[currentVertex])
            {
                if (!visited[edge.Key] && (edge.Value < capacityAdjacencyList[currentVertex][edge.Key]) && (edge.Value <= 0 || edge.Value >= threshold))
                {
                    int delta = dfs(edge.Key, Math.Min(MinC, capacityAdjacencyList[currentVertex][edge.Key] - edge.Value), threshold);
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
