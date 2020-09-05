using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace GroupDMinefieldMidterm
{
    public class GameBoard
    {
        public bool HitMine { get; set; }
        public List<Point> MineCoordinates { get; set; }
        public int BoardRows { get; set; }
        public int BoardColumns { get; set; }
        public int NumberOfMines { get; set; }
        public Cell[,] Board { get; set; }
        public int RemainingCells { get; set; }

        public GameBoard(string difficulty)
        {
            GenerateBoard(difficulty);
            RemainingCells = (BoardRows * BoardColumns) - NumberOfMines;
            MineCoordinates = new List<Point>();
            PlaceMines();
        }
        private void GenerateBoard(string difficulty)
        {
            InitializeBoardValues(difficulty);

            Board = new Cell[BoardRows, BoardColumns];
            for (int i = 0; i < BoardRows; i++)
            {
                for (int j = 0; j < BoardColumns; j++)
                {
                    var boardCell = new Cell();
                    Board[i, j] = boardCell;
                    Board[i, j].CellValue = GameValues.Empty;
                }
            }
        }

        private void InitializeBoardValues(string difficulty)
        {
            switch (difficulty)
            {
                case "Beginner":
                    BoardRows = 9;
                    BoardColumns = 9;
                    NumberOfMines = 10;
                    break;
                case "Intermediate":
                    BoardRows = 16;
                    BoardColumns = 16;
                    NumberOfMines = 40;
                    break;
                case "Expert":
                    BoardRows = 16;
                    BoardColumns = 30;
                    NumberOfMines = 99;
                    break;
                default:
                    break;
            }
        }

        private void PlaceMines()
        {
            var random = new Random();
            for (int i = 0; i < NumberOfMines; i++)
            {
                int row;
                int column;
                do
                {
                    row = random.Next(0, BoardRows);
                    column = random.Next(0, BoardColumns);
                } while (Board[row, column].CellValue == GameValues.Mine);

                Board[row, column].CellValue = GameValues.Mine;
                PlaceNumber(row, column);
                MineCoordinates.Add(new Point(row, column));
            }
        }

        private void PlaceNumber(int row, int column)
        {

            List<Point> surroundingCells = GetSurroundingCells(row, column);

            foreach (Point point in surroundingCells)
            {
                int currentValue = (int)Board[point.X, point.Y].CellValue;

                if (currentValue != 9)
                    currentValue += 1;

                Board[point.X, point.Y].CellValue = (GameValues)currentValue;
            }
        }

        private List<Point> GetSurroundingCells(int row, int column)
        {
            List<Point> surroundingCells = new List<Point> {
            new Point(row - 1, column - 1),
            new Point(row - 1, column),
            new Point(row - 1, column + 1),
            new Point(row, column + 1),
            new Point(row + 1, column + 1),
            new Point(row + 1, column),
            new Point(row + 1, column - 1),
            new Point(row , column - 1),
            };

            foreach (var point in surroundingCells.ToList())
            {
                if (((point.X < 0) || (point.Y < 0) || (point.X > BoardRows - 1) || (point.Y > BoardColumns - 1)))
                {
                    surroundingCells.Remove(point);
                }
            }

            return surroundingCells;
        }

        private void RevealMines()
        {
            foreach (var mine in MineCoordinates)            
                Board[mine.X, mine.Y].Revealed = true;
            
        }
        public void CheckCell(Point point)
        {
            var cellValue = Board[point.X, point.Y].CellValue;

            if (!Board[point.X, point.Y].Revealed)
            {
                switch (cellValue)
                {
                    case GameValues.Empty:
                        Board[point.X, point.Y].Revealed = true;
                        RemainingCells--;
                        List<Point> surroundingCells = GetSurroundingCells(point.X, point.Y);
                        foreach (Point cell in surroundingCells)
                        {
                            CheckCell(cell);
                        }
                        break;
                    case GameValues.Mine:
                        HitMine = true;
                        RevealMines();
                        break;
                    default:
                        Board[point.X, point.Y].Revealed = true;
                        RemainingCells--;
                        break;
                }
            }
        }
    }
}

