﻿namespace TronGame
{
    using System;
    using System.Runtime.InteropServices;
    using System.Threading;
    public class Startup
    {
        private static int left = 0;
        private static int right = 1;
        private static int up = 2;
        private static int down = 3;

        static int firstPlayerScore = 0;
        static int firstPlayerDirection = right;
        static int firstPlayerColumn = 0;
        static int firstPlayerRow = 15;

        static int secondPlayerScore = 0;
        static int secondPlayerDirection = left;
        static int secondPlayerColumn = 99;
        static int secondPlayerRow = 15;

        static bool[,] isUsed;

        public static void Main(string[] args)
        {
            SetGameField();
            isUsed = new bool[Console.WindowWidth, Console.WindowHeight];

            while (true)
            {
                var speed = 100;
               if(Console.KeyAvailable)
               { 
                    var key = Console.ReadKey(true);
                    ChangePlayerDirection(key);
               }

                MovePlayer();

                var firstPlayerLoses = DoesPlayersLose(firstPlayerRow, firstPlayerColumn);
                var secondPlayerLoses = DoesPlayersLose(secondPlayerRow, secondPlayerColumn);

                if (firstPlayerLoses && secondPlayerLoses)
                {
                    firstPlayerScore++;
                    secondPlayerScore++;
                    Console.WriteLine();
                    Console.WriteLine("Come over");
                    Console.WriteLine("Draw game!!!");
                    Console.WriteLine($"Current score: {firstPlayerScore} - {secondPlayerScore}");
                    ResetGame();
                }
                else if (firstPlayerLoses)
                {
                    secondPlayerScore++;
                    Console.WriteLine();
                    Console.WriteLine("Game over");
                    Console.WriteLine("Second player wins!");
                    Console.WriteLine($"Current score: {firstPlayerScore} - {secondPlayerScore}");
                    ResetGame(); ;
                }
                else if (secondPlayerLoses)
                {
                    firstPlayerScore++;
                    Console.WriteLine();
                    Console.WriteLine("Game over");
                    Console.WriteLine("First player wins!");
                    Console.WriteLine($"Current score: {firstPlayerScore} - {secondPlayerScore}");
                    ResetGame();
                }

                isUsed[firstPlayerColumn, firstPlayerRow] = true;
                isUsed[secondPlayerColumn, secondPlayerRow] = true;

                WriteOnPosition(firstPlayerColumn, firstPlayerRow, '*', ConsoleColor.Blue);
                WriteOnPosition(secondPlayerColumn, secondPlayerRow, '*', ConsoleColor.DarkYellow);
               
             Thread.Sleep(speed);
                speed -= 10;
            }
        }

        private static void ResetGame()
        {
            isUsed = new bool[Console.WindowWidth, Console.WindowHeight];
            firstPlayerDirection = right;
            secondPlayerDirection = left;
            Console.WriteLine("Press any key to start again");
            Console.ReadKey();
            Console.Clear();
            Console.Beep();
            MovePlayer();
        }

        private static bool DoesPlayersLose(int row, int col)
        {
            if (row < 0)
            {
                return true;
            }
            else if (col < 0)
            {
                return true;
            }
            else if (row >= Console.WindowHeight)
            {
                return true;
            }
            else if (col >= Console.WindowWidth)
            {
                return true;
            }
            else if (isUsed[col, row])
            {
                return true;
            }
            return false;
        }

        private static void SetGameField()
        {
            Console.WindowHeight = 30;
            Console.BufferHeight = 30;

            Console.WindowWidth = 100;
            Console.BufferWidth = 100;

            
        }

        private static void MovePlayer()
        {
            //FirstPlayerMove
            if(firstPlayerDirection == right)
            {
                firstPlayerColumn++;
            }
            else if (firstPlayerDirection == left)
            {
                firstPlayerColumn--;
            }
            else if(firstPlayerDirection == up)
            {
                firstPlayerRow--;
            }
            else if (firstPlayerDirection == down)
            {
                firstPlayerRow++;
            }

            //SecondPlayerMove
            if (secondPlayerDirection == right)
            {
                secondPlayerColumn++;
            }
            else if (secondPlayerDirection == left)
            {
                secondPlayerColumn--;
            }
            else if (secondPlayerDirection == up)
            {
                secondPlayerRow--;
            }
            else if (secondPlayerDirection == down)
            {
                secondPlayerRow++;
            }
        }

        static void WriteOnPosition(int x, int y, char c, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        private static void ChangePlayerDirection(ConsoleKeyInfo key)
        {

            switch (key.Key.ToString())
            {
                case "W":
                    firstPlayerDirection = up;
                    break;
                case "A":
                    firstPlayerDirection = left;
                    break;;
                case "D":
                    firstPlayerDirection = right;
                    break;
                case "S":
                    firstPlayerDirection = down;
                    break;;
                case "UpArrow":
                    secondPlayerDirection = up;
                    break;
                case "LeftArrow":
                    secondPlayerDirection = left;
                    break;
                case "RightArrow":
                    secondPlayerDirection = right;
                    break;
                case "DownArrow":
                    secondPlayerDirection = down;
                    break;
            }           
        }
    }
}
