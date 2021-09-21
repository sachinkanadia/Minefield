using Minefield.Core.Board;
using Minefield.Core.Game;
using Minefield.Core.Mines;
using Minefield.Core.Player;
using NUnit.Framework;
using System;
namespace Minefield.Test
{
    public class GameTests
    {
        private const int MINES_TO_ALLOCATE = 10;
        private const int NUMBER_OF_LIVES = 10;
        private const int COLUMN_SIZE = 8;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PlayGameWithOutResettingFirstTest()
        {
            var game = new Game();
            Assert.Throws<InvalidOperationException>(() => game.MoveUp());
        }

        [Test]
        public void ResetNewGameNoStartSquareTest()
        {
            IBoard board = new Board();

            IBoard errorBoard = new Board();            
            for (var c = 0; c < COLUMN_SIZE; c++)
            {
                errorBoard.Squares[0, c].HasMine = true;
            }

            var mockMineAllocator = new Moq.Mock<IMineAllocator>();
            mockMineAllocator.Setup(ma => ma.ClearMines(board)).Returns(board);
            mockMineAllocator.Setup(ma => ma.AllocateMines(board, MINES_TO_ALLOCATE)).Returns(errorBoard);

            IPlayer player = new Player();

            var game = new Game(board, mockMineAllocator.Object, player);

            Assert.Throws<ArgumentNullException>(() => game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE));
        }

        [Test]
        public void MoveDownOffBoardTest()
        {
            IBoard board = new Board();
            IMineAllocator mineAllocator = new MineAllocator();
            IPlayer player = new Player();

            var game = new Game(board, mineAllocator, player);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            Assert.Throws<IndexOutOfRangeException>(() => game.MoveDown());
        }

        [Test]
        public void MoveUpOffBoardTest()
        {
            IBoard board = new Board();
            IMineAllocator mineAllocator = new MineAllocator();
            var player = new Moq.Mock<IPlayer>();
            player.SetupGet(p => p.CurrentPosition).Returns(new Core.Squares.Square(7, 0));

            var game = new Game(board, mineAllocator, player.Object);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            Assert.Throws<IndexOutOfRangeException>(() => game.MoveUp());
        }

        [Test]
        public void MoveLeftOffBoardTest()
        {
            IBoard board = new Board();
            IMineAllocator mineAllocator = new MineAllocator();
            var player = new Moq.Mock<IPlayer>();
            player.SetupGet(p => p.CurrentPosition).Returns(new Core.Squares.Square(0, 0));

            var game = new Game(board, mineAllocator, player.Object);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            Assert.Throws<IndexOutOfRangeException>(() => game.MoveLeft());
        }

        [Test]
        public void MoveRightOffBoardTest()
        {
            IBoard board = new Board();
            IMineAllocator mineAllocator = new MineAllocator();
            var player = new Moq.Mock<IPlayer>();
            player.SetupGet(p => p.CurrentPosition).Returns(new Core.Squares.Square(0, 7));

            var game = new Game(board, mineAllocator, player.Object);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            Assert.Throws<IndexOutOfRangeException>(() => game.MoveRight());
        }

        [Test]
        public void HitMineTest()
        {
            IBoard board = new Board();
            for (var c = 0; c < COLUMN_SIZE; c++)
            {
                board.Squares[1, c].HasMine = true;
            }

            var mockMineAllocator = new Moq.Mock<IMineAllocator>();
            mockMineAllocator.Setup(ma => ma.ClearMines(board)).Returns(board);
            mockMineAllocator.Setup(ma => ma.AllocateMines(board, MINES_TO_ALLOCATE)).Returns(board);

            IPlayer player = new Player();

            var game = new Game(board, mockMineAllocator.Object, player);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);
            var move = game.MoveUp();

