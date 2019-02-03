using System.ComponentModel;
using System.Windows.Input;
using Battleship.Wpf.GameBoard.Commands;
using Battleship.Logic;
using System.Runtime.CompilerServices;
using System;

namespace Battleship.Wpf.GameBoard.Models
{
    internal class BoardTile : INotifyPropertyChanged
    {
        private Action<BoardTile> _tileClickedCallback;

        public BoardTile(Cell cell, Action<BoardTile> tileClickedCallback)
        {
            Cell = cell;
            _tileClickedCallback = tileClickedCallback;
            TileClickedCommand = new TileClickedCommand(this);
            RefreshTile();
        }

        public Cell Cell { get; private set; }
        public ICommand TileClickedCommand { get; private set; }
        public int Row => Cell.Row;
        public char Column => Cell.Column;
        public bool IsTileEnabled => !Cell.IsBombed;
        public bool IsBombed => Cell.IsBombed;
        public bool ShowShip { get; set; }
        public bool IsOccupiedByShip => ShowShip ? Cell.IsOccupiedByShip : Cell.IsBombed && Cell.IsOccupiedByShip;

        public event PropertyChangedEventHandler PropertyChanged;

        public void RefreshTile()
        {
            RaisePropChanged(nameof(IsBombed));
            RaisePropChanged(nameof(IsOccupiedByShip));
        }

        internal void Clicked()
        {
            _tileClickedCallback(this);
        }

        private void RaisePropChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
