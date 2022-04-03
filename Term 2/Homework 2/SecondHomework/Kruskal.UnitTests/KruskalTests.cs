using NUnit.Framework;

namespace Kruskal.UnitTests
{
    public class KruskalTests
    {
        [Test]
        public void InitTest()
        {
            var edge = new Edge(3, 5, 12);
            Assert.AreEqual(3, edge.FirstVertex);
            Assert.AreEqual(5, edge.SecondVertex);
            Assert.AreEqual(12, edge.Length);
        }
    }
}
