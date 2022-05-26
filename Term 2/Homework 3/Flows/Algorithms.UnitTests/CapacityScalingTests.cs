namespace Algorithms.UnitTests;
using System.Collections.Generic;
using NUnit.Framework;
using CommonClasses;
using Algorithms;

public class CapacityScalingTests
{
    [Test]
    public void FindMaxFlowGraphCorrectnessTest()
    {
        var edges = new List<Edge>();
        edges.Add(new Edge(0, 1, 3));
        edges.Add(new Edge(0, 2, 5));
        edges.Add(new Edge(1, 3, 5));
        edges.Add(new Edge(2, 1, 4));
        edges.Add(new Edge(2, 4, 2));
        edges.Add(new Edge(3, 4, 6));
        edges.Add(new Edge(3, 5, 5));
        edges.Add(new Edge(4, 5, 7));

        var capacityAdjacencyList = GraphRepresentation.EdgeListToAdjacencyList(edges, 6);
        var maxFlowGraph = CapacityScaling.FindMaxFlowGraph(capacityAdjacencyList, 0, 5);
        int[] sums = new int[6];
        for (int i = 0; i < maxFlowGraph.Length; i++)
        {
            foreach (var edge in maxFlowGraph[i])
            {
                sums[i] -= edge.Value;
                sums[edge.Key] += edge.Value;
            }
        }

        Assert.AreEqual(-7, sums[0]);
        Assert.AreEqual(7, sums[5]);
        for (int i = 1; i < 5; i++)
        {
            Assert.AreEqual(0, sums[i]);
        }
    }

    [Test]
    public void FindMaxFlowValueTest()
    {
        var edges = new List<Edge>();
        edges.Add(new Edge(0, 1, 3));
        edges.Add(new Edge(0, 2, 5));
        edges.Add(new Edge(1, 3, 5));
        edges.Add(new Edge(2, 1, 4));
        edges.Add(new Edge(2, 4, 2));
        edges.Add(new Edge(3, 4, 6));
        edges.Add(new Edge(3, 5, 5));
        edges.Add(new Edge(4, 5, 7));

        var capacityAdjacencyList = GraphRepresentation.EdgeListToAdjacencyList(edges, 6);
        int maxFlow = CapacityScaling.FindMaxFlowValue(capacityAdjacencyList, 0, 5);
        Assert.AreEqual(7, maxFlow);
    }

    [Test]
    public void FindMaxFlowValueInBigGraphTest()
    {
        var edges = new List<Edge>();
        edges.Add(new Edge(0, 1, 6));
        edges.Add(new Edge(1, 5, 4));
        edges.Add(new Edge(5, 9, 5));
        edges.Add(new Edge(9, 13, 6));
        edges.Add(new Edge(0, 2, 6));
        edges.Add(new Edge(1, 2, 3));
        edges.Add(new Edge(5, 2, 9));
        edges.Add(new Edge(2, 6, 4));
        edges.Add(new Edge(6, 5, 8));
        edges.Add(new Edge(9, 6, 10));
        edges.Add(new Edge(6, 10, 5));
        edges.Add(new Edge(10, 9, 8));
        edges.Add(new Edge(10, 13, 9));
        edges.Add(new Edge(2, 3, 4));
        edges.Add(new Edge(7, 6, 8));
        edges.Add(new Edge(11, 10, 8));
        edges.Add(new Edge(6, 3, 10));
        edges.Add(new Edge(10, 7, 12));
        edges.Add(new Edge(0, 3, 8));
        edges.Add(new Edge(3, 7, 5));
        edges.Add(new Edge(7, 11, 5));
        edges.Add(new Edge(11, 13, 7));
        edges.Add(new Edge(0, 4, 9));
        edges.Add(new Edge(3, 4, 3));
        edges.Add(new Edge(4, 8, 6));
        edges.Add(new Edge(3, 8, 10));
        edges.Add(new Edge(8, 7, 7));
        edges.Add(new Edge(8, 12, 7));
        edges.Add(new Edge(7, 12, 12));
        edges.Add(new Edge(12, 11, 7));
        edges.Add(new Edge(12, 13, 6));

        var capacityAdjacencyList = GraphRepresentation.EdgeListToAdjacencyList(edges, 14);
        int maxFlow = CapacityScaling.FindMaxFlowValue(capacityAdjacencyList, 0, 13);
        Assert.AreEqual(26, maxFlow);
    }
}
