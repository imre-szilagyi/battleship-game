using System.Collections.Generic;

namespace Battleship.Logic.Interfaces
{
    public interface IBoard
    {
        int Width { get; }
        int Height { get; }        
        IEnumerable<Cell> Cells { get; }
    }
}
