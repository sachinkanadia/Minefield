using Minefield.Core.Squares;

namespace Minefield.Core.Board
{
    public interface IBoard
    {
        Square[,] Squares { get; }
    }
}