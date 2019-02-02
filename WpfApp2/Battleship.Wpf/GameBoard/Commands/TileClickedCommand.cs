using System;
using System.Windows.Input;
using Battleship.Wpf.GameBoard.Models;

namespace Battleship.Wpf.GameBoard.Commands
{
    internal class TileClickedCommand : ICommand
    {
        private BoardTile _tile;

        public event EventHandler CanExecuteChanged;

        public TileClickedCommand(BoardTile tile)
        {
            _tile = tile;
        }

        public bool CanExecute(object parameter)
        {
            return _tile.IsTileEnabled;
        }

        public void Execute(object parameter)
        {
            _tile.Bomb();
        }
    }
}
