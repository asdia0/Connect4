namespace Connect4
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    class Program
    {
        public static int nodeCount;

        public static int[] ColumnOrder;

        public static Dictionary<string, int> Moves = new Dictionary<string, int>();

        private static void SortColumnOrder(int length)
        {
            ColumnOrder = new int[length];
            for (int i = 0; i < length; i++)
            {
                ColumnOrder[i] = length / 2 + (1 - 2 * (i % 2)) * (i + 1) / 2;
            }
        }

        public static int FindBestColumn(int length, int breadth, int toWin)
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

            if (g.Turn % 2 == 0)
            {
                return firstMoves.Aggregate((l, r) => l.Value < r.Value ? l : r).Key;
            }
            else
            {
                return firstMoves.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            }
        }

        public static int NegaMax(Game game, int alpha, int beta, int depth)
        {
            // Credits to http://blog.gamesolver.org/solving-connect-four/04-alphabeta/

            nodeCount++;

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
