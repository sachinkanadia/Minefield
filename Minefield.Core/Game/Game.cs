using Minefield.Core.Board;
using Minefield.Core.Mines;
using Minefield.Core.Player;
using Minefield.Core.Squares;
using System;
namespace Minefield.Core.Game
{
    public class Game
    {
        public Game() : this(new Board.Board(), new MineAllocator(), new Player.Player()) { }
        internal Game(IBoard board, IMineAllocator mineAllocator, IPlayer player)
        {
            _Board = board;
            _MineAllocator = mineAllocator;
            _Player = player;
        }

        private IBoard _Board;
        private IPlayer _Player;
        private readonly IMineAllocator _MineAllocator;

        public Square ResetNewGame(int numberOfLives, int minesToAllocate)
        {
            _Board = _MineAllocator.ClearMines(_Board);
            _Board = _MineAllocator.AllocateMines(_Board, minesToAllocate);

            Square startSquare = null;
            for (var c = 0; c < Globals.BOARD_COLUMN_SIZE; c++)
            {
                if (_Board.Squares[0, c].HasMine)
                    continue;

                startSquare = _Board.Squares[0, c];
                break;
            }

            if (startSquare == null)
                throw new ArgumentNullException("startSquare", "Could not set a start square because there are too many mines! Please try again");

            _Player.Reset(numberOfLives, startSquare);
            return startSquare;
        }

        private IMove Move(int row, int col)
        {
            if (_Player.IsGameOver)
                return _Player;

            if (row < 0 || row > (Globals.BOARD_ROW_SIZE - 1))
                throw new IndexOutOfRangeException();
            if (col < 0 || col > (Globals.BOARD_COLUMN_SIZE - 1))
                throw new IndexOutOfRangeException();

            var nextPosition = _Board.Squares[row, col];
            _Player.MoveTo(nextPosition);
            
            return _Player;
        }

        public IMove MoveUp()
        {
            if (_Player.CurrentPosition == null)
            {
                throw new InvalidOperationException("Please call ResetNewGame before moving");
            }

            var currentPosition = _Player.CurrentPosition;
            var nextRow = currentPosition.Row + 1;
            var nextCol = currentPosition.Column;
            return Move(nextRow, nextCol);
        }

        public IMove MoveDown()
        {
            if (_Player.CurrentPosition == null)
            {
                throw new InvalidOperationException("Please call ResetNewGame before moving");
            }

            var currentPosition = _Player.CurrentPosition;
            var nextRow = currentPosition.Row - 1;
            var nextCol = currentPosition.Column;
            return Move(nextRow, nextCol);
        }

        public IMove MoveLeft()
        {
            if (_Player.CurrentPosition == null)
            {
                throw new InvalidOperationException("Please call ResetNewGame before moving");
            }

            var currentPosition = _Player.CurrentPosition;
            var nextRow = currentPosition.Row;
            var nextCol = currentPosition.Column - 1;
            return Move(nextRow, nextCol);
        }

        public IMove MoveRight()
        {
            if (_Player.CurrentPosition == null)
            {
                throw new InvalidOperationException("Please call ResetNewGame before moving");
            }

            var currentPosition = _Player.CurrentPosition;
            var nextRow = currentPosition.Row;
            var nextCol = currentPosition.Column + 1;
            return Move(nextRow, nextCol);
        }
    }
}
