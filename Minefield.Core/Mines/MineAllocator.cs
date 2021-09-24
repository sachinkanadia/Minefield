using Minefield.Core.Board;
using System;
namespace Minefield.Core.Mines
{
    public class MineAllocator : IMineAllocator
    {
        internal MineAllocator() { }

        public IBoard AllocateMines(IBoard board, int minesToAllocate)
        {
            var rndm = new Random();

            var rows = board.Squares.GetLength(0);
            var columns = board.Squares.GetLength(1);

            for (var i = 0; i < minesToAllocate; i++)
            {                
                var row = rndm.Next(0, rows);
                var col = rndm.Next(0, columns);

                if (board.Squares[row, col].HasMine)
                {
                    //Square already has mine in it so retry.
                    i--;
                    continue;
                }

                board.Squares[row, col].HasMine = true;
            }

            return board;
        }

        public IBoard ClearMines(IBoard board)
        {
            var rows = board.Squares.GetLength(0);
            var columns = board.Squares.GetLength(1);

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < columns; c++)
                {
                    board.Squares[r, c].HasMine = false;
                }
            }

            return board;
        }
    }
}
