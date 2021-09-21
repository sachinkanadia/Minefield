using Minefield.Core.Board;
using Minefield.Core.Squares;
using NUnit.Framework;
namespace Minefield.Test
{
    public class CoordinateTests
    {
        private const int ROW_SIZE = 8;
        private const int COLUMN_SIZE = 8;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetCoordinate()
        {
            var board = new Board();

            for (var r = 0; r < ROW_SIZE; r++)
            {
                for (var c = 0; c < COLUMN_SIZE; c++)
                {
                    var coordinate = new Coordinate(board.Squares[r, c]);

                    string column = null;

                    switch(c)
                    {
                        case 0:
                            column = "A";
                            break;
                        case 1:
                            column = "B";
                            break;
                        case 2:
                            column = "C";
                            break;
                        case 3:
                            column = "D";
                            break;
                        case 4:
                            column = "E";
                            break;
                        case 5:
                            column = "F";
                            break;
                        case 6:
                            column = "G";
                            break;
                        case 7:
                            column = "H";
                            break;
                    }

                    Assert.AreEqual(coordinate.Column, column);
                    Assert.AreEqual(coordinate.Row, (r + 1).ToString());
                }
            }
        }
    }
}