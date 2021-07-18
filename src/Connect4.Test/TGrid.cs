namespace Connect4.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TGrid
    {
        [TestMethod]
        public void GridIntInt()
        {
            Grid grid = new(2, 3);

            Assert.AreEqual(2, grid.Length);
            Assert.AreEqual(3, grid.Breadth);
            Assert.AreEqual(6, grid.Tokens.Length);
        }

        [TestMethod]
        public void GridGrid()
        {
            Grid clone = new(2, 3);
            Grid grid = new(clone);

            Assert.AreEqual(2, grid.Length);
            Assert.AreEqual(3, grid.Breadth);
            Assert.AreEqual(6, grid.Tokens.Length);
        }

        [TestMethod]
        public void GetRows()
        {
            Grid grid = new(3, 3);

            Assert.AreEqual(6, grid.GetRows(2).Count);
        }

        [TestMethod]
        public void GetColumns()
        {
            Grid grid = new(3, 3);

            Assert.AreEqual(6, grid.GetColumns(2).Count);
        }

        [TestMethod]
        public void GetDiagonals()
        {
            Grid grid = new(3, 3);

            Assert.AreEqual(8, grid.GetDiagonals(2).Count);
        }
    }
}