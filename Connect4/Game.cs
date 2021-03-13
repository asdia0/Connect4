namespace Connect4
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Game
    {
        public int ToWin;

        public int Players;

        public int Turn;

        public Grid Grid;

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

        public void Move(int column)
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

        public int? Winner()
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
    }
}
