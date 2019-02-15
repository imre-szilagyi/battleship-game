using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic
{
    internal class ShipRandomPlacer
    {
        /// <summary>
        /// A very simple and dumb random placer.
        /// Sets the OccupiedCells property for each ship.
        /// </summary>
        /// <param name="ships">
        ///     A collection of ships, each ships OccupiedCells 
        ///     property will be set to a random group cells
        /// </param>
        /// <param name="cells">
        ///     The cells that the ships can occupy. Because this is a very simple placer, 
        ///     the number of rows and columns must be larger than the size of the largest ships
        ///     and the the total number of ships.        
        /// </param>
        /// <exception cref="System.ArgumentNullException">Thrown if parameter is null.</exception>
        /// <exception cref="System.ArgumentException">Thrown if parameter is invalid.</exception>
        public void PlaceShips(IEnumerable<Ship> ships, IEnumerable<Cell> cells)
        {
            VerifyArguments(ships, cells);

            //Place the larger ships first so that the smaller don't get in the way
            var shipsOrderedBySize = ships.OrderByDescending(s => s.Size);
            var unOccupiedCells = new List<Cell>(cells);
            foreach (var ship in shipsOrderedBySize)
            {
                var occupiedCells = GetCellsForShip(ship.Size, unOccupiedCells);
                if (occupiedCells == null)
                    throw new Exception($"Wans't able to find cells to ocupy for ship: {ship.Size}.");

                unOccupiedCells.RemoveAll(c => occupiedCells.Contains(c));
                ship.OccupiedCells = occupiedCells;
                foreach (var cell in occupiedCells)
                {
                    cell.IsOccupiedByShip = true;
                }
            }
        }

        private void VerifyArguments(IEnumerable<Ship> ships, IEnumerable<Cell> cells)
        {
            if (ships == null)
                throw new ArgumentNullException(nameof(ships));

            if (cells == null)
                throw new ArgumentNullException(nameof(cells));

            var maxShipSize = ships.Max(s => s.Size);
            var maxCol = cells.Max(c => c.Column);
            var maxRow = cells.Max(c => c.Row);

            if (maxShipSize > maxCol && maxShipSize > maxRow)
                throw new ArgumentException("Number or rows and columns must be greater or equal to the largest ship.");

            var shipsCount = ships.Count();
            if (shipsCount > maxCol && shipsCount > maxRow)
                throw new ArgumentException("Number or rows and columns must be greater or equal to the number of ships.");
        }

        private IEnumerable<Cell> GetCellsForShip(int shipSize, List<Cell> cells)
        {
            var random = new Random();
            var isVertical = random.Next(0, 2) == 1;
            var randomIndex = random.Next(0, cells.Count);
            var randomCell = cells.ElementAt(randomIndex);
            var startingColumn = randomCell.Column;
            var startingRow = randomCell.Row;
            var maxRow = cells.Max(c => c.Row);
            var maxColumn = cells.Max(c => c.Column);

            IEnumerable<Cell> cellsToOccupy = FindCells(cells, startingRow, startingColumn, maxRow, maxColumn, shipSize, isVertical);
            if (cellsToOccupy == null)
                cellsToOccupy = FindCells(cells, startingRow, startingColumn, maxRow, maxColumn, shipSize, !isVertical);

            return cellsToOccupy;
        }

        private IEnumerable<Cell> FindCells(IEnumerable<Cell> cells, int startingRow, int startingColumn, int maxRow, int maxColumn, int size, bool vertical)
        {
            for (int row = startingRow; ;)
            {
                for (int col = startingColumn; ;)
                {
                    if (vertical)
                    {
                        var vcells = FindVerticalCellGroup(cells, row, col, size);
                        if (vcells != null)
                            return vcells;
                    }
                    else
                    {
                        var vcells = FindHorizontalCellGroup(cells, row, col, size);
                        if (vcells != null)
                            return vcells;
                    }

                    //If the start cell is not a good cell, go around the board and check every cell.
                    //Full loop around the board
                    col++;
                    if (col > maxColumn)
                        col = 0;

                    if (col == startingColumn)
                        break;
                }

                //If the start cell is not a good cell, go around the board and check every cell.
                //Full loop around the board
                row++;
                if (row > maxRow)
                    row = 0;

                if (row == startingRow)
                    break;
            }

            return null;
        }

        private IEnumerable<Cell> FindHorizontalCellGroup(IEnumerable<Cell> cells, int row, int col, int size)
        {
            var tempCol = col;
            var vcells = new List<Cell>();
            for (int i = 0; i < size; i++)
            {
                var cell = GetCell(row, tempCol, cells);
                if (cell == null)
                    break;

                vcells.Add(cell);
                tempCol++;
            }

            if (vcells.Count < size)
            {
                tempCol = col;
                for (int i = 0; i < size; i++)
                {
                    var cell = GetCell(row, tempCol, cells);
                    if (cell == null)
                        break;

                    vcells.Add(cell);
                    tempCol--;
                }
            }

            return vcells.Count == size ? vcells : null;
        }

        private IEnumerable<Cell> FindVerticalCellGroup(IEnumerable<Cell> cells, int row, int col, int shipSize)
        {
            var tempRow = row;
            var vcells = new List<Cell>();
            for (int i = 0; i < shipSize; i++)
            {
                var cell = GetCell(tempRow, col, cells);
                if (cell == null)
                    break;

                vcells.Add(cell);
                tempRow++;
            }

            if (vcells.Count < shipSize)
            {
                tempRow = row;
                for (int i = 0; i < shipSize; i++)
                {
                    var cell = GetCell(tempRow, col, cells);
                    if (cell == null)
                        break;

                    vcells.Add(cell);
                    tempRow--;
                }
            }

            return vcells.Count == shipSize ? vcells : null;
        }

        private Cell GetCell(int row, int col, IEnumerable<Cell> cells)
        {
            return cells.FirstOrDefault(c => c.Row == row && c.Column == col);
        }
    }
}
