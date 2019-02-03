using System.Collections.Generic;
using System.Linq;

namespace Battleship.Logic
{
    public class Ship
    {
        public Ship(string name, int size)
        {
            Name = name;
            Size = size;
        }

        public string Name { get; }
        public int Size { get; }
        public bool IsSunk => OccupiedCells.All(c => c.IsBombed);
        public IEnumerable<Cell> OccupiedCells { get; internal set; }
    }
}
