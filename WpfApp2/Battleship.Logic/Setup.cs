using System.Collections.Generic;

namespace Battleship.Logic
{
    public static class Setup
    {
        private const int DEFAULT_WIDTH = 10;
        private const int DEFAULT_HEIGHT = 10;

        public static IEnumerable<Cell> GetBoard()
        {
            var cells = new List<Cell>();
            for (int row = 0; row < DEFAULT_WIDTH; row++)
            {
                for (int col = 0; col < DEFAULT_HEIGHT; col++)
                {
                    var cell = new Cell(row, col);
                    cells.Add(cell);
                }
            }

            return cells;
        }
    }
}
