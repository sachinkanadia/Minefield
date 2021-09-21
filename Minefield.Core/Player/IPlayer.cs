using Minefield.Core.Game;
using Minefield.Core.Squares;
namespace Minefield.Core.Player
{
    public interface IPlayer : IMove
    {
        void Reset(int numberOfLives, Square startPosition);
        void MoveTo(Square square);
    }
}