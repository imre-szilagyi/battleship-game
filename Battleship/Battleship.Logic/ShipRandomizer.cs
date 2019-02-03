using System;
using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic
{
    internal class ShipRandomizer
    {
        public void OcupyCellsWithShips(IEnumerable<Cell> cells, IEnumerable<Ship> ships)
        {
            var random = new Random();

            var rows = cells.GroupBy(c => c.Row).OrderBy(c => c.Key).ToList();
            var col = cells.GroupBy(c => c.ColumnNumber).OrderBy(c => c.Key).ToList();
            int width = cells.Max(c => c.ColumnNumber) - 1;
            int height = cells.Max(c => c.Row) - 1;

            foreach (var ship in ships)
            {
                var unoccupiedCells = cells.Where(c => c.IsOccupiedByShip == false).ToList();

                FindCellsForShip(random, width, height, ship, unoccupiedCells);
            }
        }

        private void FindCellsForShip(Random random, int width, int height, Ship ship, List<Cell> unoccupiedCells)
        {
            for (int i = 0; i < 3; i++)
            {
                var cellsToOccupy = GetCellsForShip(random, ship.Size, unoccupiedCells, width, height);
                if (cellsToOccupy != null)
                {
                    ship.OccupiedCells = cellsToOccupy;
                    foreach (var cell in cellsToOccupy)
                    {
                        cell.IsOccupiedByShip = true;
                    }
                    break;
                }
            }

        }

        private IEnumerable<Cell> GetCellsForShip(Random random, int size, List<Cell> cells, int boardWidth, int boardHeight)
        {
            var cellsToOccupy = new List<Cell>();

            bool vertical = random.Next(0, 100) % 2 == 0;
            var startingColumn = random.Next(1, boardWidth);
            var startingRow = random.Next(1, boardHeight);


            cellsToOccupy = FindCells(cells, startingRow, startingColumn, boardHeight, boardWidth, size, vertical);
            if (cellsToOccupy == null)
                cellsToOccupy = FindCells(cells, startingRow, startingColumn, boardHeight, boardWidth, size, !vertical);


            return cellsToOccupy;
        }

        private List<Cell> FindCells(List<Cell> cells, int startingRow, int startingColumn, int boardHeight, int boardWidth, int size, bool vertical)
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


                    //Full loop around the board
                    col++;
                    col = col % boardWidth;
                    if (col == startingColumn)
                        break;
                }

                //Full loop around the board
                row++;
                row = row % boardHeight;
                if (row == startingRow)
                    break;
            }

            return null;
        }

        private List<Cell> FindHorizontalCellGroup(List<Cell> cells, int row, int col, int size)
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

        private List<Cell> FindVerticalCellGroup(List<Cell> cells, int row, int col, int size)
        {
            var tempRow = row;
            var vcells = new List<Cell>();
            for (int i = 0; i < size; i++)
            {
                var cell = GetCell(tempRow, col, cells);
                if (cell == null)
                    break;

                vcells.Add(cell);
                tempRow++;
            }

            if (vcells.Count < size)
            {
                tempRow = row;
                for (int i = 0; i < size; i++)
                {
                    var cell = GetCell(tempRow, col, cells);
                    if (cell == null)
                        break;

                    vcells.Add(cell);
                    tempRow--;
                }
            }

            return vcells.Count == size ? vcells : null;
        }

        private Cell GetCell(int row, int col, IEnumerable<Cell> cells)
        {
            return cells.FirstOrDefault(c => c.Row == row && c.ColumnNumber == col && c.IsOccupiedByShip == false);
        }
    }
}
