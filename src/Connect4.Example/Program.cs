using System;

namespace Connect4.Example
{
    class Program
    {
        static void Main()
        {
            // Simulate a game of Connect 4
            Grid grid = new(7, 6);
            Game game = new(grid, 2, 4);

            while (true)
            {
                if (game.Draw || game.Winner != null)
                {
                    break;
                }

                (int?, int) e = Solve.FindBestColumn(game, 1);
                Console.WriteLine(e);
                game.Play((int)e.Item1);
            }
        }
    }
}
