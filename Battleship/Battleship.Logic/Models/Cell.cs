namespace Battleship.Logic
{
    public class Cell
    {
        internal Cell(char column, int columnNumber, int row)
        {
            ColumnHeader = column;
            Column = columnNumber;
            Row = row;
        }

        public char ColumnHeader { get; private set; }
        public int Column { get; private set; }
        public int Row { get; private set; }
        public bool IsBombed { get; internal set; }
        public bool IsOccupiedByShip { get; internal set; }
    }
}
