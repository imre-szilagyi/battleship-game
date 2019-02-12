using Battleship.Logic.Interfaces;
using Battleship.Logic.Players;
using System.Linq;

namespace Battleship.Logic
{
    public static class Setup
    {
        private const int DEFAULT_WIDTH = 10;
        private const int DEFAULT_HEIGHT = 10;

        public static IPlayer SetupSinglePlayerGame()
        {
            var board = new Board(DEFAULT_WIDTH, DEFAULT_HEIGHT);
            var shipfactory = new ShipFactory();
            board.Ships = shipfactory.GetSinglePlayerShips();

            var randomizer = new ShipRandomPlacer();
            randomizer.PlaceShips(board.Ships, board.Cells);
            var humanPlayer = new SinglePlayerHuman(board);

            return humanPlayer;
        }
    }
}
