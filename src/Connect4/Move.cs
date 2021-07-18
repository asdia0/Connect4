namespace Connect4
{
    using System;

    /// <summary>
    /// Defines a move.
    /// </summary>
    public struct Move
    {
        /// <summary>
        /// The coordinates of the token placed.
        /// </summary>
        public (int X, int Y) Coordinates { get; set; }

        /// <summary>
        /// The player that made the move.
        /// </summary>
        public int Player { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Move"/> struct.
        /// </summary>
        /// <param name="grid">The grid where the move was made.</param>
        /// <param name="token">The token that was moved.</param>
        public Move(Grid grid, Token token)
        {
            if (token.Player == null)
            {
                throw new Exception("Token.Player must not be null.");
            }

            this.Coordinates = (token.ID % grid.Length, grid.Breadth - (int)Math.Floor(decimal.Divide(token.ID, grid.Breadth)));

            this.Player = (int)token.Player;
        }

        /// <summary>
        /// Converts the move into a string.
        /// </summary>
        /// <returns>A string version of the x-coordinate of <see cref="Coordinates"/>.</returns>
        public override string ToString()
        {
            return this.Coordinates.X.ToString();
        }
    }
}
