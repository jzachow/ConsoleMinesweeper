using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace GroupDMinefieldMidterm
{
    public class Minesweeper
    {
        public bool GameOver { get; set; }
        public bool GameWon { get; set; }
        public GameBoard Game { get; set; }

        public Minesweeper()
        {
            GameOver = false;
            GameWon = false;
            Game = new GameBoard(GetUserDifficulty());
            Display.DisplayBoard(Game);
        }

        public string GetUserDifficulty()
        {
            Console.WriteLine("\nWould you like to play a game?");
            Console.WriteLine("Select your difficulty - 1: Beginner 2: Intermediate 3: Expert");
            string userInput = Console.ReadLine();

            bool parsed = int.TryParse(userInput, out int userDifficulty);

            while (!parsed || (userDifficulty < 1) || (userDifficulty > 3))
            {
                Console.WriteLine("Invalid entry. Please enter 1, 2, or 3.");
                userInput = Console.ReadLine();
                parsed = int.TryParse(userInput, out userDifficulty);
            }

            switch (userDifficulty)
            {
                case 1:
                    return "Beginner";
                case 2:
                    return "Intermediate";
                case 3:
                    return "Expert";
                default:
                    break;
            }
            return "";
        }

        public void GetUserSelection()
        {
            bool checkFlag = CheckOrFlag();
            int columnInput = GetUserColumn();
            int rowInput = GetUserRow();
            Point point = new Point(rowInput, columnInput);

            if (checkFlag)
                Game.CheckCell(point);
            else
                Game.FlagCell(point);


            if (Game.HitMine)
                GameOver = true;

            if (Game.RemainingCells <= 0)
                GameWon = true;

            if (Game.FlaggedAllMines)
                GameWon = true;

            Display.DisplayBoard(Game);
        }


        private int GetUserColumn()
        {
            Console.WriteLine("\nPlease enter column");
            string userInput = Console.ReadLine();

            bool parsed = int.TryParse(userInput, out int userColumn);

            while (!parsed || userColumn < 0 || userColumn > (Game.BoardColumns - 1))
            {
                Console.WriteLine("\nInvalid entry. Please enter a valid column.");
                userInput = Console.ReadLine();
                parsed = int.TryParse(userInput, out userColumn);
            }

            return userColumn;
        }

        private int GetUserRow()
        {
            var upperBound = Game.BoardRows + 64;
            Console.WriteLine("\nPlease enter row");
            string userInput = Console.ReadLine().ToUpper();
            char userRow;

            while ((!Char.TryParse(userInput, out userRow)) || (userRow < 'A') || userRow > (char)upperBound)
            {
                Console.WriteLine("\nInvalid entry. Please enter a valid row");
                userInput = Console.ReadLine().ToUpper();
            }

            return (int)(userRow - 65);
        }

        private bool CheckOrFlag()
        {
            Console.WriteLine("\nEnter 'c' to check a cell or 'f' to flag/unflag a cell: ");
            string userInput = Console.ReadLine();

            while (!userInput.Equals("c", StringComparison.CurrentCultureIgnoreCase) &&
                   !userInput.Equals("f", StringComparison.CurrentCultureIgnoreCase))
            {
                Console.WriteLine("Invalid entry. Please enter 'c' or 'f'.");
                userInput = Console.ReadLine();
            }

            if (userInput.ToLower() == "c")
                return true;
            else
                return false;
        }
    }
}
