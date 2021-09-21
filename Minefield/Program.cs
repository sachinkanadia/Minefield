using Minefield.Core.Game;
using System;
namespace Minefield
{
    class Program
    {
        private const int MINES = 10;
        private const int LIVES = 3;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Minefield!");
            var game = new Game();
            var exit = false;

            while (!exit)
            {
                var resetComplete = false;
                while (!resetComplete)
                {
                    try
                    {
                        var startSquare = game.ResetNewGame(LIVES, MINES);
                        Console.WriteLine($"There are {MINES} mines and you have {LIVES} lives remaining");
                        var startCoordinate = startSquare.ToCoordinate();
                        Console.WriteLine($"Your starting position is {startCoordinate.Column}{startCoordinate.Row}");
                        resetComplete = true;
                    }
                    catch (ArgumentNullException) //Incase the game cannot set a start square because of too many mines.
                    {
                        resetComplete = false;
                    }
                }

                var inGame = true;
                while (inGame)
                {
                    Console.WriteLine("Please select an arrow key to make your move");
                    var keyInfo = Console.ReadKey();
                    Console.WriteLine();

                    IMove move = null;

                    try
                    {
                        switch (keyInfo.Key)
                        {
                            case ConsoleKey.UpArrow:
                                move = game.MoveUp();
                                break;
                            case ConsoleKey.DownArrow:
                                move = game.MoveDown();
                                break;
                            case ConsoleKey.LeftArrow:
                                move = game.MoveLeft();
                                break;
                            case ConsoleKey.RightArrow:
                                move = game.MoveRight();
                                break;
                            default:
                                Console.WriteLine("Invalid key!");
                                break;
                        }
                    }
                    catch (IndexOutOfRangeException) //Incase you move off the board.
                    {
                        Console.WriteLine("You've moved off the board! Please try again");
                        continue;
                    }

                    if (move != null)
                    {
                        if (move.CurrentPosition.HasMine)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You hit a mine!");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        var coordinate = move.CurrentPosition.ToCoordinate();
                        if (move.IsGameOver)
                        {
                            if (move.OutOfLives)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("You lost!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (move.ReachedOtherSide)
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("You won!");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                        }                        
                        Console.WriteLine($"Number of moves {move.NumberOfMoves}");
                        Console.WriteLine($"Number of lives {move.NumberOfLivesLeft}");
                        Console.WriteLine($"Current Position {coordinate.Column}{coordinate.Row}");
                        Console.WriteLine();
                        if (move.IsGameOver)
                            inGame = false;
                    }
                }

                Console.WriteLine("Press any key to restart or press X to exit");                
                var exitkeyInfo = Console.ReadKey();
                Console.WriteLine();

                if (exitkeyInfo.Key == ConsoleKey.X)
                {
                    exit = true;
                }
            }
        }
    }
}