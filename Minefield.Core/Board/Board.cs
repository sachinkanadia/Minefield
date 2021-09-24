using Minefield.Core.Squares;
namespace Minefield.Core.Board
{
    public class Board : IBoard
    {
        internal Board()
        {
            var rows = _Squares.GetLength(0);
            var columns = _Squares.GetLength(1);

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < columns; c++)
                {
                    _Squares[r, c] = new Square(r, c);
                }
            }
        }

        private readonly Square[,] _Squares = new Square[Globals.BOARD_ROW_SIZE, Globals.BOARD_COLUMN_SIZE];

        public Square[,] Squares
        {
            get
            {
                return _Squares;
            }
        }
    }
}