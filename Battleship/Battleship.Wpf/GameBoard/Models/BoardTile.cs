using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using Battleship.Wpf.GameBoard.Commands;
using Battleship.Logic;

namespace Battleship.Wpf.GameBoard.Models
{
    internal class BoardTile : INotifyPropertyChanged
    {
        private Cell _cell;

        public BoardTile(Cell cell)
        {
            _cell = cell;
            TileClickedCommand = new TileClickedCommand(this);
            TileColor = new SolidColorBrush();
            RefreshCellColor();
        }

        public ICommand TileClickedCommand { get; private set; }
        public int Col => _cell.Col;
        public int Row => _cell.Row;
        public bool IsTileEnabled => !_cell.IsBombed;
        public SolidColorBrush TileColor { get; private set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void RefreshCellColor()
        {
            if (_cell.IsBombed)
            {
                if (_cell.IsOccupiedByShip)
                    TileColor.Color = Colors.Red;
                else
                    TileColor.Color = Colors.Orange;
            }
            else
            {
                TileColor.Color = Colors.White;
            }

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TileColor)));
        }

        internal void Bomb()
        {
            if (_cell.IsBombed)
                return;

            _cell.BombCell();
            RefreshCellColor();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsTileEnabled)));
        }
    }
}
