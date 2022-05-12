namespace CommonClasses;

public class DisjointSetUnion
{
    private int[] parent;

    public DisjointSetUnion(int n)
    {
        parent = new int[n];
        for (int i = 0; i < n; i++)
        {
            parent[i] = i;
        }
    }

    public int FindSet(int vertex)
    {
        if (parent[vertex] == vertex)
        {
            return vertex;
        }

        return FindSet(parent[vertex]);
    }

    public void UnionSets(int vertex1, int vertex2)
    {
        int set1 = FindSet(vertex1);
        int set2 = FindSet(vertex2);

        if (set1 != set2)
        {
            parent[set2] = set1;
        }
    }
}
