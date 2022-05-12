namespace CommonClasses.UnitTests;
using NUnit.Framework;
using CommonClasses;

public class DSUTests
{
    [Test]
    public void FindSetAfterInitTest()
    {
        var dsu = new DisjointSetUnion(10);
        for (int i = 0; i < 10; i++)
        {
            Assert.AreEqual(i, dsu.FindSet(i));
        }
    }

    [Test]
    public void UnionSetsTest()
    {
        var dsu = new DisjointSetUnion(10);
        for (int i = 0; i < 10 - 1; i++)
        {
            dsu.UnionSets(i, i + 1);
        }

        for (int i = 0; i < 10 - 1; i++)
        {
            Assert.AreEqual(dsu.FindSet(i), dsu.FindSet(i + 1));
        }
    }
}
