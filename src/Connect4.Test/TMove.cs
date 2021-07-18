namespace Connect4.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TMove
    {
        [TestMethod]
        public void Move()
        {
            int pid = 0;
            Token t = new(0);
            t.Player = pid;
            Move move = new(new(2, 2), t);

            Assert.AreEqual((0, 2), move.Coordinates);
            Assert.AreEqual(pid, move.Player);
        }
    }
}