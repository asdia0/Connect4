namespace Connect4
{
    using System;
    using static System.Math;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// This class contains methods to find the move.
    /// </summary>
    public static class Solve
    {
        /// <summary>
        /// An implementation of the alpha-beta pruning algorithm.
        /// </summary>
        /// <param name="game">The position to solve.</param>
        /// <param name="depth">The amount of full moves to search through.</param>
        /// <param name="alpha">The value of alpha (maximising player).</param>
        /// <param name="beta">The value of beta (minimising player).</param>
        /// <returns></returns>
        private static (int?, int) AlphaBetaPruning(Game game, int depth, int alpha, int beta)
        {
            bool maxPlayer = game.Turn % 2 == 0;

            List<int> children = new();

            for (int col = 0; col < game.Grid.Length; col++)
            {
                if (!game.IsFilled(col))
                {
                    children.Add(col);
                }
            }

            if (game.Draw)
            {
                return (null, 0);
            }

            foreach (int child in children)
            {
                if (game.IsWinningMove(child))
                {
                    return (child, maxPlayer ? int.MaxValue : int.MinValue);
                }
            }

            if (depth == 0)
            {
                return (null, game.Evaluation(maxPlayer ? 0 : 1));
            }

            if (maxPlayer)
            {
                int value = int.MinValue;
                Dictionary<int, int> scores = new();

                foreach (int child in children)
                {
                    Game opp = new(game);
                    opp.Play(child);
                    int score = AlphaBetaPruning(opp, depth - 1, alpha, beta).Item2;
                    scores.Add(child, score);
                    if (score > value)
                    {
                        value = score;
                    }
                    alpha = Max(alpha, value);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return (scores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key, value);
            }
            else
            {
                int value = int.MaxValue;
                Dictionary<int, int> scores = new();

                foreach (int child in children)
                {
                    Game opp = new(game);
                    opp.Play(child);
                    int score = AlphaBetaPruning(opp, depth - 1, alpha, beta).Item2;
                    scores.Add(child, score);
                    if (score < value)
                    {
                        value = score;
                    }
                    beta = Max(beta, value);
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return (scores.Aggregate((l, r) => l.Value > r.Value ? l : r).Key, value);
            }
        }

        /// <summary>
        /// Finds the best move from a given position.
        /// </summary>
        /// <param name="g">The position to find the best move in.</param>
        /// <param name="depth">The amount of full moves to search.</param>
        /// <returns>(Column to search in, the column's score)</returns>
        public static (int?, int) FindBestColumn(Game g, int depth)
        {
            if (g.Draw || g.Winner != null || g.Players != 2)
            {
                throw new Exception("Invalid game");
            }

            (int?, int) eval = AlphaBetaPruning(g, 2 * depth, int.MinValue, int.MaxValue);

            return eval;
        }
    }
}
