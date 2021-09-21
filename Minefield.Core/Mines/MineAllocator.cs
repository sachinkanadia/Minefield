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

            for (var i = 0; i < minesToAllocate; i++)
            {
                var row = rndm.Next(0, Globals.BOARD_ROW_SIZE);
                var col = rndm.Next(0, Globals.BOARD_COLUMN_SIZE);

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
            for (var r = 0; r < Globals.BOARD_ROW_SIZE; r++)
            {
                for (var c = 0; c < Globals.BOARD_COLUMN_SIZE; c++)
                {
                    board.Squares[r, c].HasMine = false;
                }
            }

            return board;
        }
    }
}
