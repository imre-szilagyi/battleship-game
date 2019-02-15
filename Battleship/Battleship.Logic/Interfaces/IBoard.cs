using System.Collections.Generic;

namespace Battleship.Logic.Interfaces
{
    public interface IBoard
    {
        int ColumnCount { get; }
        int RowCount { get; }        
        IEnumerable<Cell> Cells { get; }
    }
}
