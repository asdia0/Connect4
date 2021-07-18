namespace Connect4
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Game
    {
        public int ToWin { get; set; }

        public int Players { get; set; }

        public int Turn { get; set; }

        public Grid Grid { get; set; }

        public List<Move> MoveList = new List<Move>();

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

        public bool IsDraw()
        {
            if (this.MoveList.Count == this.Grid.Breadth * this.Grid.Length)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            string s = string.Empty;

            foreach (Move move in this.MoveList)
            {
                s += move.ToString();
            }

            return s;
        }

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
