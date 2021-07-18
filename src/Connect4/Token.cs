namespace Connect4
{
    public struct Token
    {
        public int? Player { get; set; }

        public int ID { get; set; }

        public Token(int id)
        {
            this.ID = id;
            this.Player = null;
        }

        public Token(Token token)
        {
            this.Player = token.Player;
            this.ID = token.ID;
        }
    }
}
