using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic
{
    public class Ship
    {
        public Ship(string shipClass, int size)
        {
            Class = shipClass;
            Size = size;
        }

        public string Class { get; }
        public int Size { get; }
        public bool IsSunk => OccupiedCells.All(c => c.IsBombed);
        public IEnumerable<Cell> OccupiedCells { get; internal set; }
    }
}
