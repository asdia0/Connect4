namespace Connect4
{
    /// <summary>
    /// Defines a token.
    /// </summary>
    public struct Token
    {
        /// <summary>
        /// Gets or sets the player that placed the token.
        /// </summary>
        public int? Player { get; set; }

        /// <summary>
        /// Gets or sets the token's ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Initialises a new instance of the <see cref="Token"/> struct.
        /// </summary>
        /// <param name="id">The token's ID.</param>
        public Token(int id)
        {
            this.ID = id;
            this.Player = null;
        }

        /// <summary>
        /// Initiaises a new instance of the <see cref="Token"/> struct from another token.
        /// </summary>
        /// <param name="token">The token to clone.</param>
        public Token(Token token)
        {
            this.Player = token.Player;
            this.ID = token.ID;
        }
    }
}
