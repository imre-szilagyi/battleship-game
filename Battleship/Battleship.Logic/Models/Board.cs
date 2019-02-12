using Battleship.Logic.Models;
using Battleship.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic
{
    internal class Board : IBoard
    {
        public Board(int width, int height)
        {

            Width = width;
            Height = height;
            FillBoard(width, height);
        }

        public int Width { get; private set; }
        public int Height { get; private set; }
        public IEnumerable<Ship> Ships { get; internal set; }
        public IEnumerable<Cell> Cells { get; private set; }

        public BombingResult Bomb(int row, char column)
        {
            var cell = Cells.FirstOrDefault(c => c.Row == row && c.ColumnHeader == column);
            if (cell == null)
                throw new ArgumentException($"Invalid coordinates '{column}{row}'.");
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