            Assert.IsTrue(move.CurrentPosition.HasMine);
            Assert.AreEqual(NUMBER_OF_LIVES - 1, move.NumberOfLivesLeft);
            Assert.AreEqual(1, move.NumberOfMoves);
        }

        [Test]
        public void GameOverAsNoLivesLeftThenResetTest()
        {
            IBoard board = new Board();
            for (var c = 0; c < COLUMN_SIZE; c++)
            {
                board.Squares[1, c].HasMine = true;
            }

            var mockMineAllocator = new Moq.Mock<IMineAllocator>();
            mockMineAllocator.Setup(ma => ma.ClearMines(board)).Returns(board);
            mockMineAllocator.Setup(ma => ma.AllocateMines(board, MINES_TO_ALLOCATE)).Returns(board);

            IPlayer player = new Player();

            var game = new Game(board, mockMineAllocator.Object, player);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);
            
            game.MoveUp();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveLeft();
            var move = game.MoveLeft();

            Assert.IsTrue(move.IsGameOver);
            Assert.IsTrue(move.CurrentPosition.HasMine);
            Assert.AreEqual(0, move.NumberOfLivesLeft);
            Assert.AreEqual(NUMBER_OF_LIVES, move.NumberOfMoves);

            game = new Game(board, mockMineAllocator.Object, player);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            game.MoveUp();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveLeft();
            move = game.MoveLeft();

            Assert.IsTrue(move.IsGameOver);
            Assert.IsTrue(move.CurrentPosition.HasMine);
            Assert.AreEqual(0, move.NumberOfLivesLeft);
            Assert.AreEqual(NUMBER_OF_LIVES, move.NumberOfMoves);
        }

        [Test]
        public void GameOverAsReachedTheOtherSideThenResetTest()
        {
            IBoard board = new Board();
            for (var c = 0; c < COLUMN_SIZE; c++)
            {
                board.Squares[1, c].HasMine = true;
            }

            var mockMineAllocator = new Moq.Mock<IMineAllocator>();
            mockMineAllocator.Setup(ma => ma.ClearMines(board)).Returns(board);
            mockMineAllocator.Setup(ma => ma.AllocateMines(board, MINES_TO_ALLOCATE)).Returns(board);

            IPlayer player = new Player();

            var game = new Game(board, mockMineAllocator.Object, player);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            var move = game.MoveUp();

            Assert.IsTrue(move.IsGameOver);
            Assert.IsFalse(move.CurrentPosition.HasMine);
            Assert.AreEqual(9, move.NumberOfLivesLeft);
            Assert.AreEqual(7, move.NumberOfMoves);

            game = new Game(board, mockMineAllocator.Object, player);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            move = game.MoveUp();

            Assert.IsTrue(move.IsGameOver);
            Assert.IsFalse(move.CurrentPosition.HasMine);
            Assert.AreEqual(9, move.NumberOfLivesLeft);
            Assert.AreEqual(7, move.NumberOfMoves);
        }

        [Test]
        public void GameOverButStillTryingThenResetTest()
        {
            IBoard board = new Board();
            for (var c = 0; c < COLUMN_SIZE; c++)
            {
                board.Squares[1, c].HasMine = true;
            }

            var mockMineAllocator = new Moq.Mock<IMineAllocator>();
            mockMineAllocator.Setup(ma => ma.ClearMines(board)).Returns(board);
            mockMineAllocator.Setup(ma => ma.AllocateMines(board, MINES_TO_ALLOCATE)).Returns(board);

            IPlayer player = new Player();

            var game = new Game(board, mockMineAllocator.Object, player);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();

            //Keep on moving even though game has ended.
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();

            var move = game.MoveUp();

            Assert.IsTrue(move.IsGameOver);
            Assert.IsFalse(move.CurrentPosition.HasMine);
            Assert.AreEqual(9, move.NumberOfLivesLeft);
            Assert.AreEqual(7, move.NumberOfMoves);

            game = new Game(board, mockMineAllocator.Object, player);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            game.MoveUp();
            move = game.MoveUp();

            Assert.IsTrue(move.IsGameOver);
            Assert.IsFalse(move.CurrentPosition.HasMine);
            Assert.AreEqual(9, move.NumberOfLivesLeft);
            Assert.AreEqual(7, move.NumberOfMoves);
        }

        [Test]
        public void GameOverAsNoLivesLeftAndStillTryingThenResetTest()
        {
            IBoard board = new Board();
            for (var c = 0; c < COLUMN_SIZE; c++)
            {
                board.Squares[1, c].HasMine = true;
            }

            var mockMineAllocator = new Moq.Mock<IMineAllocator>();
            mockMineAllocator.Setup(ma => ma.ClearMines(board)).Returns(board);
            mockMineAllocator.Setup(ma => ma.AllocateMines(board, MINES_TO_ALLOCATE)).Returns(board);

            IPlayer player = new Player();

            var game = new Game(board, mockMineAllocator.Object, player);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            game.MoveUp();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveLeft();

            //Keep on moving even though game has ended.
            game.MoveLeft();
            game.MoveLeft();
            game.MoveLeft();
            game.MoveLeft();
            game.MoveLeft();
            game.MoveLeft();
            game.MoveLeft();
            game.MoveLeft();

            var move = game.MoveLeft();

            Assert.IsTrue(move.IsGameOver);
            Assert.IsTrue(move.CurrentPosition.HasMine);
            Assert.AreEqual(0, move.NumberOfLivesLeft);
            Assert.AreEqual(NUMBER_OF_LIVES, move.NumberOfMoves);

            game = new Game(board, mockMineAllocator.Object, player);
            game.ResetNewGame(NUMBER_OF_LIVES, MINES_TO_ALLOCATE);

            game.MoveUp();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveRight();
            game.MoveLeft();
            move = game.MoveLeft();

            Assert.IsTrue(move.IsGameOver);
            Assert.IsTrue(move.CurrentPosition.HasMine);
            Assert.AreEqual(0, move.NumberOfLivesLeft);
            Assert.AreEqual(NUMBER_OF_LIVES, move.NumberOfMoves);
        }
    }
}