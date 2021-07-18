using System;
using System.Linq;
using System.Collections.Generic;

namespace Connect4.Example
{
    class Program
    {
        static void Main()
        {
            Grid grid = new(3, 3);
            Game game = new(grid, 2, 2);
            game.Play(0);
            game.Play(0);
            game.Play(2);

            Console.WriteLine(grid);
            Console.WriteLine(game.Winner);
        }
    }
}
