using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic
{
    public class Board 
    {
        private List<Ship> _ships;

        public Board(int width, int height)
        {
            _ships = new List<Ship>();

            Width = width;
            Height = height;

            FillBoard(width, height);
        }

        public event EventHandler<Ship> ShipSunk;
        public event EventHandler<Ship> ShipHit;

        public int Width { get; private set; }
        public int Height { get; private set; }
        public IEnumerable<Ship> Ships => _ships;
        public IEnumerable<Cell> Cells { get; private set; }

        public void Bomb(Cell cell)
        {
            cell.IsBombed = true;
            CheckIfAShipWasHit(cell);
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
        }

        internal void AddShip(Ship ship)
        {
            _ships.Add(ship);
        }

        private void FillBoard(int width, int height)
        {
            var cells = new List<Cell>();
            for (int row = 0; row < width; row++)
            {
                for (int col = 0; col < height; col++)
                {
                    var cell = new Cell(row, col);
                    cells.Add(cell);
                }
            }

            Cells = cells;
        }
    }
}
