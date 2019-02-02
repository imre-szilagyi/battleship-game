using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic
{
    public class Ship
    {
        private IEnumerable<Cell> _occupiedCells;

        public Ship(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public int Size { get; }
        public bool IsSunk => OccupiedCells.All(c => c.IsBombed);
        public IEnumerable<Cell> OccupiedCells { get; internal set; }
    }
}
