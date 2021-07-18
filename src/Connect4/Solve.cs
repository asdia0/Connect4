namespace Connect4
{
    using System;
    using static System.Math;
    using System.Linq;
    using System.Collections.Generic;

    public static class Solve
    {
        private static (int?, int) MiniMax(Game game, int depth, int alpha, int beta)
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
                    int score = MiniMax(opp, depth - 1, alpha, beta).Item2;
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
                    int score = MiniMax(opp, depth - 1, alpha, beta).Item2;
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

        public static (int?, int) FindBestColumn(Game g, int depth)
        {
            if (g.Draw || g.Winner != null || g.Players != 2)
            {
                throw new Exception("Invalid game");
            }

            (int?, int) eval = MiniMax(g, 2 * depth, int.MinValue, int.MaxValue);

            return eval;
        }
    }
}
