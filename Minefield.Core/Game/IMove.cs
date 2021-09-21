using Minefield.Core.Squares;
namespace Minefield.Core.Game
{
    public interface IMove
    {
        public Square CurrentPosition { get; }
        public int NumberOfLivesLeft { get; }
        public int NumberOfMoves { get; }
        public bool OutOfLives { get; }
        public bool ReachedOtherSide { get; }
        public bool IsGameOver { get; }
    }
}
