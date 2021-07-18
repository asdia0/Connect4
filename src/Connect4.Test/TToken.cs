namespace Connect4.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TToken
    {
        [TestMethod]
        public void TokenInt()
        {
            int id = 0;
            Token t = new(id);

            Assert.AreEqual(id, t.ID);
            Assert.AreEqual(null, t.Player);
        }

        [TestMethod]
        public void TokenToken()
        {
            int tid = 0;
            int pid = 1;
            Token clone = new Token(tid);
            clone.Player = pid;

            Token t = new(clone);

            Assert.AreEqual(tid, t.ID);
            Assert.AreEqual(pid, t.Player);
        }
    }
}