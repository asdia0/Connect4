namespace Connect4
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a game.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Gets or sets the number of tokens needed in a row to win.
        /// </summary>
        public int ToWin { get; set; }

        /// <summary>
        /// Gets or sets the number of players in the game.
        /// </summary>
        public int Players { get; set; }

        /// <summary>
        /// Gets the ID of the current player.
        /// </summary>
        public int? Turn
        {
            get
            {
                if (this.Winner != null || this.Draw)
                {
                    return null;
                }

                return this.MoveList.Count % this.Players;
            }
        }

        /// <summary>
        /// Gets or sets the grid the game is being played on.
        /// </summary>
        public Grid Grid { get; set; }
        
        /// <summary>
        /// Gets or sets a list of moves played.
        /// </summary>
        public List<Move> MoveList { get; set; }

        /// <summary>
        /// Gets the winner.
        /// </summary>
        public int? Winner
        {
            get
            {
                List<List<int>> streaks = this.Grid.GetColumns(this.ToWin).Union(this.Grid.GetRows(this.ToWin).Union(this.Grid.GetDiagonals(this.ToWin))).ToList();

                foreach (List<int> streak in streaks)
                {
                    foreach (int id in streak)
                    {
                        List<int?> dist = streak.Select(i => this.Grid.Tokens[i].Player).Distinct().ToList();

                        if (dist.Count == 1)
                        {
                            if (dist[0] != null)
                            {
                                return dist[0];
                            }
                        }
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a value determining if the game is a draw.
        /// </summary>
        public bool Draw
        {
            get
            {
                if (this.MoveList.Count == this.Grid.Breadth * this.Grid.Length)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="grid">The grid the game is being played on.</param>
        /// <param name="players">The number of players in the game.</param>
        /// <param name="toWin">The number of tokens needed in a row to win.</param>
        public Game(Grid grid, int players, int toWin)
        {
            if (players < 2)
            {
                throw new Exception("Number of players must be greater than or equal to 2.");
            }

            this.Grid = grid;
            this.Players = players;
            this.ToWin = toWin;
            this.MoveList = new();
        }

        /// <summary>
        /// Iniitalises a new instance of the <see cref="Game"/> from another game.
        /// </summary>
        /// <param name="game">The game to clone.</param>
        public Game(Game game)
        {
            this.ToWin = game.ToWin;
            this.Players = game.Players;
            this.Grid = new Grid(game.Grid.Length, game.Grid.Breadth);
            this.MoveList = new();

            foreach (char c in game.ToString())
            {
                this.Play(int.Parse(c.ToString()));
            }
        }

        /// <summary>
        /// Places a token in a column.
        /// </summary>
        /// <param name="column">The column to play.</param>
        public void Play(int column)
        {
            int[] tokens = new int[this.Grid.Breadth];

            for (int row = 0; row < this.Grid.Breadth; row++)
            {
                tokens[row] = (row * this.Grid.Length) + column;
            }

            foreach (int id in tokens.Reverse())
            {
                if (this.Grid.Tokens[id].Player == null)
                {
                    this.Grid.Tokens[id].Player = this.Turn;
                    this.MoveList.Add(new Move(this.Grid, this.Grid.Tokens[id]));
                    return;
                }
            }

            throw new Exception("Column is full.");
        }

        /// <summary>
        /// Removes the last token placed in a column
        /// </summary>
        /// <param name="column">The column to remove the token from.</param>
        public void Undo(int column)
        {
            int[] tokens = new int[this.Grid.Breadth];

            for (int row = 0; row < this.Grid.Breadth; row++)
            {
                tokens[row] = (row * this.Grid.Length) + column;
            }

            foreach (int id in tokens.Reverse())
            {
                if (this.Grid.Tokens[id].Player != null)
                {
                    this.Grid.Tokens[id].Player = null;
                    this.MoveList.RemoveAt(MoveList.Count - 1);
                    return;
                }
            }
        }

        /// <summary>
        /// Converts the game to a string.
        /// </summary>
        /// <returns>A string version of the game.</returns>
        public override string ToString()
        {
            string s = string.Empty;

            foreach (Move move in this.MoveList)
            {
                s += move.ToString();
            }

            return s;
        }

        /// <summary>
        /// Determines if the column is full.
        /// </summary>
        /// <param name="column">The column to check.</param>
        /// <returns>A value determining if the column is full.</returns>
        public bool IsFilled(int column)
        {
            int[] tokens = new int[this.Grid.Breadth];

            for (int row = 0; row < this.Grid.Breadth; row++)
            {
                tokens[row] = (row * this.Grid.Length) + column;
            }

            return !tokens.Select(i => this.Grid.Tokens[i]).Where(i => i.Player == null).Any();
        }

        /// <summary>
        /// Determines if a move played in the column wins the game.
        /// </summary>
        /// <param name="column">The column to check.</param>
        /// <returns>A value determinig if a move played in the column wins the game.</returns>
        public bool IsWinningMove(int column)
        {
            Game g = new(this);
            g.Play(column);
            return g.Winner != null;
        }

        public int Evaluation(int player)
        {
            List<List<int>> streaks = this.Grid.GetColumns(this.ToWin).Union(this.Grid.GetRows(this.ToWin).Union(this.Grid.GetDiagonals(this.ToWin))).ToList();

            int score = 0;

            foreach (List<int> streak in streaks)
            {
                List<int?> tokens = streak.Select(i => this.Grid.Tokens[i].Player).ToList();
                List<int?> dist = tokens.Distinct().ToList();
                dist.Remove(null);

                if (dist.Count == 1)
                {
                    if (dist[0] == player)
                    {
                        score += 10 / (this.ToWin - tokens.Where(i => i == player).Count());
                    }
                    else
                    {
                        score -= 10 / (this.ToWin - tokens.Where(i => i == player).Count());
                    }
                }
            }

            return score;
        }
    }
}
