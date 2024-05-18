namespace _2048.logic
{
    public class Game
    {
        internal Board Board { get; set; }
        public GameStatus Status { get; set; }
        public int Points { get; protected set; }

        public Game()
        {
            this.Board = new Board();
            this.Status = GameStatus.Idle;
            this.Points = 0;
        }

        /// <summary>
        /// Attempts to move the tiles on the board in the specified direction, updates the game status and points accordingly,
        /// and checks if the player has won or lost.
        /// </summary>
        /// <param name="direction">The direction in which to move the tiles.</param>
        public void Move(Direction direction)
        {
            if (Status != GameStatus.Lose)
            {
                int pointsEarned = Board.Move(direction);
                Points += pointsEarned;

                //checking if the player won
                if (Points > Cell.WinningCell)
                {
                    if (Board.IsThereAWinningCell())
                    {
                        Status = GameStatus.Win;
                    }
                }

                // checking of the player lost 
                else if (!Board.AreThereAnyEmptyCells())
                {
                    Status = GameStatus.Lose;
                }

            }
        }
    }
}
