using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Battleship.Wpf.GameBoard.Commands;
using Battleship.Wpf.GameBoard.Models;

namespace Battleship.Wpf.GameBoard
{
    internal class GameBoardViewModel : INotifyPropertyChanged
    {
        private IEnumerable<IGrouping<int, BoardTile>> _groupedCells;

        public GameBoardViewModel()
        {
            RefreshCommand = new RefreshCommand(this);
            RefreshCommand.Execute(null);
        }

        public string StatusMessage { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public IEnumerable<IGrouping<int, BoardTile>> Tiles
        {
            get { return _groupedCells; }
            set
            {
                _groupedCells = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Tiles)));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        public void UpdateStatusMessage(string message)
        {
            StatusMessage = message;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StatusMessage)));
        }
    }
}
