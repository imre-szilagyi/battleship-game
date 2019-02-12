using System.Collections.Generic;

namespace Battleship.Logic
{
    internal class ShipFactory
    {
        public IEnumerable<Ship> GetSinglePlayerShips()
        {
            var ships = new List<Ship>();

            ships.Add(GetDestroyer());
            ships.Add(GetDestroyer());
            ships.Add(GetBattleship());

            return ships;
        }

        private Ship GetDestroyer()
        {
            return new Ship("Destroyer", 4);
        }

        private Ship GetBattleship()
        {
            return new Ship("Battleship", 5);
        }
    }
}
