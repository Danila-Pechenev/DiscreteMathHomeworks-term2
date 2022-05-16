namespace Algorithms;

public class Dinic
{
    public static Dictionary<int, int>[] FindMaxFlowGraph(Dictionary<int, int>[] capacityAdjacencyList, int source, int sink)
    {
        int N = capacityAdjacencyList.Length;
        int INF = int.MaxValue - 1;

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

        int[] shortestPathLengths = new int[N];
        var visited = new bool[N];
        while (bfs())
        {
            for (int i = 0; i < N; i++)
            {
                visited[i] = false;
            }

            int delta = dfs(source, INF);
            while (delta != 0)
            {
                for (int i = 0; i < N; i++)
                {
                    visited[i] = false;
                }

                delta = dfs(source, INF);
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

        bool bfs()
        {
            for (int i = 0; i < N; i++)
            {
                shortestPathLengths[i] = INF;
            }

            shortestPathLengths[source] = 0;
            var q = new Queue<int>();
            q.Enqueue(source);

            while (q.Count > 0)
            {
                int vertex = q.Dequeue();
                foreach (var edge in maxFlowGraph[vertex])
                {
                    if ((edge.Value < capacityAdjacencyList[vertex][edge.Key]) && 
                        shortestPathLengths[edge.Key] == INF)
                    {
                        shortestPathLengths[edge.Key] = shortestPathLengths[vertex] + 1;
                        q.Enqueue(edge.Key);
                    }
                }
            }

            return shortestPathLengths[sink] != INF;
        }

        int dfs(int currentVertex, int MinC)
        {
            if (currentVertex == sink || MinC == 0)
            {
                return MinC;
            }

            visited[currentVertex] = true;
            foreach (var edge in maxFlowGraph[currentVertex])
            {
                if (!visited[edge.Key] && shortestPathLengths[edge.Key] == shortestPathLengths[currentVertex] + 1)
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
