namespace CommonClasses.UnitTests;
using NUnit.Framework;
using CommonClasses;
using System.Collections.Generic;

public class GraphRepresentationTests
{
    [Test]
    public void EdgeListToAdjacencyMatrixTest()
    {
        var edges = new List<Edge>();
        edges.Add(new Edge(1, 2, 3));
        edges.Add(new Edge(0, 2, 12));
        edges.Add(new Edge(1, 3, 7));

        var matrix = GraphRepresentation.EdgeListToAdjacencyMatrix(edges, 4);
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                Assert.AreEqual(matrix[i, j], matrix[j, i]);

                if (i == 1 && j == 2 || i == 2 && j == 1)
                {
                    Assert.AreEqual(3, matrix[i, j]);
                }
                else if (i == 0 && j == 2 || i == 2 && j == 0)
                {
                    Assert.AreEqual(12, matrix[i, j]);
                }
                else if (i == 1 && j == 3 || i == 3 && j == 1)
                {
                    Assert.AreEqual(7, matrix[i, j]);
                }
                else
                {
                    Assert.AreEqual(0, matrix[i, j]);
                }
            }
        }
    }

    [Test]
    public void AdjacencyMatrixToEdgeListTest()
    {
        var matrix = new int[,] {
            { 0, 0, 10, 0, 0 },
            { 0, 0, 0, 2, 0 },
            { 10, 0, 0, 0, 0 },
            { 0, 2, 0, 0, 5 },
            { 0, 0, 0, 5, 0 },
        };

        var edges = GraphRepresentation.AdjacencyMatrixToEdgeList(matrix);

        Assert.AreEqual(3, edges.Count);
        foreach (var edge in edges)
        {
            if (edge.FirstVertex == 2 && edge.SecondVertex == 0 ||
                edge.FirstVertex == 0 && edge.SecondVertex == 2)
            {
                Assert.AreEqual(10, edge.Weight);
            }
            else if (edge.FirstVertex == 1 && edge.SecondVertex == 3 ||
                edge.FirstVertex == 3 && edge.SecondVertex == 1)
            {
                Assert.AreEqual(2, edge.Weight);
            }
            else if (edge.FirstVertex == 3 && edge.SecondVertex == 4 ||
                edge.FirstVertex == 4 && edge.SecondVertex == 3)
            {
                Assert.AreEqual(5, edge.Weight);
            }
            else
            {
                Assert.Fail();
            }
        }
    }

    [Test]
    public void EdgeListToAdjacencyListTest()
    {
        var edges = new List<Edge>();
        edges.Add(new Edge(1, 2, 3));
        edges.Add(new Edge(0, 2, 12));
        edges.Add(new Edge(1, 3, 7));

        var adjacencyList = GraphRepresentation.EdgeListToAdjacencyList(edges, 4);
        for (int i = 0; i < adjacencyList.Length; i++)
        {
            foreach (var edge in adjacencyList[i])
            {
                if (i == 1 && edge.Key == 2 || i == 2 && edge.Key == 1)
                {
                    Assert.AreEqual(3, edge.Value);
                }
                else if (i == 0 && edge.Key == 2 || i == 2 && edge.Key == 0)
                {
                    Assert.AreEqual(12, edge.Value);
                }
                else if (i == 1 && edge.Key == 3 || i == 3 && edge.Key == 1)
                {
                    Assert.AreEqual(7, edge.Value);
                }
                else
                {
                    Assert.Fail();
                }
            }
        }
    }

    [Test]
    public void AdjacencyListToEdgeListTest()
    {
        var adjacencyList = new Dictionary<int, int>[4];
        for (int i = 0; i < adjacencyList.Length; i++)
        {
            adjacencyList[i] = new Dictionary<int, int>();
        }

        adjacencyList[0].Add(2, 15);
        adjacencyList[2].Add(0, 15);
        adjacencyList[1].Add(3, 8);
        adjacencyList[3].Add(1, 8);
        adjacencyList[0].Add(1, 10);
        adjacencyList[1].Add(0, 10);

        var edges = GraphRepresentation.AdjacencyListToEdgeList(adjacencyList);
        Assert.AreEqual(3, edges.Count);

        foreach (var edge in edges)
        {
            if (edge.FirstVertex == 0 && edge.SecondVertex == 2 ||
                edge.FirstVertex == 2 && edge.SecondVertex == 0)
            {
                Assert.AreEqual(15, edge.Weight);
            }
            else if (edge.FirstVertex == 1 && edge.SecondVertex == 3 ||
                edge.FirstVertex == 3 && edge.SecondVertex == 1)
            {
                Assert.AreEqual(8, edge.Weight);
            }
            else if (edge.FirstVertex == 0 && edge.SecondVertex == 1 ||
                edge.FirstVertex == 1 && edge.SecondVertex == 0)
            {
                Assert.AreEqual(10, edge.Weight);
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
