namespace Algorithms.UnitTests;
using NUnit.Framework;
using System.Collections.Generic;
using CommonClasses;
using Algoritms;

public class DijkstraTests
{
    [Test]
    public void AlgorithmTest()
    {
        var edges = new List<Edge>();
        edges.Add(new Edge(0, 1, 10));
        edges.Add(new Edge(1, 2, 50));
        edges.Add(new Edge(0, 4, 100));
        edges.Add(new Edge(2, 4, 10));
        edges.Add(new Edge(0, 3, 30));
        edges.Add(new Edge(3, 2, 20));
        edges.Add(new Edge(3, 4, 60));

        var path = Dijkstra.FindMinPath(edges, 5, 0, 4);

        int countLength = 0;
        foreach (var edge in path)
        {
            countLength += edge.Length;
        }

        Assert.AreEqual(60, countLength);
    }

    [Test]
    public void MatrixRepresentationAlgorithmTest()
    {
        var edges = new List<Edge>();
        edges.Add(new Edge(0, 1, 10));
        edges.Add(new Edge(1, 2, 50));
        edges.Add(new Edge(0, 4, 100));
        edges.Add(new Edge(2, 4, 10));
        edges.Add(new Edge(0, 3, 30));
        edges.Add(new Edge(3, 2, 20));
        edges.Add(new Edge(3, 4, 60));

        var matrix = GraphRepresentation.EdgeListToAdjacencyMatrix(edges, 5);
        var path = Dijkstra.FindMinPath(matrix, 0, 4);

        int countLength = 0;
        foreach (var edge in path)
        {
            countLength += edge.Length;
        }

        Assert.AreEqual(60, countLength);
    }
}
