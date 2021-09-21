using Minefield.Core.Board;
using Minefield.Core.Squares;
using NUnit.Framework;
namespace Minefield.Test
{
    public class BoardTests
    {
        private const int ROW_SIZE = 8;
        private const int COLUMN_SIZE = 8;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestCreateBoard()
        {
            var board = new Board();

            for (var r = 0; r < ROW_SIZE; r++)
            {
                for (var c = 0; c < COLUMN_SIZE; c++)
                {
                    Assert.IsTrue(board.Squares[r, c] is Square);
                    Assert.AreEqual(board.Squares[r, c].Column, c);
                    Assert.AreEqual(board.Squares[r, c].Row, r);
                }
            }
        }
    }
}