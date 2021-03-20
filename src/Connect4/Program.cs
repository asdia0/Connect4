namespace Connect4
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Text.Json;

    class Program
    {
        public static int nodeCount;

        public static int[] ColumnOrder;

        public static Dictionary<string, int> Moves = new Dictionary<string, int>();

        static void Main(string[] args)
        {
            nodeCount = 0;
            Find(7, 6, 4);
        }

        public static void Play()
        {
            bool lengthDone = false, breadthDone = false, goFirstDone = false, toWinDone = false;
            int length = 0, breadth = 0, goFirst = 0, toWin = 4;

            while (!lengthDone)
            {
                Console.Write("Length of grid: ");
                if (int.TryParse(Console.ReadLine(), out length))
                {
                    lengthDone = true;
                    Console.Clear();
                }
            }


            while (!breadthDone)
            {
                Console.Write("Breadth of grid: ");
                if (int.TryParse(Console.ReadLine(), out breadth))
                {
                    breadthDone = true;
                    Console.Clear();
                }
            }

            while (!toWinDone)
            {
                Console.Write("Number of tokens in a row to win: ");
                if (int.TryParse(Console.ReadLine(), out toWin))
                {
                    toWinDone = true;
                    Console.Clear();
                }
            }


            while (!goFirstDone)
            {
                Console.Write("Would you like to go first? (Y/N): ");
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
                Console.WriteLine(game);

                if (game.MoveList.Any())
                {
                    Console.WriteLine($"Computer: Column {game.MoveList[^1]}");
                }

                if (game.Turn == goFirst)
                {
                    bool moveDone = false;
                    int column;

                    while (!moveDone)
                    {
                        Console.Write("Column: ");
                        if (int.TryParse(Console.ReadLine(), out column))
                        {
                            try
                            {
                                game.Play(column);
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
                            game.Play(rnd.Next(0, length));
                            ok = true;
                            Console.Clear();
                        }
                        catch
                        {

                        }
                    }
                }

                if (game.Draw())
                {
                    Console.Clear();
                    Console.WriteLine("Draw.");
                    gameOn = false;
                }
                else
                {
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

        public static void SortColumnOrder(int length)
        {
            ColumnOrder = new int[length];
            for (int i = 0; i < length; i++)
            {
                ColumnOrder[i] = length / 2 + (1 - 2 * (i % 2)) * (i + 1) / 2;
            }
        }

        public static void Find(int length, int breadth, int toWin)
        {
            Game g = new Game(new Grid(length, breadth), 2, toWin);

            Moves.Clear();

            SortColumnOrder(length);

            int score = NegaMax(g, int.MinValue, int.MaxValue, 0);

            Dictionary<int, int> firstMoves = new Dictionary<int, int>();
            for (int i = 0; i < length; i++)
            {
                if (Moves.Keys.Contains($"{g}{i}"))
                {
                    firstMoves.Add(i, Moves[$"{g}{i}"]);
                }
            }

            int col;

            if (g.Turn % 2 == 0)
            {
                col = firstMoves.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
            }
            else
            {
                col = firstMoves.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            }

            Console.WriteLine($"Column: {col}");
        }

        public static int NegaMax(Game game, int alpha, int beta, int depth)
        {
            // Credits to http://blog.gamesolver.org/solving-connect-four/04-alphabeta/

            // TODO: Store to transposition table
            // TODO: Load transposition table
            // TODO: Load openings

            nodeCount++;

            Console.WriteLine($"Node {nodeCount} (Depth {game.MoveList.Count})");
            Grid grid = game.Grid;

            if (game.Draw())
            {
                return 0;
            }

            // Player can win immediately
            for (int x = 0; x < grid.Length; x++)
            {
                if (game.CanPlay(x) && game.WinningMove(x))
                {
                    return (grid.Length * grid.Breadth + 1 - game.MoveList.Count) / 2;
                }
            }

            // Upper bound of score
            int max = (grid.Length * grid.Breadth - 1 - game.MoveList.Count) / 2;
            
            // Configure beta
            if (beta > max)
            {
                beta = max;
                if (alpha >= beta)
                {
                    return beta;
                }
            }

            // Get child scores
            for (int x = 0; x < grid.Length; x++)
            {
                if (game.CanPlay(ColumnOrder[x]))
                {
                    game.Play(ColumnOrder[x]);

                    int score = -NegaMax(game, -beta, -alpha, depth + 1);

                    game.Undo(ColumnOrder[x]);

                    if (Moves.Keys.Contains(game.ToString()))
                    {
                        return Moves[game.ToString()];
                    }

                    Moves.Add(game.ToString(), score);

                    // Prune exploration if we find a better move
                    if (score >= beta)
                    {
                        return score;
                    }
                    if (score > alpha)
                    {
                        alpha = score;
                    }
                }
            }

            return alpha;
        }
    }
}
