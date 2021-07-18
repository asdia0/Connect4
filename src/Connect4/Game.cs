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
        /// Gets or sets the ID of the current player.
        /// </summary>
        public int Turn { get; set; }

        /// <summary>
        /// Gets or sets the grid the game is being played on.
        /// </summary>
        public Grid Grid { get; set; }
        
        /// <summary>
        /// Gets or sets a list of moves played.
        /// </summary>
        public List<Move> MoveList { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="grid">The grid the game is being played on.</param>
        /// <param name="players">The number of players in the game.</param>
        /// <param name="toWin">The number of tokens needed in a row to win.</param>
        public Game(Grid grid, int players, int toWin)
        {
            // Multiple players

            if (players < 2)
            {
                throw new Exception("Number of players must be greater than or equal to 2.");
            }

            this.Turn = 0;
            this.Grid = grid;
            this.Players = players;
            this.ToWin = toWin;
        }

        /// <summary>
        /// Iniitalises a new instance of the <see cref="Game"/> from another game.
        /// </summary>
        /// <param name="game">The game to clone.</param>
        public Game(Game game)
        {
            this.ToWin = game.ToWin;
            this.Players = game.Players;
            this.Turn = 0;
            this.Grid = new Grid(game.Grid.Length, game.Grid.Breadth);

            this.MoveList = new List<Move>();

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
                    this.Turn++;
                    if (this.Turn == this.Players)
                    {
                        this.Turn = 0;
                    }
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
                    this.Turn--;
                    if (this.Turn == 0)
                    {
                        this.Turn = this.Players;
                    }
                    return;
                }
            }
        }

        /// <summary>
        /// Gets the winner if the game has ended.
        /// </summary>
        /// <returns>The ID of the player who won.</returns>
        public int? GetWinner()
        {
            List<List<int>> streaks = this.Grid.GetColumns(this.ToWin).Union(this.Grid.GetRows(this.ToWin).Union(this.Grid.GetDiagonals(this.ToWin))).ToList();

            foreach (List<int> streak in streaks)
            {
                int counter = 0;
                foreach (int id in streak)
                {
                    int? player = this.Grid.Tokens[id].Player;
                    if (player == null)
                    {
                        break;
                    }
                    else if (player != this.Grid.Tokens[streak[0]].Player)
                    {
                        break;
                    }
                    else
                    {
                        counter++;
                    }
                }

                if (counter == this.ToWin)
                {
                    return this.Grid.Tokens[streak[0]].Player;
                }
            }

            return null;
        }

        /// <summary>
        /// Determines if the game is a draw.
        /// </summary>
        /// <returns>A value determining if the game is a draw.</returns>
        public bool IsDraw()
        {
            if (this.MoveList.Count == this.Grid.Breadth * this.Grid.Length)
            {
                return true;
            }

            return false;
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
            try
            {
                Game g = new Game(this);
                g.Play(column);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Determines if a move played in the column wins the game.
        /// </summary>
        /// <param name="column">The column to check.</param>
        /// <returns>A value determinig if a move played in the column wins the game.</returns>
        public bool IsWinningMove(int column)
        {
            Game g = new Game(this);
            g.Play(column);
            if (g.GetWinner() != null)
            {
                return true;
            }

            return false;
        }
    }
}
