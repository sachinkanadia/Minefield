namespace Minefield.Core.Player
{
    public class Player : IPlayer
    {
        internal Player() { }

        private int _LivesStartedWith;
        private Squares.Square _StartPosition;
        private Squares.Square _CurrentPosition;
        private int _NumberOfMoves;
        private int _NumberOfLivesLeft;

        public Squares.Square CurrentPosition
        {
            get
            {
                return _CurrentPosition;
            }
        }

        public int NumberOfLivesLeft
        {
            get 
            {
                return _NumberOfLivesLeft;
            }
        }

        public int NumberOfMoves
        {
            get
            {
                return _NumberOfMoves;
            }
        }

        public bool OutOfLives
        {
            get 
            {
                return NumberOfLivesLeft == 0;
            }
        }

        public bool ReachedOtherSide
        {
            get
            {
                return CurrentPosition.Row == (Globals.BOARD_ROW_SIZE - 1);
            }
        }

        public bool IsGameOver
        {
            get
            {
                return OutOfLives || ReachedOtherSide;
            }
        }

        public void Reset(int numberOfLives, Squares.Square startPosition)
        {
            _LivesStartedWith = numberOfLives;
            _NumberOfLivesLeft = _LivesStartedWith;
            _StartPosition = startPosition;
            _CurrentPosition = _StartPosition;
            _NumberOfMoves = 0;
        }

        public void MoveTo(Squares.Square square)
        {
            if (_NumberOfLivesLeft == 0)
                return;

            this._CurrentPosition = square;

            if (this._CurrentPosition.HasMine)
                _NumberOfLivesLeft--;

            _NumberOfMoves++;
        }
    }
}
