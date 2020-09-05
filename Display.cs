using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;


namespace GroupDMinefieldMidterm
{
    public static class Display
    {
        public static void DisplayBoard(GameBoard gameBoard)
        {          
           
            DisplayScore(gameBoard.RemainingCells);
            DisplayColumns(gameBoard.BoardColumns);

            for (int i = 0; i < gameBoard.BoardRows; i++)
            {
                char label = (char)(i + 65);
                Console.WriteLine();
                Console.Write($"{label}");
                Console.Write("|");

                StringBuilder cellOutput = new StringBuilder("");               

                for (int j = 0; j < gameBoard.BoardColumns; j++)
                {
                    cellOutput.Clear();
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (gameBoard.Board[i, j].Flagged)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;                        
                        cellOutput.Append($" F ");                       
                    }
                    else if (gameBoard.Board[i, j].Revealed)
                    {
                        GameValues cellValue = gameBoard.Board[i, j].CellValue;
                        Console.BackgroundColor = ConsoleColor.DarkGray;

                        switch (cellValue)
                        {
                            case GameValues.Empty:
                                cellOutput.Append("   ");
                                break;
                            case GameValues.Mine:
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                cellOutput.Append(" M ");
                                break;
                            default:                                
                                cellOutput.Append($" {(int)cellValue} ");
                                break;
                        }                        
                    }
                    else
                    {                        
                        Console.BackgroundColor = ConsoleColor.Gray;
                        cellOutput.Append(" * ");                        
                    }
                    Console.Write(cellOutput);
                    Console.ResetColor();
                }
                Console.Write("|");
            }
            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < gameBoard.BoardColumns; i++)
            {
                Console.Write("---");
            }
        }

        private static void DisplayColumns(int columns)
        {
            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < columns; i++)
            {
                Console.Write($"{i:00} ");
            }
            Console.WriteLine();
            Console.Write("  ");
            for (int i = 0; i < columns; i++)
            {
                Console.Write("---");
            }
        }

        private static void DisplayScore(int score)
        {
            Console.WriteLine("\n -------------------------------");
            Console.WriteLine($"|\tRemaining cells: {score}\t|");
            Console.WriteLine(" -------------------------------");
        }

        public static void GameEndScreen(bool winOrLose)
        {
            string gameEndMessage;

            if (winOrLose)
            {
                gameEndMessage = "Congratulations! You've won the game!!!!";
            }
            else
            {
                gameEndMessage = "Try better next time. You've lost the game!!!!";
            }

            Console.WriteLine("\n\n----------------------------------------------");
            Console.WriteLine(gameEndMessage);
            Console.WriteLine("----------------------------------------------");
        }
    }
}
