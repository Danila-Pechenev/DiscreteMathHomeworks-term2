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
            if (edge.FirstVertex == 2 && edge.SecondVertex == 0 || edge.FirstVertex == 0 && edge.SecondVertex == 2)
            {
                Assert.AreEqual(10, edge.Length);
            }
            else if (edge.FirstVertex == 1 && edge.SecondVertex == 3 || edge.FirstVertex == 3 && edge.SecondVertex == 1)
            {
                Assert.AreEqual(2, edge.Length);
            }
            else
            {
                Assert.AreEqual(5, edge.Length);
            }
        }
    }
}
