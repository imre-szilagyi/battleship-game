namespace Battleship.Logic
{
    public class Cell
    {
        internal Cell(int row, int col)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; private set; }
        public int Col { get; private set; }
        public bool IsBombed { get; internal set; }
        public bool IsOccupiedByShip { get; internal set; }

        public void BombCell()
        {
            IsBombed = true;
        }
    }
}
