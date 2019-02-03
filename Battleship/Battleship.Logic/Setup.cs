namespace Battleship.Logic
{
    public static class Setup
    {
        private const int DEFAULT_WIDTH = 10;
        private const int DEFAULT_HEIGHT = 10;

        public static Board GetBoard()
        {
            var board = new Board(DEFAULT_WIDTH, DEFAULT_HEIGHT);

            var shipfactory = new ShipFactory();
            var ships = shipfactory.GetSinglePlayerShips();
            var randomizer = new ShipRandomizer();
            randomizer.OcupyCellsWithShips(board.Cells, ships);

            board.Ships = ships;

            return board;
        }
    }
}
