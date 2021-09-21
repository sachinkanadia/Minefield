using Minefield.Core.Board;
namespace Minefield.Core.Mines
{
    public interface IMineAllocator
    {
        IBoard AllocateMines(IBoard board, int minesToAllocate);
        IBoard ClearMines(IBoard board);
    }
}