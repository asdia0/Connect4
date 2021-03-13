namespace Connect4
{
    using System;
    using System.Linq;
    using System.Text.Json;

    class Program
    {
        public static void Main(string[] args)
        {
            bool lengthDone = false, breadthDone = false, goFirstDone = false, toWinDone = false;
            int length = 0, breadth = 0, goFirst = 0, toWin = 4;

            while (!lengthDone)
            {
                Console.WriteLine("Length of grid: ");
                if (int.TryParse(Console.ReadLine(), out length))
                {
                    lengthDone = true;
                    Console.Clear();
                }
            }


            while (!breadthDone)
            {
                Console.WriteLine("Breadth of grid: ");
                if (int.TryParse(Console.ReadLine(), out breadth))
                {
                    breadthDone = true;
                    Console.Clear();
                }
            }

            while (!toWinDone)
            {
                Console.WriteLine("Number of tokens in a row to win: ");
                if (int.TryParse(Console.ReadLine(), out toWin))
                {
                    toWinDone = true;
                    Console.Clear();
                }
            }


            while (!goFirstDone)
            {
                Console.WriteLine("Would you like to go first? (Y/N)");
                string response = Console.ReadLine();
                if (response.ToLower() == "y")
                {
                    goFirst = 0;
                    goFirstDone = true;
                    Console.Clear();
                }
                else if (response.ToLower() == "n")
                {
                    goFirst = 1;
                    goFirstDone = true;
                    Console.Clear();
                }
            }

            Game game = new Game(new Grid(length, breadth), 2, 4);

            bool gameOn = true;

            while (gameOn)
            {
                Console.WriteLine(game.Grid);
                if (game.MoveList.Any())
                {
                    Console.WriteLine(game.MoveList[^1]);
                }

                if (game.Turn == goFirst)
                {
                    bool moveDone = false;
                    int column;

                    while (!moveDone)
                    {
                        Console.WriteLine("Column: ");
                        if (int.TryParse(Console.ReadLine(), out column))
                        {
                            try
                            {
                                game.Move(column);
                                Console.Clear();
                                moveDone = true;
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                }
                else
                {
                    bool ok = false;
                    while (!ok)
                    {
                        Random rnd = new Random();
                        try
                        {
                            game.Move(rnd.Next(0, length));
                            ok = true;
                            Console.Clear();
                        }
                        catch
                        {

                        }
                    }
                }

                int? winner = game.Winner();
                if (winner != null)
                {
                    Console.Clear();
                    Console.WriteLine($"Player {winner} won!");
                    gameOn = false;
                }
            }
        }
    }
}
