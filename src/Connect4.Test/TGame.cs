namespace Connect4.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TGame
    {
        [TestMethod]
        public void Turn()
        {
            Game game = new(new(2, 2), 2, 2);

            Assert.AreEqual(0, game.Turn);

            game.Play(0);

            Assert.AreEqual(1, game.Turn);

            game.Play(1);

            Assert.AreEqual(0, game.Turn);
        }
    }
}
