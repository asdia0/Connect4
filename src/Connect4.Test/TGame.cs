namespace Connect4.Test
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TGame
    {
        [TestMethod]
        public void GameGridIntInt()
        {
            Grid grid = new(2, 2);
            Game game = new(grid, 2, 2);

            Assert.AreEqual(grid, game.Grid);

            Assert.AreEqual(2, game.ToWin);

            Assert.AreEqual(2, game.Players);

            Assert.AreEqual(0, game.MoveList.Count);

            Assert.AreEqual(0, game.Turn);

            game.Play(0);

            Assert.AreEqual(1, game.Turn);

            game.Play(1);

            Assert.AreEqual(0, game.Turn);

            Assert.AreEqual(2, game.MoveList.Count);

            game.Play(0);

            Assert.AreEqual(0, game.Winner);
        }

        [TestMethod]
        public void GameGameEmpty()
        {
            Grid grid = new(2, 2);
            Game clone = new(grid, 2, 2);
            Game game = new(clone);

            Assert.AreEqual(grid.Length, game.Grid.Length);
            Assert.AreEqual(grid.Breadth, game.Grid.Breadth);
            Assert.AreEqual(grid.Tokens.Length, game.Grid.Tokens.Length);

            Assert.AreEqual(2, game.ToWin);

            Assert.AreEqual(2, game.Players);

            Assert.AreEqual(0, game.MoveList.Count);

            Assert.AreEqual(0, game.Turn);

            game.Play(0);

            Assert.AreEqual(1, game.Turn);

            game.Play(1);

            Assert.AreEqual(0, game.Turn);

            Assert.AreEqual(2, game.MoveList.Count);
        }

        [TestMethod]
        public void GameGamePlaying()
        {
            Grid grid = new(2, 2);
            Game clone = new(grid, 2, 2);
            clone.Play(1);
            Game game = new(clone);

            Assert.AreEqual(grid.Length, game.Grid.Length);
            Assert.AreEqual(grid.Breadth, game.Grid.Breadth);
            Assert.AreEqual(grid.Tokens.Length, game.Grid.Tokens.Length);

            Assert.AreEqual(2, game.ToWin);

            Assert.AreEqual(2, game.Players);

            Assert.AreEqual(1, game.MoveList.Count);

            Assert.AreEqual(1, game.Turn);

            game.Play(1);

            Assert.AreEqual(0, game.Turn);

            Assert.AreEqual(2, game.MoveList.Count);
        }

        [TestMethod]
        public void GameGameWinner()
        {
            Grid grid = new(2, 2);
            Game clone = new(grid, 2, 2);
            clone.Play(1);
            clone.Play(0);
            clone.Play(1);
            Game game = new(clone);

            Assert.AreEqual(grid.Length, game.Grid.Length);
            Assert.AreEqual(grid.Breadth, game.Grid.Breadth);
            Assert.AreEqual(grid.Tokens.Length, game.Grid.Tokens.Length);
            Assert.AreEqual(2, game.ToWin);
            Assert.AreEqual(2, game.Players);
            Assert.AreEqual(3, game.MoveList.Count);
            Assert.AreEqual(null, game.Turn);
            Assert.AreEqual(0, game.Winner);
        }

        [TestMethod]
        public void Play()
        {
            Grid grid = new(2, 2);
            Game game = new(grid, 2, 2);

            game.Play(0);

            Assert.AreEqual(1, game.MoveList.Count);
            Assert.AreEqual(1, game.Turn);
            Assert.AreEqual(0, game.Grid.Tokens[2].Player);
        }
    }
}
