namespace Battleship.Logic
{
    public class Cell
    {
        internal Cell(char column, int columnNumber, int row)
        {
            Column = column;
            ColumnNumber = columnNumber;
            Row = row;
        }

        public char Column { get; private set; }
        public int ColumnNumber { get; private set; }
        public int Row { get; private set; }
        public bool IsBombed { get; internal set; }
        public bool IsOccupiedByShip { get; internal set; }

        public void BombCell()
        {
            IsBombed = true;
        }
    }
}
