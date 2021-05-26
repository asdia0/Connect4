namespace Connect4
{
    using System;

    public class Move
    {
        public (int X, int Y) Coordinates;
        public int Player;

        public Move(Grid grid, Token token)
        {
            if (token.Player == null)
            {
                throw new Exception("Token.Player must not be null.");
            }

            this.Coordinates.X = token.ID % grid.Length;
            this.Coordinates.Y = grid.Breadth - (int)Math.Floor(decimal.Divide(token.ID, grid.Breadth));

            this.Player = (int)token.Player;
        }

        public override string ToString()
        {
            return this.Coordinates.ToString();
        }
    }
}
