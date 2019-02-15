using Battleship.Logic.Models;
using Battleship.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic
{
    internal class Board : IBoard
    {
        public Board(int columns, int rows)
        {
            ColumnCount = columns;
            RowCount = rows;
            FillBoard(columns, rows);
        }

        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }
        public IEnumerable<Ship> Ships { get; internal set; }
        public IEnumerable<Cell> Cells { get; private set; }

        public BombingResult Bomb(char column, int row)
        {
            var cell = Cells.FirstOrDefault(c => c.Row == row && c.ColumnHeader == column);
            if (cell == null)
                throw new IndexOutOfRangeException($"Invalid coordinates '{column}{row}'.");
            if (cell.IsBombed)
                throw new ArgumentException($"Cell '{cell.ColumnHeader}{cell.Row}' was already bombed.");

            cell.IsBombed = true;
            return GetResultOfBombing(cell);
        }

        private bool IsGameOver()
        {
            return Ships.All(s => s.IsSunk);
        }

        private BombingResult GetResultOfBombing(Cell cell)
        {
            if (Ships == null)
                return BombingResult.Miss;

            var ship = Ships.FirstOrDefault(s => s.OccupiedCells.Contains(cell));
            if (ship == null)
                return BombingResult.Miss;

            if (ship.IsSunk)
            {
                if (IsGameOver())
                    return BombingResult.AllShipsSunk;

                return BombingResult.Sink;
            }

            return BombingResult.Hit;
        }

        private void FillBoard(int columns, int rows)
        {
            //Only 26 letters in the alphabet
            int alphabetLetters = 26;
            if (columns > alphabetLetters)
                throw new ArgumentOutOfRangeException(nameof(columns), columns, $"Width is bigger than the allowed maximun of {alphabetLetters}");

            var columnChar = 'A';
            var cells = new List<Cell>();
            for (int column = 1; column <= columns; column++)
            {
                for (int row = 1; row <= rows; row++)
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
