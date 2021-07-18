namespace Connect4
{
    using System;

    public class Move
    {
        public (int X, int Y) Coordinates { get; set; }
        public int Player { get; set; }

        public Move(Grid grid, Token token)
        {
            if (token.Player == null)
            {
                throw new Exception("Token.Player must not be null.");
            }

            this.Coordinates = (token.ID % grid.Length, grid.Breadth - (int)Math.Floor(decimal.Divide(token.ID, grid.Breadth)));

            this.Player = (int)token.Player;
        }

        public override string ToString()
        {
            return this.Coordinates.X.ToString();
        }
    }
}
