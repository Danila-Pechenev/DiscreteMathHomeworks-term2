namespace Algorithms.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using CommonClasses;
using Algoritms;

public class EffectivePrimaTests
{
    [Test]
    public void AlgorithmTest()
    {
        var edges = new List<Edge>();
        edges.Add(new Edge(0, 1, 7));
        edges.Add(new Edge(1, 2, 8));
        edges.Add(new Edge(0, 3, 5));
        edges.Add(new Edge(1, 3, 9));
        edges.Add(new Edge(3, 4, 15));
        edges.Add(new Edge(1, 4, 7));
        edges.Add(new Edge(3, 5, 6));
        edges.Add(new Edge(4, 5, 8));
        edges.Add(new Edge(5, 6, 11));
        edges.Add(new Edge(4, 6, 9));
        edges.Add(new Edge(2, 4, 5));

        var adjacencyList = GraphRepresentation.EdgeListToAdjacencyList(edges, 7);
        var minSpanningTree = EffectivePrima.FindMinSpanningTree(adjacencyList);

        int countLength = 0;
        foreach (var edge in minSpanningTree)
        {
            countLength += edge.Length;
        }

        Assert.AreEqual(39, countLength);
    }

    [Test]
    public void CountEdgesTest()
    {
        var edges = new List<Edge>();
        edges.Add(new Edge(0, 1, 6));
        edges.Add(new Edge(1, 2, 19));
        edges.Add(new Edge(1, 6, 25));
        edges.Add(new Edge(2, 3, 9));
        edges.Add(new Edge(3, 4, 15));
        edges.Add(new Edge(5, 3, 14));
        edges.Add(new Edge(5, 4, 21));
        edges.Add(new Edge(5, 6, 2));
        edges.Add(new Edge(7, 6, 8));
        edges.Add(new Edge(0, 6, 11));
        edges.Add(new Edge(0, 7, 17));

        var adjacencyList = GraphRepresentation.EdgeListToAdjacencyList(edges, 8);
        var minSpanningTree = EffectivePrima.FindMinSpanningTree(adjacencyList);

        Assert.AreEqual(7, minSpanningTree.Count);
    }
}
