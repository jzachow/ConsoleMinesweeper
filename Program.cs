using System;
using System.Drawing;

namespace GroupDMinefieldMidterm
{
    class Program
    {
        static void Main(string[] args)
        {
            bool userContinue = true;
            Console.SetWindowSize(48, 64);

            Console.WriteLine("Welcome to Group D's Minesweeper game!");

            while (userContinue)
            {
                Minesweeper minesweeper = new Minesweeper();

                while (!minesweeper.GameOver && !minesweeper.GameWon)                
                    minesweeper.GetUserSelection();
                

                if (minesweeper.GameWon)                
                    Display.GameEndScreen(true);                
                else 
                    Display.GameEndScreen(false);

                string userInput = MinesweeperValidator.ValidateContinue();

                if (userInput.Equals("NO") || userInput.Equals("N"))
                    userContinue = false;
                
            }
        }
    }
}
