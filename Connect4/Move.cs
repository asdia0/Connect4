namespace Connect4
{
    using System;
    public class Move
    {
        public int X;
        public int Y;

        public int Player;

        public Move(Grid grid, Token token)
        {
            if (token.Player == null)
            {
                throw new Exception("Token.Player must not be null.");
            }

            this.X = token.ID % grid.Length;
            this.Y = grid.Breadth - (int)Math.Floor(decimal.Divide(token.ID, grid.Breadth));

            this.Player = (int)token.Player;
        }

        public override string ToString()
        {
            return $"Player {this.Player}: {(this.X + 1, this.Y + 1)}";
        }
    }
}
