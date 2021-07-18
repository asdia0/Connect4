namespace Connect4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Grid
    {
        public Token[] Tokens { get; set; }

        public int Length { get; set; }

        public int Breadth { get; set; }

        public Grid(int length, int breadth)
        {
            if (length < 1 || breadth < 1)
            {
                throw new Exception("The dimensions of the grid must be at least 1.");
            }

            this.Length = length;
            this.Breadth = breadth;
            this.Tokens = new Token[length * breadth];

            for (int id = 0; id < this.Tokens.Length; id++)
            {
                this.Tokens[id] = new Token(id);
            }
        }

        public Grid(Grid grid)
        {
            this.Tokens = new Token[grid.Tokens.Length];
            this.Length = grid.Length;
            this.Breadth = grid.Breadth;

            for (int i = 0; i < grid.Tokens.Count(); i++)
            {
                this.Tokens[i] = new Token(grid.Tokens[i]);
            }
        }

        public List<List<int>> GetColumns(int length)
        {
            List<List<int>> result = new List<List<int>>();

            for (int x = 0; x < this.Length; x++)
            {
                for (int y = 0; y < this.Breadth - length + 1; y++)
                {
                    int startID = (y * this.Length) + x;

                    List<int> column = new List<int>();

                    for (int increment = 0; increment < length; increment++)
                    {
                        column.Add(startID + (increment * this.Length));
                    }

                    result.Add(column);
                }
            }

            return result;
        }

        public List<List<int>> GetRows(int length)
        {
            List<List<int>> result = new List<List<int>>();

            for (int x = 0; x < this.Breadth; x++)
            {
                for (int y = 0; y < this.Length - (length - 1); y++)
                {
                    int startID = (x * this.Length) + y;

                    List<int> row = new List<int>();

                    for (int increment = 0; increment < length; increment++)
                    {
                        row.Add(startID + increment);
                    }

                    result.Add(row);
                }
            }

            return result;
        }

        public List<List<int>> GetDiagonals(int length)
        {
            List<List<int>> result = new List<List<int>>();

            // Positive diagonals
            for (int x = 0; x < this.Length - length + 1; x++)
            {
                for (int y = 0; y < this.Breadth - length + 1; y++)
                {
                    int startID = (y * this.Length) + x;

                    List<int> diagonal = new List<int>();

                    for (int increment = 0; increment < length; increment++)
                    {
                        diagonal.Add(startID + (increment * (this.Length + 1)));
                    }

                    result.Add(diagonal);
                }
            }

            // Negative diagonals
            for (int x = this.Length - length + 1; x <= this.Length; x++)
            {
                for (int y = 0; y < this.Breadth - length + 1; y++)
                {
                    int startID = (y * this.Length) + x;

                    List<int> diagonal = new List<int>();

                    for (int increment = 0; increment < length; increment++)
                    {
                        diagonal.Add(startID + (increment * (this.Length - 1)));
                    }

                    result.Add(diagonal);
                }
            }

            return result;
        }

        public override string ToString()
        {
            string s = string.Empty;
            int counter = 0;

            foreach (Token t in this.Tokens)
            {
                if (t.Player == null)
                {
                    s += "[ ]";
                }
                else
                {
                    s += $"[{t.Player}]";
                }

                counter++;

                if (counter % this.Length == 0)
                {
                    s += "\n";
                }
            }

            return s;
        }
    }
}
