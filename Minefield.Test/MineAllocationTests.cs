using Minefield.Core.Board;
using Minefield.Core.Mines;
using NUnit.Framework;
namespace Minefield.Test
{
    public class MineAllocationTests
    {
        private const int MINES_TO_ALLOCATE = 10;
        private const int ROW_SIZE = 8;
        private const int COLUMN_SIZE = 8;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestMineAllocation()
        {
            IBoard board = new Board();
            var mineAllocator = new MineAllocator();

            board = mineAllocator.AllocateMines(board, MINES_TO_ALLOCATE);

            var minesFound = 0;

            for (var r = 0; r < ROW_SIZE; r++)
            {
                for (var c = 0; c < COLUMN_SIZE; c++)
                {
                    if (board.Squares[r, c].HasMine)
                        minesFound++;
                }
            }

            Assert.AreEqual(MINES_TO_ALLOCATE, minesFound);

            board = mineAllocator.ClearMines(board);

            minesFound = 0;
            for (var r = 0; r < ROW_SIZE; r++)
            {
                for (var c = 0; c < COLUMN_SIZE; c++)
                {
                    if (board.Squares[r, c].HasMine)
                        minesFound++;
                }
            }

            Assert.AreEqual(0, minesFound);
        }
    }
}