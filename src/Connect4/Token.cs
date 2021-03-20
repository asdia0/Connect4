namespace Connect4
{
    public class Token
    {
        public int? Player = null;

        public int ID;

        public Token(int id)
        {
            this.ID = id; ;
        }

        public Token(Token token)
        {
            this.Player = token.Player;
            this.ID = token.ID;
        }
    }
}
