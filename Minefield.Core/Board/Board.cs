using Minefield.Core.Squares;
namespace Minefield.Core.Board
{
    public class Board : IBoard
    {
        internal Board()
        {
            for (var r = 0; r < Globals.BOARD_ROW_SIZE; r++)
            {
                for (var c = 0; c < Globals.BOARD_COLUMN_SIZE; c++)
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