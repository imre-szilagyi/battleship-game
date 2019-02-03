using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic
{
    public class Board
    {
        public Board(int width, int height)
        {

            Width = width;
            Height = height;
            FillBoard(width, height);
        }

        public event EventHandler<Ship> ShipSunk;
        public event EventHandler<Ship> ShipHit;
        public event EventHandler ShipMiss;
        public event EventHandler<bool> GameOver;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public IEnumerable<Ship> Ships { get; internal set; }
        public IEnumerable<Cell> Cells { get; private set; }

        public bool Bomb(int row, char column)
        {
            var cell = Cells.FirstOrDefault(c => c.Row == row && c.Column == column);
            if (cell == null)
                return false;

            return Bomb(cell);
        }

        public bool Bomb(Cell cell)
        {
            if (cell.IsBombed == false)
            {
                cell.IsBombed = true;
                CheckIfAShipWasHit(cell);

                if (IsGameOver())
                    InvokeGameOver();
            }

            return true;
        }

        private void InvokeGameOver()
        {
            GameOver?.Invoke(this, true);
        }

        public bool IsGameOver()
        {
            return Ships.All(s => s.IsSunk);
        }

        private void CheckIfAShipWasHit(Cell cell)
        {
            var ship = Ships.FirstOrDefault(s => s.OccupiedCells.Contains(cell));
            if (ship != null)
            {
                if (ship.IsSunk)
                {
                    ShipSunk?.Invoke(this, ship);
                }
                else
                {
                    ShipHit.Invoke(this, ship);
                }
            }
            else
            {
                ShipMiss?.Invoke(this, EventArgs.Empty);
            }
        }

        private void FillBoard(int width, int height)
        {
            //Only 26 letters in the alphabet
            int alphabetLetters = 26;
            if (width > alphabetLetters)
                throw new ArgumentOutOfRangeException(nameof(width), width, $"Width is bigger than the allowed maximun of {alphabetLetters}");

            var columnChar = 'A';
            var cells = new List<Cell>();
            for (int column = 1; column <= height; column++)
            {
                for (int row = 1; row <= width; row++)
                {
                    var cell = new Cell(columnChar, column, row);
                    cells.Add(cell);
                }

                columnChar++;
            }

            Cells = cells;
        }

    }
}
