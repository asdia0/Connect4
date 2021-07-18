namespace Connect4
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a grid.
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// Gets or sets the tokens on the grid.
        /// </summary>
        public Token[] Tokens { get; set; }

        /// <summary>
        /// Gets or sets the grid's length.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the grid's breadth.
        /// </summary>
        public int Breadth { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Grid"/> class.
        /// </summary>
        /// <param name="length">The grid's length.</param>
        /// <param name="breadth">The grid's breadth.</param>
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

        /// <summary>
        /// Initialises a new instance of the <see cref="Grid"/> class from another grid.
        /// </summary>
        /// <param name="grid">The grid to clone.</param>
        public Grid(Grid grid)
        {
            this.Tokens = new Token[grid.Tokens.Length];
            this.Length = grid.Length;
            this.Breadth = grid.Breadth;

            for (int i = 0; i < grid.Tokens.Length; i++)
            {
                this.Tokens[i] = new Token(grid.Tokens[i]);
            }
        }

        /// <summary>
        /// Gets a list of columns of a certain length.
        /// </summary>
        /// <param name="length">The length of the columns to find.</param>
        /// <returns>A list of columns of a certain length.</returns>
        public List<List<int>> GetColumns(int length)
        {
            List<List<int>> result = new();

            for (int x = 0; x < this.Length; x++)
            {
                for (int y = 0; y < this.Breadth - length + 1; y++)
                {
                    int startID = (y * this.Length) + x;

                    List<int> column = new();

                    for (int increment = 0; increment < length; increment++)
                    {
                        column.Add(startID + (increment * this.Length));
                    }

                    result.Add(column);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a list of rows of a certain length.
        /// </summary>
        /// <param name="length">The length of the rows to find.</param>
        /// <returns>A list of rows of a certain length.</returns>
        public List<List<int>> GetRows(int length)
        {
            List<List<int>> result = new();

            for (int x = 0; x < this.Breadth; x++)
            {
                for (int y = 0; y < this.Length - (length - 1); y++)
                {
                    int startID = (x * this.Length) + y;

                    List<int> row = new();

                    for (int increment = 0; increment < length; increment++)
                    {
                        row.Add(startID + increment);
                    }

                    result.Add(row);
                }
            }

            return result;
        }

        /// <summary>
        /// Gets a list of diagonals of a certain length.
        /// </summary>
        /// <param name="length">The length of the diagonals to find.</param>
        /// <returns>A list of diagonals of a certain length.</returns>
        public List<List<int>> GetDiagonals(int length)
        {
            List<List<int>> result = new();

            // Positive diagonals
            for (int x = 0; x < this.Length - (length - 1); x++)
            {
                for (int y = 0; y < this.Breadth - (length - 1); y++)
                {
                    int startID = (y * this.Length) + x;

                    List<int> diagonal = new();

                    for (int increment = 0; increment < length; increment++)
                    {
                        diagonal.Add(startID + (increment * (this.Length + 1)));
                    }

                    result.Add(diagonal);
                }
            }

            // Negative diagonals
            for (int y = 0; y < this.Breadth - (length - 1); y++)
            {
                for (int x = length - 1; x < this.Length; x++)
                {
                    int startID = (y * this.Length) + x;

                    List<int> diagonal = new();

                    for (int increment = 0; increment < length; increment++)
                    {
                        diagonal.Add(startID + (increment * (this.Length - 1)));
                    }

                    result.Add(diagonal);
                }
            }

            return result;
        }

        /// <summary>
        /// Converts the grid into a string.
        /// </summary>
        /// <returns>The string version of the grid.</returns>
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
